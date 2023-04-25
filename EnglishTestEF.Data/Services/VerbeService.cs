using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTestEF.Data.Services
{
    public class VerbeService
    {
        private EnglishTestEntities context;

        public VerbeService(EnglishTestEntities context)
        {
            this.context = context;
        }

        public Verbe GetLastVerbeOfPartieWithQuestion(Question question)
        {
            return context.Verbe.Where(x => x.id == question.idVerbe).FirstOrDefault();
        }
    }
}
