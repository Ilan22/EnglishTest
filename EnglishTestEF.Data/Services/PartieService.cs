using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTestEF.Data.Services
{
    public class PartieService
    {
        private EnglishTestEntities context;

        public PartieService(EnglishTestEntities context)
        {
            this.context = context;
        }

        public int GetHighestScore(int idJoueur)
        {
            using (context)
            {
                IQueryable<Partie> Parties = from Partie in context.Partie
                                             where Partie.idJoueur == idJoueur
                                             select Partie;
                return Parties.Max(partie => partie.score);
            }
        }

        public Partie GetLastPartieOfJoueur(int idJoueur)
        {
            using (context)
            {
                IQueryable<Partie> Parties = from Partie in context.Partie
                                             where Partie.idJoueur == idJoueur
                                             select Partie;
                return Parties.LastOrDefault();
            }
        }
    }
}
