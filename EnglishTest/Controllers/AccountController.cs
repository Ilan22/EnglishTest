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
    public class AccountController : Controller
    {
        public ActionResult Register()
        {
            VilleService villeService = new VilleService(new EnglishTestEntities());

            InscriptionViewModel viewModel = new InscriptionViewModel();
            viewModel.ListVille = villeService.GetVilles();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Register(InscriptionViewModel model)
        {
            // test si la validation est ok
            if (ModelState.IsValid)
            {
                JoueurService joueurService = new JoueurService(new EnglishTestEntities());

                Joueur joueur = new Joueur();

                joueur.email = model.Email;
                joueur.nom = model.Nom;
                joueur.prenom = model.Prenom;
                joueur.motDePasse = model.Password;
                joueur.idVille = model.IdVille;

                joueurService.Insert(joueur);

                return RedirectToAction("Login", "Account");
            }

            VilleService villeService = new VilleService(new EnglishTestEntities());
            model.ListVille = villeService.GetVilles();

            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(ConnexionViewModel model)
        {
            // test si la validation est ok
            if (ModelState.IsValid)
            {
                JoueurService joueurService = new JoueurService(new EnglishTestEntities());

                Joueur joueur = joueurService.GetItem(model.email, model.password);

                if (joueur != null)
                {
                    Session["player"] = joueur;
                    return RedirectToAction("Informations", "Account");
                }
                else
                {
                    ViewBag.Erreur = "Login ou mot de passe incorrect";
                }
            }

            return View();
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