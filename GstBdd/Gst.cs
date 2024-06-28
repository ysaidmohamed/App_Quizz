using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using ClassesQuizz;

namespace GstBdd
{
    public class Gst
    {
        // cnx sert � se connecter � la BDD
        private MySqlConnection cnx;
        // cmd sert � �crire nos requ�tes
        private MySqlCommand cmd;
        // dr me permet de r�cup�rer le jeu d'enregistrement(s) de ma requ�te (select)
        private MySqlDataReader dr;

        public Gst()
        {
            // La chaine va nous permettre de donner
            // 1) le nom du serveur
            // 2) le nom de la base de donn�es
            // 3) le nom de l'utilisateur
            // 4) le mot de passe
            string chaine = "Server=localhost;Database=bddquiz;Uid=root;Pwd=";
            cnx = new MySqlConnection(chaine);
            cnx.Open();
        }

        // R�cup�re tous les joueurs existants
        public List<Joueur> GetAllJoueurs()
        {
            List<Joueur> mesJoueurs = new List<Joueur>();

            cmd = new MySqlCommand("SELECT numJoueur,nomJoueur,scoreJoueur FROM joueur", cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Joueur unJoueur = new Joueur(Convert.ToInt16(dr[0].ToString()), dr[1].ToString(), Convert.ToInt16(dr[2].ToString()));
                mesJoueurs.Add(unJoueur);
            }
            dr.Close();
            return mesJoueurs;
        }

        // Ajoute une question dans la base de donn�es
        public void AjouterQuestion(int diff, string libQuestion, string propUn, string propDeux, string propTrois, string propQuatre, string reponseQ)
        {
            try
            {
                string query = "INSERT INTO question (diffQuestion, libQuestion, propUn, propDeux, propTrois, propQuatre, reponseQuestion) " +
                               "VALUES (@diff, @libQuestion, @propUn, @propDeux, @propTrois, @propQuatre, @reponseQ)";

                using (MySqlCommand cmd = new MySqlCommand(query, cnx))
                {
                    cmd.Parameters.AddWithValue("@diff", diff);
                    cmd.Parameters.AddWithValue("@libQuestion", libQuestion);
                    cmd.Parameters.AddWithValue("@propUn", propUn);
                    cmd.Parameters.AddWithValue("@propDeux", propDeux);
                    cmd.Parameters.AddWithValue("@propTrois", propTrois);
                    cmd.Parameters.AddWithValue("@propQuatre", propQuatre);
                    cmd.Parameters.AddWithValue("@reponseQ", reponseQ);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while adding the question: " + ex.Message);
                throw; // Re-lancer l'exception pour la g�rer � un niveau sup�rieur si n�cessaire
            }
        }

        // V�rifie si une question existe d�ja dans la base de donn�es

        public int V�rifierQuestion(string question)
        {
            // Utilisation d'une requ�te param�tr�e pour �viter les injections SQL
            string query = "SELECT COUNT(numquestion) FROM question WHERE libQuestion = @question";
            using (MySqlCommand cmd = new MySqlCommand(query, cnx))
            {
                // Ajout du param�tre pour �viter les injections SQL
                cmd.Parameters.AddWithValue("@question", question);

                // Ex�cution de la commande et lecture du r�sultat
                int count = Convert.ToInt32(cmd.ExecuteScalar());

                // Si count est sup�rieur � 0, cela signifie qu'au moins une ligne a �t� trouv�e
                return count > 0 ? 1 : 0;
            }
        }

        // V�rifie si un pseudo existe d�ja dans la base de donn�es
        public int V�rifierPseudo(string pseudo)
        {
            // Utilisation d'une requ�te param�tr�e pour �viter les injections SQL
            string query = "SELECT COUNT(numJoueur) FROM joueur WHERE nomJoueur = @pseudo";
            using (MySqlCommand cmd = new MySqlCommand(query, cnx))
            {
                // Ajout du param�tre pour �viter les injections SQL
                cmd.Parameters.AddWithValue("@pseudo", pseudo);

                // Ex�cution de la commande et lecture du r�sultat
                int count = Convert.ToInt32(cmd.ExecuteScalar());

                // Si count est sup�rieur � 0, cela signifie qu'au moins une ligne a �t� trouv�e
                return count > 0 ? 1 : 0;
            }
        }

        // Ajoute un pseudo dans la base de donn�es
        public void AjouterPseudo(string pseudo, int score)
        {
            try
            {
                string query = "INSERT INTO joueur (nomJoueur, scoreJoueur) " +
                               "VALUES (@pseudo, @score)";

                using (MySqlCommand cmd = new MySqlCommand(query, cnx))
                {
                    cmd.Parameters.AddWithValue("@pseudo", pseudo);
                    cmd.Parameters.AddWithValue("@score", score);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while adding the question: " + ex.Message);
                throw; // Re-lancer l'exception pour la g�rer � un niveau sup�rieur si n�cessaire
            }
        }

        // R�cup�rer un joueur uniquement

        public List<Joueur> GetJoueur(string unJoueur)
        {
            List<Joueur> leJoueur = new List<Joueur>();

            cmd = new MySqlCommand("SELECT numJoueur,nomJoueur,scoreJoueur FROM joueur where nomJoueur='" + unJoueur + "';", cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Joueur ceJoueur = new Joueur(Convert.ToInt16(dr[0].ToString()), dr[1].ToString(), Convert.ToInt16(dr[2].ToString()));
                leJoueur.Add(ceJoueur);
            }
            dr.Close();
            return leJoueur;
        }

        // R�cup�rer un quiz

        public List<Quiz> GetQuiz(int diff)
        {
            List<Quiz> leQuiz = new List<Quiz>();

            cmd = new MySqlCommand("SELECT DISTINCT questionsquiz.numQuizz FROM questionsquiz INNER JOIN quiz ON questionsquiz.numQuizz = quiz.numQuizz WHERE quiz.diffQuizz =" + diff + " ORDER BY RAND() LIMIT 1;", cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Quiz ceQuiz = new Quiz(Convert.ToInt16(dr[0].ToString()), diff);
                leQuiz.Add(ceQuiz);
            }
            dr.Close();
            return leQuiz;
        }

        // R�cup�rer les questions d'un quiz

        public List<Question> GetQuestionsQuiz(int numQuiz)
        {
            List<Question> lesQuestions = new List<Question>();

            cmd = new MySqlCommand("SELECT libQuestion,propUn,propDeux,propTrois,propQuatre,reponseQuestion FROM questionsquiz INNER JOIN question on questionsquiz.numQuestion = question.numQuestion WHERE numQuizz=" + numQuiz + ";", cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Question cetteQuestion = new Question(0, 0, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());
                lesQuestions.Add(cetteQuestion);
            }
            dr.Close();
            return lesQuestions;
        }

        // Met � jour le score d'un joueur dans la base de donn�es
        public void majJoueur(string pseudo, int score)
        {
            try
            {
                string query = "UPDATE joueur SET scoreJoueur=@score" + " WHERE nomJoueur =@pseudo ;";

                using (MySqlCommand cmd = new MySqlCommand(query, cnx))
                {
                    cmd.Parameters.AddWithValue("@pseudo", pseudo);
                    cmd.Parameters.AddWithValue("@score", score);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while adding the question: " + ex.Message);
                throw; // Re-lancer l'exception pour la g�rer � un niveau sup�rieur si n�cessaire
            }
        }

        // R�cup�re tous les quiz existants
        public List<Quiz> GetAllQuizz()
        {
            List<Quiz> mesQuiz = new List<Quiz>();

            cmd = new MySqlCommand("SELECT numQuizz,diffQuizz FROM quiz;", cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Quiz unQuiz = new Quiz(Convert.ToInt16(dr[0].ToString()), Convert.ToInt16(dr[1].ToString()));
                mesQuiz.Add(unQuiz);
            }
            dr.Close();
            return mesQuiz;
        }

        // R�cup�rer les questions selon leur difficult�

        public List<Question> GetQuestionByDiff(int unediff)
        {
            List<Question> lesQuestions = new List<Question>();

            cmd = new MySqlCommand("SELECT numQuestion,diffQuestion,libQuestion,propUn,propDeux,propTrois,propQuatre,reponseQuestion FROM question WHERE diffQuestion="+unediff, cnx);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Question cetteQuestion = new Question(Convert.ToInt16(dr[0].ToString()),Convert.ToInt16(dr[1].ToString()),dr[2].ToString(),dr[3].ToString(),dr[4].ToString(),dr[5].ToString(),dr[6].ToString(),dr[7].ToString());
                lesQuestions.Add(cetteQuestion);
            }
            dr.Close();
            return lesQuestions;
        }

        // V�rifie si une question est d�ja associ� � un quiz
        public int V�rifierQuestionQuiz(int numQuiz,int numQuestion)
        {
            // Utilisation d'une requ�te param�tr�e pour �viter les injections SQL
            string query = "SELECT COUNT(numQuestion) FROM questionsquiz WHERE numQuizz=@numQuiz AND numQuestion=@numQuestion;";
            using (MySqlCommand cmd = new MySqlCommand(query, cnx))
            {
                // Ajout du param�tre pour �viter les injections SQL
                cmd.Parameters.AddWithValue("@numQuiz", numQuiz);
                cmd.Parameters.AddWithValue("@numQuestion", numQuestion);

                // Ex�cution de la commande et lecture du r�sultat
                int count = Convert.ToInt32(cmd.ExecuteScalar());

                // Si count est sup�rieur � 0, cela signifie qu'au moins une ligne a �t� trouv�e
                return count > 0 ? 1 : 0;
            }
        }

        // Ajouter une question � un quiz
        public void AjouterQuestionQuiz(int numQuiz,int numQuestion)
        {
            try
            {
                string query = "INSERT INTO questionsquiz (numQuizz, numQuestion)" +
                               "VALUES (@numQuiz, @numQuestion)";

                using (MySqlCommand cmd = new MySqlCommand(query, cnx))
                {
                    cmd.Parameters.AddWithValue("@numQuiz", numQuiz);
                    cmd.Parameters.AddWithValue("@numQuestion", numQuestion);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while adding the question: " + ex.Message);
                throw; // Re-lancer l'exception pour la g�rer � un niveau sup�rieur si n�cessaire
            }
        }

    }
}
