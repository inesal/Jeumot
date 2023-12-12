using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjetAlgoMotGliss
{
    internal class Plateau
    {


        private char[,] matrice;
        private int lignes;
        private int colonnes;

        public Plateau(int lignes, int colonnes)
        {
            this.lignes = lignes;
            this.colonnes = colonnes;
            matrice = new char[lignes, colonnes];
            GenererPlateauAleatoire();
        }

        public Plateau(string cheminFichier)
        {
            ChargerDepuisFichier(cheminFichier);
        }
        public char[,] GetMatrice()
        {
            return matrice;
        }

        private void GenererPlateauAleatoire()
        {
            Random rand = new Random();
            for (int i = 0; i < lignes; i++)
            {
                for (int j = 0; j < colonnes; j++)
                {
                    matrice[i, j] = (char)('A' + rand.Next(26)); // Génère une lettre aléatoire
                }
            }
        }

        private void ChargerDepuisFichier(string cheminFichier)
        {
            string[] lignesFichier = File.ReadAllLines(cheminFichier);

            // Vérifiez si le fichier est vide
            if (lignesFichier.Length == 0)
            {
                throw new InvalidOperationException("Le fichier est vide."); // throw créer une erreur
            }

            // Nettoyer les lignes et construire la matrice
            List<string> lignesNettoyees = new List<string>();
            foreach (var ligne in lignesFichier)
            {
                string ligneNettoyee = ligne.Replace(" ", "").Replace(";", "");// remplace m ; a ; i par m;a;i
                lignesNettoyees.Add(ligneNettoyee);
            }

            this.lignes = lignesNettoyees.Count;
            this.colonnes = lignesNettoyees[0].Length;

            // Vérifiez si toutes les lignes ont la même longueur
            foreach (var ligne in lignesNettoyees)
            {
                if (ligne.Length != this.colonnes)
                {
                    throw new InvalidOperationException("Les lignes du fichier n'ont pas la même longueur après le nettoyage."); // throw 
                }
            }

            matrice = new char[this.lignes, this.colonnes];

            for (int i = 0; i < this.lignes; i++)
            {
                for (int j = 0; j < this.colonnes; j++)
                {
                    matrice[i, j] = char.ToUpper(lignesNettoyees[i][j]);
                }
            }
        }



        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();             // changer streambuilder 
            for (int i = 0; i < lignes; i++)
            {
                for (int j = 0; j < colonnes; j++)
                {
                    sb.Append(matrice[i, j] + " ");
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public bool Recherche_Mot(string mot)
        {
            // Start from the bottom row and search upwards
            
                for (int j = 0; j < colonnes; j++)
                {
                    if (Recherche_Mot_Recursif(mot, 0, lignes -1, j))
                    {
                        return true;
                    }
               }
            
            return false;
        }

        private bool Recherche_Mot_Recursif(string mot, int motIndex, int x, int y)
        {
            // Check if out of bounds or the cell doesn't match the current letter in the word.
            if (x < 0 || x >= lignes || y < 0 || y >= colonnes || matrice[x, y] != mot[motIndex])
            {
                return false;
            }

            // If the whole word is found.
            if (motIndex == mot.Length - 1)
            {
                matrice[x, y] = '#';
                Maj_Plateau(); 

                return true;
            }

            // Mark the current cell as visited.
            char temp = matrice[x, y];
            matrice[x, y] = '#';

            // Check all four directions.
            bool found = Recherche_Mot_Recursif(mot, motIndex + 1, x - 1, y) || // à gauche
                          Recherche_Mot_Recursif(mot, motIndex + 1, x + 1, y) || // à droite
                          Recherche_Mot_Recursif(mot, motIndex + 1, x, y - 1) || // en bas 
                          Recherche_Mot_Recursif(mot, motIndex + 1, x, y + 1) ||  // en haut
                          Recherche_Mot_Recursif(mot, motIndex + 1, x - 1, y + 1) || //en haut à gauche
                          Recherche_Mot_Recursif(mot, motIndex + 1, x + 1, y + 1) || // en haut à droite
                          Recherche_Mot_Recursif(mot, motIndex + 1, x - 1, y - 1) || // en bas à gauche 
                          Recherche_Mot_Recursif(mot, motIndex + 1, x + 1, y - 1); // en bas à droite


            // Unmark the cell before backtracking.
            matrice[x, y] = temp;

            return found;
        }

        public void SauvegarderDansFichier(string cheminFichier)
        {
            using (StreamWriter file = new StreamWriter(cheminFichier)) // utiliser une classe du system
            {
                for (int i = 0; i < lignes; i++)
                {
                    for (int j = 0; j < colonnes; j++)
                    {
                        file.Write(matrice[i, j] + (j == colonnes - 1 ? "" : ";"));
                    }
                    file.WriteLine();
                }
            }
        }
        public void Maj_Plateau()
        {
            Random rand = new Random();
            for (int j = 0; j < colonnes; j++)
            {
                int count = 0;
                for (int i = lignes - 1; i >= 0; i--)
                {
                    if (matrice[i, j] == '#')
                    {
                        count++;
                    }
                    else if (count > 0)
                    {
                        matrice[i + count, j] = matrice[i, j];
                        matrice[i, j] = (char)('*'); // Nouvelle lettre aléatoire
                    }
                }
            }
            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                for (int j = 0; j < matrice.GetLength(1); j++)
                {
                    Console.Write(matrice[i, j] + " ");
                }
                Console.WriteLine(); // Passer à la ligne suivante
            }

            SauvegarderDansFichier("PlateauFinal.csv");
        }
    }
}
