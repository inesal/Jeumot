using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetAlgoMotGliss
{
    internal class Joueur
    {

        //attributs
        string nom;
        List<string> motsTrouves;
        int score;

        //propriétés
        public string Nom
        {
            get { return nom; }
            set { nom = value; } //ça sert à quoi de donner accès en écriture pour le nom ?
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        //constructeur

        /// <summary>
        /// Constricteur de la classe joueur avec un nom donné
        /// </summary>
        /// <param name="nom">Le nom du joueur</param>
        public Joueur(string nom)
        {
            if (nom != null && nom.Length != 0)
            {
                Nom = nom;
                motsTrouves = new List<string>();
                Score = 0;
            }
            else
            {
                Console.WriteLine("Le joueur n'a pas de nom");
            }
        }
        /// <summary>
        /// Ajoute un mot à la liste des mots trouvés du joueur s'il n'est pas déjà présent
        /// </summary>
        /// <param name="mot">Le mot à ajouter</param>
        public void Add_Mot(string mot)
        {
            if (!Contient(mot))
            {
                motsTrouves.Add(mot);
                // Vous pouvez également mettre à jour le score ici si nécessaire,
                // par exemple en fonction de la longueur du mot.
            }
        }

        /// <summary>
        /// Calcule le score d'un mot en utilisant un ensemble de valeurs attribuées à chaque lettre
        /// </summary>
        /// <param name="mot">Le mot pour lequel calculer le score</param>
        /// <returns>Le score du mot</returns>
        public int CalculerScoreMot(string mot)
        {
            
            Dictionary<char, int> val = new Dictionary<char, int>
        {
            {'A', 1 }, {'B', 2} , {'C' , 4} , {'D' , 5} , { 'E' ,  6} , {'F' , 9} , { 'G' , 2} , {'H' , 1} , {'I' , 3} , {'J' , 1} , {'K', 3} , { 'L' , 2 } , {'M' , 2 } ,{'N',1 }, {'O' , 6} , {'P' , 8} , {'Q' , 2} ,{ 'R' , 2} , { 'S' , 4} , {'T' , 6} , { 'U' , 1},{'V' , 2} , {'W' , 10} , {'X' , 1} , { 'Y' , 7} , {'Z' , 10}

        };
            int scoreMot = 0;

            // Utiliser un foreach pour parcourir chaque lettre du mot
            foreach (char lettre in mot)
            {
                // Vérifier si la lettre est présente dans le dictionnaire des scores
                if (val.ContainsKey(lettre))
                {
                    // Ajouter le score de la lettre au score total du mot
                    scoreMot += val.GetValueOrDefault(lettre);
                    //Console.WriteLine(scoreMot);
                }
                // Vous pouvez également choisir de gérer le cas où la lettre n'est pas présente dans le dictionnaire
            }
            return scoreMot;
            
        }
        /// <summary>
        /// Ajoute un score spécifié au score existant du joueur
        /// </summary>
        /// <param name="val">La valeur à ajouter au score</param>
        public void Add_Score( int val)
        {
            score += val;

        }
        /// <summary>
        /// Vérifie si le joueur a déjà utilisé un mot
        /// </summary>
        /// <param name="mot">Le mot à vérifier</param>
        /// <returns>True si le mot a déjà été utilisé sinon False</returns>
        public bool Contient(string mot)
        {
            foreach (string element in motsTrouves)
            {
                if (element == mot)
                {
                    Console.WriteLine("Vous avez déjà utilisé ce mot");
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Retourne les informations du joueur
        /// </summary>
        /// <returns>Une chaîne de caractères</returns>
        public override string ToString()
        {
            string result = "";
            result += $"Nom: {Nom}\n";
            result += $"Score: {Score}\n";
            result += "Mots trouvés: ";

            foreach (var mot in motsTrouves)
            {
                result += mot + ", ";
            }
            return result.Substring(0, result.Length - 2);
        }
    }

}
