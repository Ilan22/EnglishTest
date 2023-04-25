using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnglishTest.Models;
using EnglishTestEF.Data;
using EnglishTestEF.Data.Services;

namespace EnglishTest.Controllers
{
    public class GameController : Controller
    {
        private Random random = new Random();
        private List<Verbe> verbes = new List<Verbe>();
        private EnglishTestEntities context = new EnglishTestEntities();


        public ActionResult CreateGame()
        {
            PartieService partieService = new PartieService(context);
            JoueurService joueurService = new JoueurService(context);
            partieService = new PartieService(new EnglishTestEntities());
            Partie partie = new Partie();
            partie = new Partie();
            //Joueur player = (Joueur)Session["player"];
            //player = joueurService.GetItem(player.id);
            Joueur john = joueurService.GetItem(1); // faire en sorte de recup le joueur en session
            partie.idJoueur = john.id;
            partieService.Save(partie);
            return RedirectToAction("Index");
        }
        public ActionResult Index()
        {
            Verbe verbe = new Verbe();
            
            GameService service = new GameService(context);
            PartieService partieService = new PartieService(context);
            JoueurService joueurService = new JoueurService(context);
            Partie partie = new Partie();
            //Joueur player = (Joueur)Session["player"];
            //player = joueurService.GetItem(player.id);
            Joueur john = joueurService.GetItem(1); // faire en sorte de recup le joueur en session
            
            partie = partieService.FindPartieRecent(john);

            if (partie == null)
            {
                partieService = new PartieService(new EnglishTestEntities());
                partie = new Partie();
                partie.idJoueur = john.id;
                partieService.Save(partie);
            }

            verbe = service.GetAllRandom(partie);

            GameViewModel viewModel = new GameViewModel(verbe);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(GameViewModel model)
        {
            QuestionService questionService = new QuestionService(new EnglishTestEntities());
            PartieService partieService = new PartieService(context);
            JoueurService joueurService = new JoueurService(context);
            Partie partie = new Partie();
            Question question = new Question();
            Joueur john = joueurService.GetItem(1); // faire en sorte de recup le joueur en session
            partie = partieService.FindPartieRecent(john);
            question.idVerbe = model.verbe.id;
            question.idPartie = partie.id;
            question.Verbe = model.verbe;
            question.reponseParticipePasse = model.reponseParticipePasser;
            question.reponsePreterit = model.reponsePreterit;
            question.dateEnvoie = DateTime.Now;
            question.dateReponse = DateTime.Now;
            questionService.Save(question);
            Console.WriteLine(model);
            if(partie.score == 161)
            {
                //redirection vers game over
            }
            if (model.reponseParticipePasser == model.verbe.participePasse && model.reponsePreterit == model.verbe.preterit)
            {
                partie.score = partie.score + 1;
                partieService.Update(partie);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");//a modifier pour rediriger vers le game over
        }

        public ActionResult Logout()
        {
            // test si la validation est ok
            if (ModelState.IsValid)
            {
                Session["player"] = null;
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Informations()
        {
            Joueur joueur = (Joueur)Session["player"];

            if (joueur != null)
            {
                InscriptionViewModel inscriptionViewModel = new InscriptionViewModel();

                VilleService villeService = new VilleService(new EnglishTestEntities());

                inscriptionViewModel.Prenom = joueur.prenom;
                inscriptionViewModel.Email = joueur.email;
                inscriptionViewModel.Nom = joueur.nom;
                inscriptionViewModel.NomVille = villeService.GetNomVille(joueur.idVille);

                return View(inscriptionViewModel);
            }
            else
                return RedirectToAction("Index", "Home");

        }
    }
}