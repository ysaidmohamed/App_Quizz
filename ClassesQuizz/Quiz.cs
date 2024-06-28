using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesQuizz
{
    public class Quiz
    {
        private int numQuizz;
        private int diffQuizz;

        public Quiz(int unNum,int uneDiff)
        {
            NumQuizz = unNum;
            DiffQuizz = uneDiff;
        }

        public int NumQuizz { get => numQuizz; set => numQuizz = value; }
        public int DiffQuizz { get => diffQuizz; set => diffQuizz = value; }
    }
}
