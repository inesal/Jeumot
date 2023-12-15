using System.Timers;

namespace ProjetAlgoMotGliss
{
    internal class Program
    {
        static void Main(string[] args)
        {

            

            Console.WriteLine("Choisissez comment créer le plateau:");
            Console.WriteLine("1. Entrez les dimensions manuellement");
            Console.WriteLine("2. Chargez depuis un fichier");
            

            int choix = int.Parse(Console.ReadLine());
            Plateau plateau = new Plateau(8, 8);
            
            
                Console.Write("Entrez la durée de la partie en minutes : ");
                int dureePartie = int.Parse(Console.ReadLine());

            System.Timers.Timer temps = new System.Timers.Timer(dureePartie * 1000 * 60);
            temps.Elapsed += OnTimedEvent;  
            temps.AutoReset = false;

            switch (choix)
            {
                case 1:
                    // Action pour choix = 1
                    Console.WriteLine("Action pour le choix 1.");
                    break;
                case 2:
                    // Action pour choix = 2
                    Console.WriteLine("Action pour le choix 2.");
                    break;
                case 3:
                    // Action pour choix = 3
                    Console.WriteLine("Action pour le choix 3.");
                    break;
                default:
                    // Action si aucun des choix précédents n'est sélectionné
                    Console.WriteLine("Choix invalide.");
                    break;
            }
            switch (choix)
            {


                case 1:
                    Console.Write("Entrez le nombre de lignes : ");
                    int lignes = int.Parse(Console.ReadLine());

                    Console.Write("Entrez le nombre de colonnes : ");
                    int colonnes = int.Parse(Console.ReadLine());


                    plateau = new Plateau(lignes, colonnes);
                    Console.Clear();
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
                    temps.Start();
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
                    jeu1.AfficherInfosJoueurs();
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
                    temps.Start();
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
                    jeuDeMots.AfficherInfosJoueurs();
                    break;



                case 3:



                    Jeu jeu3 = new Jeu("Mots_Français.txt", plateau);

                    Console.WriteLine("Entrez le nombre de joueurs");
                    int nbjoueurs3 = Convert.ToInt32(Console.ReadLine());
                    int k = 0;
                    while (k < nbjoueurs3)
                    {
                        Console.WriteLine($"Entrez le nom du joueur {k + 1} :");
                        jeu3.AjouterJoueur(Console.ReadLine());
                        k++;
                    }
                    temps.Start();
                    jeu3.DemarrerJeu();
                    while (true)
                    {
                        Console.WriteLine("Entrez un mot ou tapez 'quit' pour quitter:");
                        string input = Console.ReadLine().ToUpper();

                        if (input.Equals("quit", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("Fin du jeu.");
                            break;
                        }

                        jeu3.SoumettreMot(input);
                    }
                    

                    break;
                 

             


                default:
                        Console.WriteLine("Au revoir.");
                        
                        break;


             }

           
            }
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            
            Console.WriteLine("Le temps est écoulé! Fin du jeu.");
            Thread.Sleep(3000);
            Environment.Exit(0);
        }


       
    }
}




