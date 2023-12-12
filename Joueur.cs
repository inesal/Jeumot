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
        public void Add_Mot(string mot)
        {
            if (!Contient(mot))
            {
                motsTrouves.Add(mot);
                // Vous pouvez également mettre à jour le score ici si nécessaire,
                // par exemple en fonction de la longueur du mot.
            }
        }


        public int CalculerScoreMot(string mot)
        {
            
            Dictionary<char, int> val = new Dictionary<char, int>
        {
            {'A', 1 }, {'B', 2} , {'C' , 4} , {'D' , 5} , { 'E' ,  6} , {'F' , 9} , { 'G' , 2} , {'H' , 1} , {'I' , 3} , {'J' , 1} , {'K', 3} , { 'L' , 2 } , {'M' , 2 } , {'O' , 6} , {'P' , 8} , {'Q' , 2} ,{ 'R' , 2} , { 'S' , 4} , {'T' , 6} , { 'U' , 1},{'V' , 2} , {'W' , 10} , {'X' , 1} , { 'Y' , 7} , {'Z' , 10}

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
                    Console.WriteLine(scoreMot);
                }
                // Vous pouvez également choisir de gérer le cas où la lettre n'est pas présente dans le dictionnaire
            }
            return scoreMot;
            
        }

        public void Add_Score( int val)
        {
            score += val;

        }
        
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
