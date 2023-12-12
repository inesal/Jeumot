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
        private System.Timers.Timer timerJeu;
        private int tempsTour; // Temps par tour en secondes
        private int joueurActuelIndex;
        private bool jeuEnCours;
        private Thread chronometre;

        public Jeu(string cheminDictionnaire, Plateau plateau , int tempsTour = 30 )
        {
            dictionnaire = new Dictionnaire(cheminDictionnaire);
            this.plateau = plateau; // Taille du plateau par défaut, peut être ajustée
            joueurs = new List<Joueur>();
            this.tempsTour = tempsTour;
            jeuEnCours = false;

            timerJeu = new System.Timers.Timer(tempsTour * 1000);
            timerJeu.Elapsed += OnTourTermine; // temps ecoulé

        }

        public void AjouterJoueur(string nom)
        {
            joueurs.Add(new Joueur(nom));
        }

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

        private void ChronometreThread()
        {
            while (jeuEnCours)
            {
               // Console.Write($"\rTemps restant : {tempsTour} secondes");
                Thread.Sleep(1000); // Attendre 1 seconde
                tempsTour--;


                if (tempsTour <= 0)
                {
                    // Si le temps est écoulé, arrêter le tour
                    OnTourTermine(null, null);
                    break;
                }
            }
            this.tempsTour = 30;
        }

        private void LancerTour()
        {
            if (!jeuEnCours) return;
            chronometre = new Thread(ChronometreThread);

            
            Console.WriteLine($"\n C'est le tour de {joueurs[joueurActuelIndex].Nom}.");
            
            // Afficher l'état actuel du plateau
            Console.WriteLine("État actuel du plateau :");
            Console.WriteLine(plateau.ToString());
            chronometre.Start();
            timerJeu.Start();
        }


        private void OnTourTermine(Object timer, ElapsedEventArgs timersource)
        {
            timerJeu.Stop();
            Console.WriteLine($"\n Temps écoulé pour {joueurs[joueurActuelIndex].Nom}!");

            joueurActuelIndex = (joueurActuelIndex + 1) % joueurs.Count;

            LancerTour();
        }

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
    }
}
