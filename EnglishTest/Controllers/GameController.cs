using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EnglishTestEF.Data;
using EnglishTestEF.Data.Services;
using EnglishTest.Models;

namespace EnglishTest.Controllers
{
    public class GameController : Controller
    {
        public ActionResult GameOver()
        {
            Joueur test = new Joueur();
            test.id = 29;
            test.prenom = "Ilan";
            Session["player"] = test;

            PartieService partieService = new PartieService(new EnglishTestEntities());

            GameViewModel gameViewModel = new GameViewModel();
            
            Joueur player = (Joueur)Session["player"];

            gameViewModel.nomJoueur = player.prenom;
            gameViewModel.meilleurScore = partieService.GetHighestScore(player.id);

            return View(gameViewModel);
        }
    }
}