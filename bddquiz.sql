-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Hôte : 127.0.0.1:3306
-- Généré le : ven. 28 juin 2024 à 16:05
-- Version du serveur : 5.7.36
-- Version de PHP : 7.4.26

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `bddquiz`
--

-- --------------------------------------------------------

--
-- Structure de la table `joueur`
--

DROP TABLE IF EXISTS `joueur`;
CREATE TABLE IF NOT EXISTS `joueur` (
  `numJoueur` int(11) NOT NULL AUTO_INCREMENT,
  `nomJoueur` varchar(50) COLLATE utf8_bin NOT NULL,
  `scoreJoueur` int(50) NOT NULL DEFAULT '0',
  PRIMARY KEY (`numJoueur`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

--
-- Déchargement des données de la table `joueur`
--

INSERT INTO `joueur` (`numJoueur`, `nomJoueur`, `scoreJoueur`) VALUES
(2, 'Piero', 4),
(3, 'Marco', 3),
(4, 'Leo', 0);

-- --------------------------------------------------------

--
-- Structure de la table `note`
--

DROP TABLE IF EXISTS `note`;
CREATE TABLE IF NOT EXISTS `note` (
  `numJoueur` int(11) NOT NULL,
  `numQuizz` int(11) NOT NULL,
  `NoteQuizz` int(11) NOT NULL,
  PRIMARY KEY (`numJoueur`,`numQuizz`),
  KEY `numQuizz` (`numQuizz`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

-- --------------------------------------------------------

--
-- Structure de la table `question`
--

DROP TABLE IF EXISTS `question`;
CREATE TABLE IF NOT EXISTS `question` (
  `numQuestion` int(11) NOT NULL AUTO_INCREMENT,
  `diffQuestion` int(50) NOT NULL DEFAULT '0',
  `libQuestion` varchar(100) COLLATE utf8_bin NOT NULL,
  `propUn` varchar(50) COLLATE utf8_bin NOT NULL,
  `propDeux` varchar(50) COLLATE utf8_bin NOT NULL,
  `propTrois` varchar(50) COLLATE utf8_bin NOT NULL,
  `propQuatre` varchar(50) COLLATE utf8_bin NOT NULL,
  `reponseQuestion` varchar(50) COLLATE utf8_bin NOT NULL,
  PRIMARY KEY (`numQuestion`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

--
-- Déchargement des données de la table `question`
--

INSERT INTO `question` (`numQuestion`, `diffQuestion`, `libQuestion`, `propUn`, `propDeux`, `propTrois`, `propQuatre`, `reponseQuestion`) VALUES
(1, 0, 'Quelle est la couleur du ciel par temps clair ?', 'Rouge', 'Bleu', 'Vert', 'Jaune', 'Bleu'),
(2, 0, 'Combien de côtés a un triangle ?', '2', '3', '4', '5', '3'),
(3, 0, 'Quel fruit est connu pour être jaune et courbé ?', 'Pomme', 'Banane', 'Orange', 'Fraise', 'Banane'),
(7, 0, 'Quel animal est le meilleur ami de l\'homme ?', 'Chat', 'Chien', 'Oiseau', 'Poisson', 'Chien'),
(8, 0, 'Combien y a-t-il de continents sur Terre ?', '5', '6', '7', '8', '7'),
(9, 1, 'Quelle est la capitale de la France ?', 'Rome', 'Madrid', 'Paris', 'Berlin', 'Paris'),
(10, 0, 'De quel couleur est la mer ?', 'Vert', 'Orange', 'Bleu', 'Rouge', 'Bleu'),
(11, 0, 'De quel couleur sont les nuages ?', 'Blanc', 'Vert', 'Rouge', 'Bleu', 'Blanc'),
(12, 1, 'Qui a écrit \"Les Misérables\" ?', 'Gustave Flaubert', 'Victor Hugo', 'Émile Zola', 'Honoré de Balzac', 'Victor Hugo'),
(13, 1, 'Quelle est la planète la plus proche du Soleil ?', 'Vénus', 'Mars', 'Mercure', 'Terre', 'Mercure'),
(14, 1, 'Quel est l\'élément chimique représenté par le symbole \"O\" ?', 'Or', 'Oxygène', 'Osmium', 'Hydrogène', 'Oxygène');

-- --------------------------------------------------------

--
-- Structure de la table `questionsquiz`
--

DROP TABLE IF EXISTS `questionsquiz`;
CREATE TABLE IF NOT EXISTS `questionsquiz` (
  `numQuizz` int(11) NOT NULL,
  `numQuestion` int(11) NOT NULL,
  PRIMARY KEY (`numQuizz`,`numQuestion`),
  KEY `numQuestion` (`numQuestion`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

--
-- Déchargement des données de la table `questionsquiz`
--

INSERT INTO `questionsquiz` (`numQuizz`, `numQuestion`) VALUES
(1, 1),
(1, 2),
(1, 3),
(1, 7),
(1, 8),
(2, 9),
(1, 10),
(1, 11),
(2, 12),
(2, 13);

--
-- Déclencheurs `questionsquiz`
--
DROP TRIGGER IF EXISTS `BeforeInsertQuestionsQuizz`;
DELIMITER $$
CREATE TRIGGER `BeforeInsertQuestionsQuizz` BEFORE INSERT ON `questionsquiz` FOR EACH ROW BEGIN
    DECLARE v_difficulte_quizz INT;
    DECLARE v_difficulte_question INT;

    -- Obtenir la difficulté du quizz
    SELECT diffQuizz INTO v_difficulte_quizz
    FROM quiz
    WHERE numQuizz = NEW.numQuizz;

    -- Obtenir la difficulté de la question
    SELECT diffQuestion INTO v_difficulte_question
    FROM question
    WHERE numQuestion = NEW.numQuestion;

    -- Vérifier que les difficultés correspondent
    IF v_difficulte_quizz != v_difficulte_question THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'La difficulté de la question ne correspond pas à la difficulté du quizz';
    END IF;
END
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Structure de la table `quiz`
--

DROP TABLE IF EXISTS `quiz`;
CREATE TABLE IF NOT EXISTS `quiz` (
  `numQuizz` int(11) NOT NULL AUTO_INCREMENT,
  `nomQuizz` varchar(50) COLLATE utf8_bin DEFAULT NULL,
  `diffQuizz` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`numQuizz`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COLLATE=utf8_bin;

--
-- Déchargement des données de la table `quiz`
--

INSERT INTO `quiz` (`numQuizz`, `nomQuizz`, `diffQuizz`) VALUES
(1, 'Quiz de Piero', 0),
(2, 'Quizz2', 1);

--
-- Contraintes pour les tables déchargées
--

--
-- Contraintes pour la table `note`
--
ALTER TABLE `note`
  ADD CONSTRAINT `note_ibfk_1` FOREIGN KEY (`numJoueur`) REFERENCES `joueur` (`numJoueur`),
  ADD CONSTRAINT `note_ibfk_2` FOREIGN KEY (`numQuizz`) REFERENCES `quiz` (`numQuizz`);

--
-- Contraintes pour la table `questionsquiz`
--
ALTER TABLE `questionsquiz`
  ADD CONSTRAINT `questionsquiz_ibfk_1` FOREIGN KEY (`numQuestion`) REFERENCES `question` (`numQuestion`),
  ADD CONSTRAINT `questionsquiz_ibfk_2` FOREIGN KEY (`numQuizz`) REFERENCES `quiz` (`numQuizz`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
