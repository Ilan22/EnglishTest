using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTestEF.Data.Services
{
    public class JoueurService
    {
        private EnglishTestEntities context;

        public JoueurService(EnglishTestEntities context)
        {
            this.context = context;
        }

        public Joueur GetItem(int id)
        {
                return context.Joueur.Find(id);
        }

        public Joueur GetItem(string email, string motDePasse)
        {
                IQueryable<Joueur> Joueurs = from Joueur in context.Joueur
                                             where Joueur.email == email
                                             && Joueur.motDePasse == motDePasse
                                             select Joueur;
                return Joueurs.FirstOrDefault();
        }

        public void Insert(Joueur Joueur)
        {
                context.Joueur.Add(Joueur);
                context.SaveChanges();
        }

        public void Update(Joueur Joueur)
        {
                context.Entry(Joueur).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
        }

        public void Delete(Joueur Joueur)
        {
                context.Entry(Joueur).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
        }
    }
}
