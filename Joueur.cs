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

            }
        }

        public void Add_Score(int val)
        {
            Score += val;
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
