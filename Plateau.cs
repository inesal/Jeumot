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
        /// <summary>
        /// Constructeur de la classe Plateau avec le nombre spécifié de lignes et de colonnes
        /// La matrice du plateau est créée avec les dimensions saisies puis le plateau est généré de manière aléatoire
        /// </summary>
        /// <param name="lignes">Le nombre de lignes du plateau</param>
        /// <param name="colonnes">Le nombre de colonnes du plateau</param>
        public Plateau(int lignes, int colonnes)
        {
            this.lignes = lignes;
            this.colonnes = colonnes;
            matrice = new char[lignes, colonnes];
            GenererPlateauAleatoire();
        }
        /// <summary>
        /// Constructeur de la classe Plateau à partir d'un fichier spécifique
        /// </summary>
        /// <param name="cheminFichier">Le chemin vers le fichier du plateau saisi</param>
        public Plateau(string cheminFichier)
        {
            ChargerDepuisFichier(cheminFichier);
        }
        /// <summary>
        /// Obtient la grille actuelle du plateau
        /// </summary>
        /// <returns>La grille du plateau avec les lettres actuelles</returns>
        
        public char[,] Matrice
        {
            get { return matrice; }
        }
        /// <summary>
        /// Remplit le plateau avec des lettres générées aléatoirement
        /// </summary>
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
        /// <summary>
        /// Charge la configuration du plateau à partir d'un fichier spécifié
        /// </summary>
        /// <param name="cheminFichier">Le chemin vers le fichier du plateau </param>
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

        /// <summary>
        /// Crée une représentation sous forme de chaîne de caractères du plateau
        /// </summary>
        /// <returns>Une chaîne de caractères représentant le contenu actuel du plateau</returns>

        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < lignes; i++)
            {
                for (int j = 0; j < colonnes; j++)
                {
                    result += matrice[i, j] + " ";
                }
                result += "\n";
            }
            return result;
        }}
        /// <summary>
        /// Recherche le mot sur le plateau en parcourant de bas en haut et en commençant par la colonne de gauche
        /// </summary>
        /// <param name="mot">Le mot à rechercher sur le plateau</param>
        /// <returns>True si le mot est trouvé sinon False</returns>
        public bool Recherche_Mot(string mot)
        {
            
            
                for (int j = 0; j < colonnes; j++)
                {
                    if (Recherche_Mot_Recursif(mot, 0, lignes -1, j)) // commence par le bas vers le haut
                    {
                        return true;
                    }
                 }
            
            return false;
        }
        /// <summary>
        /// Recherche récursivement le mot sur le plateau à partir d'une position donnée
        /// </summary>
        /// <param name="mot">Le mot à rechercher</param>
        /// <param name="motIndex">L'indice actuel dans le mot</param>
        /// <param name="x">La position actuelle en termes de lignes</param>
        /// <param name="y">La position actuelle en termes de colonnes</param>
        /// <returns>True si le mot est trouvé sinon False</returns>
        private bool Recherche_Mot_Recursif(string mot, int motIndex, int x, int y)
        {
            // Vérifie que les conditions d'entrée sont correctes
            if (x < 0 || x >= lignes || y < 0 || y >= colonnes || matrice[x, y] != mot[motIndex])
            {
                return false;
            }

            // si le nombre de lettre correspond à la longueur du mot, on a trouvé
            if (motIndex == mot.Length - 1)
            {
                matrice[x, y] = '#';
                Maj_Plateau(); 

                return true;
            }

            // on marque la case du plateau avec la lettre trouvée
            char temp = matrice[x, y];
            matrice[x, y] = '#';

            // on cherche la lettre suivante dans les cases adjacentes
            bool trouve = Recherche_Mot_Recursif(mot, motIndex + 1, x - 1, y) || // à gauche
                          Recherche_Mot_Recursif(mot, motIndex + 1, x + 1, y) || // à droite
                          Recherche_Mot_Recursif(mot, motIndex + 1, x, y - 1) || // en bas 
                          Recherche_Mot_Recursif(mot, motIndex + 1, x, y + 1) ||  // en haut
                          Recherche_Mot_Recursif(mot, motIndex + 1, x - 1, y + 1) || //en haut à gauche
                          Recherche_Mot_Recursif(mot, motIndex + 1, x + 1, y + 1) || // en haut à droite
                          Recherche_Mot_Recursif(mot, motIndex + 1, x - 1, y - 1) || // en bas à gauche 
                          Recherche_Mot_Recursif(mot, motIndex + 1, x + 1, y - 1); // en bas à droite


            // On remet la valeur des cases à la bonne place
            matrice[x, y] = temp;

            return trouve;
        }
    
        /// <summary>
        /// Sauvegarde la configuration actuelle du plateau dans un fichier 
        /// </summary>
        /// <param name="cheminFichier">Le chemin du fichier où sauvegarder le nouveau plateau</param>
        public void SauvegarderDansFichier(string cheminFichier)
        {
            using (StreamWriter file = new StreamWriter(cheminFichier)) // utiliser une classe du system
            {
                for (int i = 0; i < lignes; i++)
                {
                    for (int j = 0; j < colonnes; j++)
                    {
                        file.Write(matrice[i, j] + (j == colonnes - 1 ? "" : ";"));//ajoute un ; sauf si c'est la dernière colonne,
                                                                                    //auquel cas on n'ajoute rien
                    }
                    file.WriteLine(); //saut de ligne pour passer à la ligne suivante
                }
            }
        }
        /// <summary>
        /// Met à jour le plateau après avoir marqué les cases avec '*' pour dépiler 
        /// </summary>
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
                        matrice[i, j] = (char)('*'); 
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
