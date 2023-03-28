using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EnglishTestEF.Data;

namespace EnglishTest.Models
{
    public class InscriptionViewModel
    {
        private string nom;

        [Required]
        [Display(Name = "Nom")]
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        [Required]
        [Display(Name = "Prénom")]
        public string Prenom { get; set; }

        private string email { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "L'adresse mail n'est pas valide.")]
        [Display(Name = "Courrier électronique")]
        public string Email { get => email; set => email = value; }

        [Required]
        [Display(Name = "Mot de passe")]
        [DataType(DataType.Password)]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "Le mot de passe doit comporter au moins {2} caractères et {1} caractères maximum.")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirmer le mot de passe")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Le mot de passe ne correspond pas.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Ville de résidence")]
        public int IdVille { get; set; }

        private List<Ville> listVille;

        public List<Ville> ListVille { get => listVille; set => listVille = value; }

        public string NomVille { get; set; }
    }
}