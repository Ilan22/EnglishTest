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

        public List<Verbe> GetAll()
        {
            return context.Verbe.ToList();
        }

        public Verbe GetById(int id)
        {
            return context.Verbe.Find(id);
        }

        public Verbe GetAllRandom(Partie partie)
        {
            List<Question> alreadyExist = new List<Question>();

                QuestionService questionService = new QuestionService(context);
                alreadyExist = questionService.GetQuestionsByPartie(partie);

                int countVerbe = GetAll().Count;

                Random random = new Random();
                Verbe verbe = null;
                while(verbe == null)
                {
                    verbe = GetById(random.Next(countVerbe));
                    Question qAR = alreadyExist.Find(m => m.idVerbe == verbe.id);
                    if (qAR != null)
                    {
                        verbe = null;
                    }
                }

                return verbe;
        }
    }
}
