using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTestEF.Data.Services
{
    public class VilleService
    {
        private EnglishTestEntities context;

        public VilleService(EnglishTestEntities context)
        {
            this.context = context;
        }

        public List<Ville> GetVilles()
        {
            using (context)
            {
                return context.Ville.ToList();
            }
        }

        public String GetNomVille(int idVille)
        {
            using (context)
            {
                IQueryable<Ville> ville = from Ville in context.Ville
                                          where Ville.id == idVille
                                          select Ville;
                return ville.FirstOrDefault().nom;
            }
        }
    }
}
