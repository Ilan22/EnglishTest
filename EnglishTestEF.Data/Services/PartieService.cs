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

        public void Save(Partie partie)
        {
            using (context)
            {
                context.Partie.Add(partie);
                context.SaveChanges();
            }
        }
    }
}
