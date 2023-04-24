using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTestEF.Data.Services
{
    
    public class QuestionService
    {
        private EnglishTestEntities context;
        public QuestionService(EnglishTestEntities context)
        {
            this.context = context;
        }

        public void Save(Question question)
        {
            using (context)
            {
                context.Question.Add(question);
                context.SaveChanges();
            }
        }
    }
}
