using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTestEF.Data.Services
{
    
    public class GameService
    {
        private EnglishTestEntities context;
        public GameService(EnglishTestEntities context)
        {
            this.context = context;
        }

        public List<Verbe> GetVerbes()
        {
            using (context)
            {
                return context.Verbe.ToList();
            }
        }
    }
}
