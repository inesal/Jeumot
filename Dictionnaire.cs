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

        /// <summary>
        /// Constructeur de la classe Dictionnaire à partir d'un fichier contenant ees mots qui sont trié par la fonction quicksort
        /// </summary>
        /// <param name="cheminFichier">Le chemin vers le fichier contenant les mots du dictionnaire</param>
        public Dictionnaire(string cheminFichier)
        {
            mots = new List<string>();
            ChargerMots(cheminFichier);
            Tri_quicksort(0, mots.Count - 1);
        }
        /// <summary>
        /// Charge les mots à partir d'un fichier spécifié et les ajoute à la liste de mots du dictionnaire
        /// </summary>
        /// <param name="cheminFichier">Le chemin vers le fichier contenant les mots du dictionnaire</param>
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
        /// <summary>
        ///Tostring override
        /// </summary>
        /// <returns>retourne le nombre de mots du dictionnaire et le notre de mots par lettre du dico</returns>
        public override string ToString()
        {
            return $"Dictionnaire contenant {mots.Count} mots.";
        }
        /// <summary>
        /// Recherche récursivement le mot dans le dictionnaire en utilisant la donction recurisve private recherche dichotomique
        /// </summary>
        /// <param name="mot">Le mot à rechercher dans le dictionnaire</param>
        /// <returns>True si le mot est trouvé, sinon False</returns>
        public bool RechDichoRecursif(string mot)
        {
            return RechDichoRecursif(mot, 0, mots.Count - 1); //état d'entré dans la fonction récursive
        }


        /// <summary>
        /// Recherche récursivement le mot dans le dictionnaire
        /// </summary>
        /// <param name="mot">Le mot à rechercher dans le dictionnaire.</param>
        /// <param name="deb">L'indice de début</param>
        /// <param name="fin"> </param>
        /// <returns>True si le mot est trouvé, sinon False</returns>
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
        /// <summary>
        /// Trie récursivement une portion du dictionnaire 
        /// </summary>
        /// <param name="debut">L'indice de début de la portion à trier</param>
        /// <param name="fin">L'indice de fin de la portion à trier</param>
        private void Tri_quicksort(int debut, int fin)
        {
            if (debut < fin)
            {
                int pivotIndex = Partition(debut, fin);
                Tri_quicksort(debut, pivotIndex - 1);
                Tri_quicksort(pivotIndex + 1, fin);
            }
        }
        /// <summary>
        /// permet de trier  le ditionnaire à droite et a gauche d'un pivotd'un pivot
        /// </summary>
        /// <param name="debut"></param>
        /// <param name="fin"></param>
        /// <returns>L'indice du pivot après la partition</returns>
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
        /// <summary>
        /// Échange les positions de deux éléments
        /// </summary>
        /// <param name="i">L'indice du premier élément à échanger</param>
        /// <param name="j">L'indice du deuxième élément à échanger</param>
        private void Echanger(int i, int j)
        {
            var temp = mots[i];
            mots[i] = mots[j];
            mots[j] = temp;
        }
    }
}
