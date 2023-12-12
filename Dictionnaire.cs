using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetAlgoMotGliss
{
    internal class Dictionnaire
    {

        private List<string> mots;

        public Dictionnaire(string cheminFichier)
        {
            mots = new List<string>();
            ChargerMots(cheminFichier);
            Tri_quicksort(0, mots.Count - 1);
        }

        private void ChargerMots(string cheminFichier)
        {
            try
            {
                string contenuFichier = File.ReadAllText(cheminFichier);
                mots = contenuFichier.Split(' ').Select(m => m.Trim()).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erreur lors du chargement du dictionnaire : {e.Message}");
            }
        }

        public override string ToString()
        {
            return $"Dictionnaire contenant {mots.Count} mots.";
        }

        public bool RechDichoRecursif(string mot)
        {
            return RechDichoRecursif(mot, 0, mots.Count - 1); //état d'entré dans la fonction récursive
        }



        private bool RechDichoRecursif(string mot, int debut, int fin)
        {

            if (debut > fin)
            {
                return false;
            }

            int milieu = (debut + fin) / 2;

            if (mots[milieu].Equals(mot, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (string.Compare(mot, mots[milieu], StringComparison.OrdinalIgnoreCase) < 0)
            {
                return RechDichoRecursif(mot, debut, milieu - 1);
            }
            else
            {
                return RechDichoRecursif(mot, milieu + 1, fin);
            }
        }

        private void Tri_quicksort(int debut, int fin)
        {
            if (debut < fin)
            {
                int pivotIndex = Partition(debut, fin);
                Tri_quicksort(debut, pivotIndex - 1);
                Tri_quicksort(pivotIndex + 1, fin);
            }
        }

        private int Partition(int debut, int fin)
        {
            string pivot = mots[fin];
            int i = debut - 1;

            for (int j = debut; j < fin; j++)
            {
                if (string.Compare(mots[j], pivot, StringComparison.OrdinalIgnoreCase) <= 0)
                {
                    i++;
                    Echanger(i, j);
                }
            }

            Echanger(i + 1, fin);
            return i + 1;
        }

        private void Echanger(int i, int j)
        {
            var temp = mots[i];
            mots[i] = mots[j];
            mots[j] = temp;
        }
    }
}
