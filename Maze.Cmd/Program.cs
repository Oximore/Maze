using Maze.Core;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Maze.Cmd
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Bonjour, aventurier !");
            Console.WriteLine("Choisissez votre nom : ");
#if DEBUG
            string? name = "Ben";
#else
            string? name = Console.ReadLine();
            // TODO test null or empty
            // string.isNullOrEmpty() ...
#endif

            Game game = new Game(name);
            Console.WriteLine($"Bonjour {name} !");

            game.PlayerMoved += Game_PlayerMoved;
            game.PlayerTakedTrap += Game_PlayerTakedTrap;
            game.PotionFound += Game_PotionFound; ;

            while (game.Player.IsAlive)
            {
                game.Play();
                Thread.Sleep(250);
            }

            Console.WriteLine("");
            Console.WriteLine($"Vous êtes mort ({game.Player.Experience} xp).");
            Console.WriteLine("The end.");

            game.PlayerMoved -= Game_PlayerMoved; // On doit se désabonner !
            // Sinon pb mémoire
            game.PlayerTakedTrap -= Game_PlayerTakedTrap;
        }

        private static void Game_PotionFound(Game game, Potion potion)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Vous avez trouvé une {potion.Name}, vous gagnez {potion.Amount}, il vous avez {game.Player.HealthPoint} hp");
            Console.ResetColor();
        }

        private static void Game_PlayerTakedTrap(Game game, Trap trap)
        {
            Console.WriteLine($"Vous vous êtes pris un {trap.Name}, vous perdez {trap.HitPoint}, il vous reste {game.Player.HealthPoint}");
        }

        private static void Game_PlayerMoved(object? sender, PlayerMovedEventArgs args)
        {
            if (sender is Game game)
            {
                Console.Write($"{game.Player.Name} se diriger vers ");
                switch (args.Direction)
                {
                    case PlayerDirection.Left:
                        Console.WriteLine("la gauche");
                        break;

                    case PlayerDirection.Right:
                        Console.WriteLine("la droite");
                        break;

                    case PlayerDirection.Up:
                        Console.WriteLine("tout droit");
                        break;

                    default:
                        Debug.WriteLine($"------- {nameof(Program)}.{Game_PlayerMoved} Erreur de direction");
                        break;
                }
            }
        }
    }
}