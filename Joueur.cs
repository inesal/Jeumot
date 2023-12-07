using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jeu
{
    internal class Joueur
    {
        //attributs
        string nom;
        string prenom;
        List<string> motsTrouves;
        int nbPlateau;
        int score;

        //constructeur
        public Joueur (string nom,string prenom,List<string> motsTrouves, int nbPlateau, int score)
        {
            if(nom!=null && nom.Length!=0)
            {
                this.nom = nom;
                this.prenom = prenom;
                this.motsTrouves = null;
                this.nbPlateau = 0;
                this.score = 0;
            }
            else
            {
                Console.WriteLine("Le joueur n'a pas de nom");
            }
        }

        //toString
        public void toString()
        {
            Console.WriteLine("Le joueur " + this.prenom + " " + this.nom+
                "\n a trouvé les mots suivants");
            AfficherListe(motsTrouves);
            Console.WriteLine("Il a effectué " + this.nbPlateau + " parties" +
                "\n et son score est actuellement de " + this.score);
        }

        public void AfficherListe(List<string> listeMot)
        {
            foreach(string mot in listeMot)
            {
                Console.WriteLine(mot);
            }
            return;
        }

        //Propriétés
        public List<string> MotsTrouves
        {
            get { return motsTrouves; }
            set { motsTrouves = value; }
        }
        public int NbPlateau
        {
            get { return nbPlateau; }
            set { nbPlateau = value; }
        }
        public int Score
        {
            get { return score; }
            set { score = value; }
        }
        public string Prenom
        {
            get { return prenom; }
        }
        public string Nom
        {
            get { return nom; }
        }


        public void Add_Mot(string mot)
        {
            motsTrouves.Add(mot);
        }

        public void Add_Score(int val)
        {
            score += val;
        }

        public bool Contient(string mot)
        {
            foreach(string element in motsTrouves)
            {
                if(element==mot)
                {
                    Console.WriteLine("Vous avez déjà utilisé ce mot");
                    return true;
                }
            }
            return false;
        }
    }
}
