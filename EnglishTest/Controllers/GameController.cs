using System;
using System.Collections.Generic;
using System.Web.Mvc;
using EnglishTestEF.Data;
using EnglishTestEF.Data.Services;
using EnglishTest.Models;

namespace EnglishTest.Controllers
{
    public class GameController : Controller
    {
        private Random random = new Random();
        private List<Verbe> verbes = new List<Verbe>();
        private EnglishTestEntities context = new EnglishTestEntities();


        public ActionResult CreateGame()
        {
            Joueur joueur = (Joueur)Session["player"];

            if (joueur != null)
            {
                PartieService partieService = new PartieService(context);
                Partie partie = new Partie();
                partie.idJoueur = joueur.id;
                partieService.Save(partie);
                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("Index", "Home");
        }
        public ActionResult Index()
        {
            Joueur joueur = (Joueur)Session["player"];

            if (joueur != null)
            {
                GameService service = new GameService(context);
                PartieService partieService = new PartieService(context);

                Partie partie = partieService.FindPartieRecent(joueur);

                Verbe verbe = service.GetAllRandom(partie);

                GameViewModel viewModel = new GameViewModel(verbe);

                return View(viewModel);
            }
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Index(GameViewModel model)
        {
            QuestionService questionService = new QuestionService(new EnglishTestEntities());
            PartieService partieService = new PartieService(context);
            Question question = new Question();
            Joueur player = (Joueur)Session["player"];
            Partie partie = partieService.FindPartieRecent(player);
            question.idVerbe = model.verbe.id;
            question.idPartie = partie.id;
            question.Verbe = model.verbe;
            question.reponseParticipePasse = model.reponseParticipePasser;
            question.reponsePreterit = model.reponsePreterit;
            question.dateEnvoie = DateTime.Now;
            question.dateReponse = DateTime.Now;
            questionService.Save(question);
            Console.WriteLine(model);

            if (model.reponseParticipePasser == model.verbe.participePasse && model.reponsePreterit == model.verbe.preterit)
            {
                partie.score = partie.score + 1;
                partieService.Update(partie);
                if (partie.score == 161)
                {
                    return RedirectToAction("GameOver");
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("GameOver");
        }

        public ActionResult Logout()
        {
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

        public ActionResult GameOver()
        {
            Joueur joueur = (Joueur)Session["player"];

            if (joueur != null)
            {
                PartieService partieService = new PartieService(new EnglishTestEntities());

                GameOverViewModel gameOverViewModel = new GameOverViewModel();

                gameOverViewModel.nomJoueur = joueur.prenom;
                gameOverViewModel.meilleurScore = partieService.GetHighestScore(joueur.id);

                partieService = new PartieService(new EnglishTestEntities());
                Partie lastPartie = partieService.FindPartieRecent(joueur);
                gameOverViewModel.lastPartie = lastPartie;

                QuestionService questionService = new QuestionService(new EnglishTestEntities());
                VerbeService verbeService = new VerbeService(new EnglishTestEntities());
                gameOverViewModel.lastVerbe = verbeService.GetLastVerbeOfPartieWithQuestion(questionService.GetLastQuestionOfJoueurWithLastPartieId(lastPartie.id));

                return View(gameOverViewModel);
            }
            else
                return RedirectToAction("Index", "Home");
        }
    }
}