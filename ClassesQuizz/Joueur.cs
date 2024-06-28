using System;

namespace ClassesQuizz
{
    public class Joueur
    {
        private int numJoueur;
        private string nomJoueur;
        private int scoreJoueur;

        public Joueur(int unNum, string unNom, int unScore)
        {
            NumJoueur = unNum;
            NomJoueur = unNom;
            ScoreJoueur = unScore;
        }

        public int NumJoueur { get => numJoueur; set => numJoueur = value; }
        public string NomJoueur { get => nomJoueur; set => nomJoueur = value; }
        public int ScoreJoueur { get => scoreJoueur; set => scoreJoueur = value; }
    }
}
