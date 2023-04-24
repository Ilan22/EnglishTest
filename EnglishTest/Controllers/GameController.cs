using System;
using System.Collections.Generic;
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
        public ActionResult Index()
        {
            List<Verbe> verbes = new List<Verbe>();
            GameService service = new GameService(new EnglishTestEntities());
            PartieService partieService = new PartieService(new EnglishTestEntities());
            QuestionService questionService = new QuestionService(new EnglishTestEntities());
            verbes = service.GetVerbes();
            List<Verbe> verbesRandom = new List<Verbe>();
            verbesRandom = verbes.OrderBy(x => x.id = random.Next()).ToList();
            GameIndexViewModel viewModel = new GameIndexViewModel();
            viewModel.currentIndex = 0;
            Partie partie = new Partie();
            partie.idJoueur = 1;
            partieService.Save(partie);

            foreach (Verbe verbe in verbesRandom)
            {
                GameViewModel gameViewModel = new GameViewModel(verbe);
                viewModel.gameViewModels.Add(gameViewModel);
                Question question = new Question();
                question.idVerbe = verbe.id;
                question.idPartie = partie.id;
                //questionService.Save(question);
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(GameIndexViewModel model)
        {
            QuestionService questionService = new QuestionService(new EnglishTestEntities());
            Question question = new Question();
            question.idVerbe = model.CurrentItem.verbe.id;
            question.idPartie = model.CurrentItem.verbe.id;
            Console.WriteLine(model);
            if (model.CurrentItem.reponseParticipePasser == model.CurrentItem.verbe.participePasse && model.CurrentItem.reponsePreterit == model.CurrentItem.verbe.preterit) 
            {
                model.MoveToNextItem();
                return View(model);
            }
            return View(model);
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