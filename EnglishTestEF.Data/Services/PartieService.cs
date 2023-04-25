using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public void Save(Partie partie)
        {
                context.Partie.Add(partie);
                context.SaveChanges();
        }

        public Partie FindPartieRecent(Joueur joueur) 
        {
            Partie partie = null;
            partie = context.Partie.Where(x => x.idJoueur.Equals(joueur.id)).OrderByDescending(x => x.id).FirstOrDefault();
            return partie;
        }

        public void Update(Partie partie)
        {
                context.Entry(partie).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
        }
    }
}
