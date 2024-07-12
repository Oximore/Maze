using Maze.Core;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Maze.Cmd
{
    using static Crayon.Output;

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
            game.PotionFound += Game_PotionFound;
            game.MonsterEncounter += Game_MonsterEncounter;

            while (game.Player.IsAlive)
            {
                game.Play();
                Thread.Sleep(150);
            }

            Console.WriteLine("");
            Console.WriteLine($"Vous êtes mort ({game.Player.Experience} xp).");
            Console.WriteLine("The end.");

            game.PlayerMoved -= Game_PlayerMoved; // On doit se désabonner !
            // Sinon pb mémoire
            game.PlayerTakedTrap -= Game_PlayerTakedTrap;
            game.MonsterEncounter -= Game_MonsterEncounter;
        }

        private static void Game_MonsterEncounter(Game game, Monster monster)
        {
            Console.WriteLine(Yellow($"Vous rencontrez un monstrer {Bold(monster.Name)}, il vous reste {game.Player.HealthPoint} PV"));
        }

        private static void Game_PotionFound(Game game, Potion potion)
        {
            Console.WriteLine(Green($"Vous avez trouvé une {Bold(potion.Name)}, vous gagnez {potion.Amount}, il vous avez {game.Player.HealthPoint} PV"));
        }

        private static void Game_PlayerTakedTrap(Game game, Trap trap)
        {
            Console.WriteLine(Red($"Vous vous êtes pris un {Bold(trap.Name)}, vous perdez {trap.AttackPoint}, il vous reste {game.Player.HealthPoint} PV"));
        }

        private static void Game_PlayerMoved(object? sender, PlayerMovedEventArgs args)
        {
            if (sender is Game game)
            {
                Console.Write($"{game.Player} se dirige ");
                switch (args.Direction)
                {
                    case PlayerDirection.Left:
                        Console.WriteLine("vers la gauche");
                        break;

                    case PlayerDirection.Right:
                        Console.WriteLine("vers la droite");
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