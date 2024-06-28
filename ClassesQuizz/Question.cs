using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesQuizz
{
    public class Question
    {
        private int numQuestion;
        private int diffQuestion;
        private string libQuestion;
        private string propUn;
        private string propDeux;
        private string propTrois;
        private string propQuatre;
        private string reponseQuestion;

        public Question(int uneQuestion,int uneDiff,string unLib,string unePropUn,string unePropDeux,string unePropTrois,string unePropQuatre,string uneReponse)
        {
            NumQuestion = uneQuestion;
            DiffQuestion = uneDiff;
            LibQuestion = unLib;
            PropUn = unePropUn;
            PropDeux = unePropDeux;
            PropTrois = unePropTrois;
            PropQuatre = unePropQuatre;
            ReponseQuestion = uneReponse;
        }

        public int NumQuestion { get => numQuestion; set => numQuestion = value; }
        public int DiffQuestion { get => diffQuestion; set => diffQuestion = value; }
        public string LibQuestion { get => libQuestion; set => libQuestion = value; }
        public string PropUn { get => propUn; set => propUn = value; }
        public string PropDeux { get => propDeux; set => propDeux = value; }
        public string PropTrois { get => propTrois; set => propTrois = value; }
        public string PropQuatre { get => propQuatre; set => propQuatre = value; }
        public string ReponseQuestion { get => reponseQuestion; set => reponseQuestion = value; }
    }
}
