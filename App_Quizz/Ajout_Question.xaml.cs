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
using System.Text.RegularExpressions;
using ClassesQuizz;

namespace App_Quizz
{
    /// <summary>
    /// Logique d'interaction pour Ajout_Question.xaml
    /// </summary>
    public partial class Ajout_Question : Window
    {
        public Ajout_Question()
        {
            Gst gst = new Gst();
            InitializeComponent();
            lstQuiz.ItemsSource = gst.GetAllQuizz();
        }

        // La valeur du slider est toujours entière.
        private void Diff_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Diff.Value = Math.Round(Diff.Value);
            SlideValue.Text = Diff.Value.ToString("F0");
        }

        // Vérifications avant d'ajouter une question.
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Gst gst = new Gst();
            if (AreAllTextBoxesValid())
            {   
                // Vérifie si la réponse est égale a une des propositions.
                if(R1.Text==P1.Text || R1.Text == P2.Text || R1.Text == P3.Text || R1.Text == P4.Text)
                {
                    int verif = gst.VérifierQuestion(Q1.Text);
                    if (verif <= 0)
                    {
                        gst.AjouterQuestion((int)Diff.Value, Q1.Text, P1.Text, P2.Text, P3.Text, P4.Text, R1.Text);
                        MessageBox.Show("Votre question a été ajouté dans la base de données.", "AppQuiz", MessageBoxButton.OK, MessageBoxImage.Information);
                        lstQuestions.ItemsSource = gst.GetQuestionByDiff((lstQuiz.SelectedItem as Quiz).DiffQuizz);
                    }
                    else
                    {
                        MessageBox.Show("Cette question existe déja.Veuillez en choisir une autre.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("La réponse ne correspond à aucune des propositions", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
            else
            {
                MessageBox.Show("Le formulaire est incomplet ou mal complété.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Les textbox de la question,des propositions et des réponses doivent être préalablement remplis
        private bool AreAllTextBoxesValid()
        {
            TextBox[] textBoxes = { Q1, P1, P2, P3, P4, R1 };
            Regex letterRegex = new Regex(@"[a-zA-Z]");

            foreach (var textBox in textBoxes)
            {
                if (string.IsNullOrWhiteSpace(textBox.Text) || !letterRegex.IsMatch(textBox.Text))
                {
                    return false;
                }
            }

            return true;
        }

        private void lstQuiz_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Gst gst = new Gst();
            lstQuestions.ItemsSource = gst.GetQuestionByDiff((lstQuiz.SelectedItem as Quiz).DiffQuizz);
            if (lstQuestions.IsEnabled == false)
            {
                lstQuestions.IsEnabled = true;
            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Gst gst = new Gst();
            // Vérifier si la question n'est pas déja associé au quiz
            if (lstQuiz.SelectedItem !=null && lstQuestions.SelectedItem !=null)
            {
                int verif = gst.VérifierQuestionQuiz((lstQuiz.SelectedItem as Quiz).NumQuizz,(lstQuestions.SelectedItem as Question).NumQuestion);
                if (verif <= 0)
                {
                    gst.AjouterQuestionQuiz((lstQuiz.SelectedItem as Quiz).NumQuizz, (lstQuestions.SelectedItem as Question).NumQuestion);
                    MessageBox.Show("La question a été ajoutée au quiz.", "AppQuiz", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Cette question est déja liée à ce quizz.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }
    }
}
