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
using System.Windows.Navigation;
using System.Windows.Shapes;
using GstBdd;
using ClassesQuizz;

namespace App_Quizz
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            
        }
        
        // Chargement de la classe gérant la base de données
        Gst gst;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gst = new Gst();
            // La comboBox Pseudo est chargée avec le pseudo de chaque joueur présent dans la base de données
            Pseudo.ItemsSource = gst.GetAllJoueurs();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Pseudo.SelectedItem == null)
            {
                MessageBox.Show("Selectionnez un joueur", "AppQuiz", MessageBoxButton.OK, MessageBoxImage.Question);
            }
            else
            {
                string joueur = (Pseudo.SelectedItem as Joueur).NomJoueur;
                Partie nouveauQuiz = new Partie(joueur, (int)Diff.Value);
                nouveauQuiz.ShowDialog();
            }
            
        }

        // Accès à la fenêtre permettant l'ajout de question.
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Ajout_Question addQ = new Ajout_Question();
            if(Login.Text=="admin" && Mdp.Text=="")
            {
                addQ.ShowDialog();
            }
            else
            {
                MessageBox.Show("Identifiants incorrects","AppQuiz", MessageBoxButton.OK, MessageBoxImage.Question);
            }
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (nouvJoueur.Text == "")
            {
                MessageBox.Show("Veuillez inscrire votre pseudo", "AppQuiz", MessageBoxButton.OK, MessageBoxImage.Question);
            }
            int verif = gst.VérifierPseudo(nouvJoueur.Text);
            if (verif <= 0)
            {
                gst.AjouterPseudo(nouvJoueur.Text,0);
                Pseudo.ItemsSource = gst.GetAllJoueurs();
                MessageBox.Show("Votre pseudo a été ajouté dans la base de données.", "AppQuiz", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Ce pseudo existe déja.Veuillez en choisir une autre.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Diff_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Diff.Value = Math.Round(Diff.Value);
            SlideValue.Text = Diff.Value.ToString("F0");
        }
    }
}
