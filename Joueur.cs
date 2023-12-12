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
            {'a', 1 }, {'b', 2} , {'c' , 4} , {'d' , 5} , { 'e' ,  6} , {'f' , 9} , { 'g' , 2} , {'h' , 1} , {'i' , 3} , {'j' , 1} , {'k', 3} , { 'l' , 2 } , {'m' , 2 } , {'o' , 6} , {'p' , 8} , {'q' , 2} ,{ 'r' , 2} , { 's' , 4} , {'t' , 6} , { 'u' , 1},{'v' , 2} , {'w' , 10} , {'x' , 1} , { 'y' , 7} , {'z' , 10}

        };
            int scoreMot = 0;

            // Utiliser un foreach pour parcourir chaque lettre du mot
            foreach (char lettre in mot)
            {
                // Vérifier si la lettre est présente dans le dictionnaire des scores
                if (val.ContainsKey(lettre))
                {
                    // Ajouter le score de la lettre au score total du mot
                    scoreMot += val[lettre];
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
