using EnglishTest.Models;
using EnglishTestEF.Data;
using EnglishTestEF.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnglishTest.Controllers
{
    public class HomeController : Controller
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
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}