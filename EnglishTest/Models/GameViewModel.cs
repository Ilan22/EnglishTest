using EnglishTestEF.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EnglishTest.Models
{
    public class GameViewModel
    {
        public Verbe verbe { get; set; }
        [Required]
        public string reponseParticipePasser { get; set; }
        [Required]
        public string reponsePreterit { get; set; }
        public bool isOK { get; set; }

        public GameViewModel()
        {
            
        }

        public GameViewModel(Verbe verbe, string reponsePreterit = "", string reponseParticipePasser = "", bool isOk = false)
        {
            this.verbe = verbe;
            this.reponseParticipePasser= reponseParticipePasser;
            this.reponsePreterit= reponsePreterit;
            this.isOK = isOk;
        }
    }

    public class GameIndexViewModel
    {
        public List<GameViewModel> gameViewModels { get; set; }
        public int currentIndex { get; set; }

        public GameIndexViewModel()
        {
            gameViewModels = new List<GameViewModel>();
            currentIndex = 0;
        }

        public GameViewModel CurrentItem
        {
            get
            {
                return gameViewModels[currentIndex];
            }   
        }

        public void MoveToNextItem()
        {
            currentIndex++;
            if(currentIndex >= gameViewModels.Count)
            {
                currentIndex = 0;
            }
        }

    }
}