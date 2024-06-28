using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GstBdd;
using ClassesQuizz;

namespace App_Quizz
{
    /// <summary>
    /// Logique d'interaction pour Partie.xaml
    /// </summary>
    public partial class Partie : Window
    {
        private Joueur joueur1;
        int difficulté;
        string laquestion;
        string laprop1;
        string laprop2;
        string laprop3;
        string laprop4;
        string lareponse;
        private List<Question> lesquestions;
        Gst gst;

        public Partie(string joueur,int diff)
        {
            InitializeComponent();
            gst = new Gst();
            List<Joueur> lejoueur = gst.GetJoueur(joueur);
            joueur1 = lejoueur[0];
            difficulté = diff;
            List<Quiz> quiz1 = gst.GetQuiz(diff);
            List<Question> questions1 = gst.GetQuestionsQuiz(Convert.ToInt16(quiz1[0].NumQuizz));
            lesquestions = questions1;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            laquestion = lesquestions[0].LibQuestion;
            laprop1 = lesquestions[0].PropUn;
            laprop2 = lesquestions[0].PropDeux;
            laprop3 = lesquestions[0].PropTrois;
            laprop4 = lesquestions[0].PropQuatre;
            lareponse = lesquestions[0].ReponseQuestion;

            libQ.Text = laquestion;
            P1.Content = laprop1;
            P2.Content = laprop2;
            P3.Content = laprop3;
            P4.Content = laprop4;

        }

        private void P1_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
            int position = -1;

            // Vérifie si la réponse est correcte
            if (P1.Content.ToString() == lareponse)
            {
                joueur1.ScoreJoueur++;
            }

            // Recherche de la question actuelle
            foreach (Question question in lesquestions)
            {
                if (question.LibQuestion == laquestion)
                {
                    position = index;
                    break;
                }
                index++;
            }

            // Vérifie que la position trouvée est valide et que la prochaine question est disponible
            if (position != -1 && position + 1 < lesquestions.Count)
            {
                position++; // Passe à la question suivante

                laquestion = lesquestions[position].LibQuestion;
                laprop1 = lesquestions[position].PropUn;
                laprop2 = lesquestions[position].PropDeux;
                laprop3 = lesquestions[position].PropTrois;
                laprop4 = lesquestions[position].PropQuatre;
                lareponse = lesquestions[position].ReponseQuestion;

                // Mise à jour des éléments UI
                libQ.Text = laquestion;
                P1.Content = laprop1;
                P2.Content = laprop2;
                P3.Content = laprop3;
                P4.Content = laprop4;
            }
            else
            {
                // Gérer la fin de la liste des questions ou d'autres conditions de sortie
                MessageBox.Show("Vous avez répondu à toutes les questions. \n Score : " + joueur1.ScoreJoueur.ToString());
                gst.majJoueur(joueur1.NomJoueur, joueur1.ScoreJoueur);
                this.Close();
            }
        }

        private void P2_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
            int position = -1;

            // Vérifie si la réponse est correcte
            if (P2.Content.ToString() == lareponse)
            {
                joueur1.ScoreJoueur++;
            }

            // Recherche de la question actuelle
            foreach (Question question in lesquestions)
            {
                if (question.LibQuestion == laquestion)
                {
                    position = index;
                    break;
                }
                index++;
            }

            // Vérifie que la position trouvée est valide et que la prochaine question est disponible
            if (position != -1 && position + 1 < lesquestions.Count)
            {
                position++; // Passe à la question suivante

                laquestion = lesquestions[position].LibQuestion;
                laprop1 = lesquestions[position].PropUn;
                laprop2 = lesquestions[position].PropDeux;
                laprop3 = lesquestions[position].PropTrois;
                laprop4 = lesquestions[position].PropQuatre;
                lareponse = lesquestions[position].ReponseQuestion;

                // Mise à jour des éléments UI
                libQ.Text = laquestion;
                P1.Content = laprop1;
                P2.Content = laprop2;
                P3.Content = laprop3;
                P4.Content = laprop4;
            }
            else
            {
                // Gérer la fin de la liste des questions ou d'autres conditions de sortie
                MessageBox.Show("Vous avez répondu à toutes les questions. \n Score : " + joueur1.ScoreJoueur.ToString());
                gst.majJoueur(joueur1.NomJoueur, joueur1.ScoreJoueur);
                this.Close();
            }
        }

        private void P3_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
            int position = -1;

            // Vérifie si la réponse est correcte
            if (P3.Content.ToString() == lareponse)
            {
                joueur1.ScoreJoueur++;
            }

            // Recherche de la question actuelle
            foreach (Question question in lesquestions)
            {
                if (question.LibQuestion == laquestion)
                {
                    position = index;
                    break;
                }
                index++;
            }

            // Vérifie que la position trouvée est valide et que la prochaine question est disponible
            if (position != -1 && position + 1 < lesquestions.Count)
            {
                position++; // Passe à la question suivante

                laquestion = lesquestions[position].LibQuestion;
                laprop1 = lesquestions[position].PropUn;
                laprop2 = lesquestions[position].PropDeux;
                laprop3 = lesquestions[position].PropTrois;
                laprop4 = lesquestions[position].PropQuatre;
                lareponse = lesquestions[position].ReponseQuestion;

                // Mise à jour des éléments UI
                libQ.Text = laquestion;
                P1.Content = laprop1;
                P2.Content = laprop2;
                P3.Content = laprop3;
                P4.Content = laprop4;
            }
            else
            {
                // Gérer la fin de la liste des questions ou d'autres conditions de sortie
                MessageBox.Show("Vous avez répondu à toutes les questions. \n Score : " + joueur1.ScoreJoueur.ToString());
                gst.majJoueur(joueur1.NomJoueur, joueur1.ScoreJoueur);
                this.Close();
            }
        }

        private void P4_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
            int position = -1;

            // Vérifie si la réponse est correcte
            if (P4.Content.ToString() == lareponse)
            {
                joueur1.ScoreJoueur++;
            }

            // Recherche de la question actuelle
            foreach (Question question in lesquestions)
            {
                if (question.LibQuestion == laquestion)
                {
                    position = index;
                    break;
                }
                index++;
            }

            // Vérifie que la position trouvée est valide et que la prochaine question est disponible
            if (position != -1 && position + 1 < lesquestions.Count)
            {
                position++; // Passe à la question suivante

                laquestion = lesquestions[position].LibQuestion;
                laprop1 = lesquestions[position].PropUn;
                laprop2 = lesquestions[position].PropDeux;
                laprop3 = lesquestions[position].PropTrois;
                laprop4 = lesquestions[position].PropQuatre;
                lareponse = lesquestions[position].ReponseQuestion;

                // Mise à jour des éléments UI
                libQ.Text = laquestion;
                P1.Content = laprop1;
                P2.Content = laprop2;
                P3.Content = laprop3;
                P4.Content = laprop4;
            }
            else
            {
                // Gérer la fin de la liste des questions ou d'autres conditions de sortie
                MessageBox.Show("Vous avez répondu à toutes les questions. \n Score : "+joueur1.ScoreJoueur.ToString());
                gst.majJoueur(joueur1.NomJoueur, joueur1.ScoreJoueur);
                this.Close();
            }
        }
    }
}
