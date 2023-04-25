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
            IQueryable<Partie> Parties = from Partie in context.Partie
                                         where Partie.idJoueur == idJoueur
                                         select Partie;
            return Parties.Max(partie => partie.score);
        }

        public void Save(Partie partie)
        {
            context.Partie.Add(partie);
            context.SaveChanges();
        }

        public Partie FindPartieRecent(Joueur joueur)
        {
            return context.Partie.Where(x => x.idJoueur.Equals(joueur.id)).OrderByDescending(x => x.id).FirstOrDefault();
        }

        public void Update(Partie partie)
        {
            context.Entry(partie).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
    }
}
