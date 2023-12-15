using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Threading;
using System.Runtime.Serialization.Json;
using static System.Formats.Asn1.AsnWriter;

namespace ProjetAlgoMotGliss
{
    internal class Jeu
    {
        private Dictionnaire dictionnaire;
        private Plateau plateau;
        private List<Joueur> joueurs;
        private System.Timers.Timer timerJeu; // peut créer l'ambiguité avec le System.Threading.Timer
        private int joueurActuelIndex;
        private bool jeuEnCours;
        private int tempsTour;


        /// <summary>
        /// constructeur de la classe Jeu
        /// </summary>
        /// <param name="cheminDictionnaire">Le chemin vers le fichier du dictionnaire</param>
        /// <param name="plateau">Le plateau de jeu initial</param>
        public Jeu(string cheminDictionnaire, Plateau plateau  )
        {
            dictionnaire = new Dictionnaire(cheminDictionnaire);
            this.plateau = plateau; // Taille du plateau par défaut, peut être ajustée
            joueurs = new List<Joueur>();
            this.tempsTour = 30; // temps fixe car celui qu'on choisit est celui de la partie. Sinon changer signature
            jeuEnCours = false;
 

            timerJeu = new System.Timers.Timer(tempsTour*1000);
            timerJeu.Elapsed += OnTourTermine; // temps ecoulé

        }
        /// <summary>
        /// Ajoute un nouveau joueur au jeu
        /// </summary>
        /// <param name="nom">Le nom du joueur à ajouter</param>
        public void AjouterJoueur(string nom)
        {
            joueurs.Add(new Joueur(nom));
        }
        /// <summary>
        /// Démarre le jeu en vérifiant le nombre de joueurs et en lancant le premier tour
        /// </summary>
        public void DemarrerJeu()
        {
            if (joueurs.Count < 2)
            {
                Console.WriteLine("Nombre insuffisant de joueurs.");
                return;
            }

            jeuEnCours = true;
            joueurActuelIndex = 0;

            LancerTour();
        }


        /// <summary>
        /// Lance un nouveau tour du jeu en affichant le joueur actuel et le plateau au cours de la partie
        /// </summary>
        private void LancerTour()
        {
            if (jeuEnCours)
            {
                Console.WriteLine($"\nC'est le tour de {joueurs[joueurActuelIndex].Nom}.");

                // Afficher l'état actuel du plateau
                Console.WriteLine("État actuel du plateau :");
                Console.WriteLine(plateau.ToString());

                
                timerJeu.Start(); // Démarrer la minuterie du tour


            }
        }

        /// <summary>
        /// Cette fonction est appelée lorsque le temps imparti pour le tour d'un joueur est écoulé et elle arrete la minuterie puis affiche un message pour montrer que le temps est écoulé et passe au joueur suivant pour lance un nouveau tour
        /// </summary>
        /// <param name="timer">L'objet Timer qui a déclenché l'événement</param>
        /// <param name="timersource">Les info sur la minuterie</param>
        private void OnTourTermine(Object timer, ElapsedEventArgs timersource)
        {
            timerJeu.Stop();

            Console.WriteLine($"Temps écoulé pour {joueurs[joueurActuelIndex].Nom}!");

            joueurActuelIndex = (joueurActuelIndex + 1) % joueurs.Count;

            LancerTour();
        }
        /// <summary>
        /// Vérifie si le mot donné est appartient au dico et au plateau, si c'est le cas , la methode met à jour le score du joueur
        /// </summary>
        /// <param name="mot">Le mot donné par le joueur</param>
        public void SoumettreMot(string mot)
        {
            if (!jeuEnCours)
            {
                Console.WriteLine("Le jeu n'est pas en cours.");
                return;
            }

            Joueur joueurActuel = joueurs[joueurActuelIndex];

            if (plateau.Recherche_Mot(mot) && dictionnaire.RechDichoRecursif(mot))
            {

                if (!joueurActuel.Contient(mot))
                {

                    joueurActuel.Add_Mot(mot);
                    joueurActuel.Add_Score(joueurActuel.CalculerScoreMot(mot));
                    
                    plateau = new Plateau("PlateauFinal.csv");
                    Console.WriteLine(joueurActuel.ToString());

                    // Mettre à jour le score ici en fonction de votre logique de calcul de score

                    Console.WriteLine(" \n Mot accepté, continuez");
                }
                else
                {
                    Console.WriteLine("Mot déjà trouvé par ce joueur.");
                }
            }
            else
            {
                Console.WriteLine("Mot non valide ou non présent sur le plateau.");
            }
        }
        /// <summary>
        /// Affiche les informations détaillées de chaque joueur participant au jeu.
        /// </summary>
        public void AfficherInfosJoueurs()
        {
            foreach (var joueur in joueurs)
            {
                Console.WriteLine(joueur.ToString());
            }
        }
    }
}
