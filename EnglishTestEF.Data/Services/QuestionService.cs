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

        public Question GetLastQuestionOfJoueurWithLastPartieId(int idPartie)
        {
            return context.Question.Where(x => x.idPartie == idPartie).OrderByDescending(x => x.id).FirstOrDefault();
        }

        public void Save(Question question)
        {
            context.Question.Add(question);
            context.SaveChanges();
        }

        public List<Question> GetQuestionsByPartie(Partie partie)
        {
            return context.Question.Where(x => x.idPartie == partie.id).ToList();
        }
    }
}
