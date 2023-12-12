namespace ProjetAlgoMotGliss
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
          
           
            

                /*Console.WriteLine("Entrez le palteau désiré, soit un nom de fichier ex: Test1.csv, soit un couple de int ex (8,8)");
                Plateau plateau =new Plateau(Convert.ToString(Console.ReadLine()));*/
                Console.WriteLine("Choisissez comment créer le plateau:");
                Console.WriteLine("1. Entrez les dimensions manuellement");
                Console.WriteLine("2. Chargez depuis un fichier");
                Console.WriteLine("3. Quitter");
                
                
          
            int choix = int.Parse(Console.ReadLine());
            Plateau plateau = new Plateau(8, 8);

            switch (choix)
                {
                    
                        
                    case 1:
                        Console.Write("Entrez le nombre de lignes : ");
                        int lignes = int.Parse(Console.ReadLine());

                        Console.Write("Entrez le nombre de colonnes : ");
                        int colonnes = int.Parse(Console.ReadLine());


                        plateau = new Plateau(lignes, colonnes);

                    Jeu jeu1 = new Jeu("Mots_Français.txt", plateau);



                    Console.WriteLine("Entrez le nombre de joueurs");
                    int nbjoueurs1 = Convert.ToInt32(Console.ReadLine());
                    int i = 0;
                    while (i < nbjoueurs1)
                    {
                        Console.WriteLine($"Entrez le nom du joueur {i + 1} :");
                        jeu1.AjouterJoueur(Console.ReadLine());
                        i++;
                    }

                    // Démarrer le jeu
                    jeu1.DemarrerJeu();

                    while (true)
                    {
                        Console.WriteLine("Entrez un mot ou tapez 'quit' pour quitter:");
                        string input = Console.ReadLine().ToUpper();

                        if (input.Equals("quit", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("Fin du jeu.");
                            break;
                        }

                        jeu1.SoumettreMot(input);
                    }
                    break;


                    case 2:
                        Console.Write("Entrez le nom du fichier : ");
                        string cheminFichier = Console.ReadLine();
                        plateau = new Plateau(cheminFichier);
                        Jeu jeuDeMots = new Jeu("Mots_Français.txt", plateau);



                    Console.WriteLine("Entrez le nombre de joueurs");
                    int nbjoueurs = Convert.ToInt32(Console.ReadLine());
                    int j = 0;
                    while (j < nbjoueurs)
                    {
                        Console.WriteLine($"Entrez le nom du joueur {j + 1} :");
                        jeuDeMots.AjouterJoueur(Console.ReadLine());
                        j++;
                    }

                    // Démarrer le jeu
                    jeuDeMots.DemarrerJeu();

                    while (true)
                    {
                        Console.WriteLine("Entrez un mot ou tapez 'quit' pour quitter:");
                        string input = Console.ReadLine().ToUpper();

                        if (input.Equals("quit", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("Fin du jeu.");
                            break;
                        }

                        jeuDeMots.SoumettreMot(input);
                    }
                    break;

                    case 3:

                  
                        Console.WriteLine("Fin du jeu.");

                    break;

                 


                    default:
                        Console.WriteLine("Choix invalide, création du plateau avec des dimensions par défaut.");
                        plateau = new Plateau(8, 8); // Ou toute autre valeur par défaut
                        break;
                }


            




        }
    }
}
