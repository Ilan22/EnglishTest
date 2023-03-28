using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EnglishTest.Models
{
    public class ConnexionViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "L'adresse mail n'est pas valide.")]
        [Display(Name = "Courrier électronique")]
        public string email { get; set; }

        [Required]
        [Display(Name = "Mot de passe")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}