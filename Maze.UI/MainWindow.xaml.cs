using Maze.Core;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Maze.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private Game game = new Game("Player");

        public Game CurrentGame
        {
            get { return game; }
            private set { game = value; }
        }
        public Player CurrentPlayer
        {
            get { return CurrentGame.Player; }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        private void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }


        //private string myValue;
        //public string MyValue
        //{
        //    get { return myValue; }
        //    set
        //    {
        //        myValue = value;
        //        RaisePropertyChanged("CurrentPlayer");
        //    }
        //}

        private void Menu_Click_Debug(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                string text = $"{i} / 100 HP";
                tbInfoHp.Text = text;
                Debug.WriteLine(text);
                Thread.Sleep(500);
            }
        }

        private void LaunchMaze()
        {
            string name = "Ben";
            game = new Game(name);
            UpdateInfo(game);

            // Suscribe
            game.PlayerMoved += Game_PlayerMoved;
            game.PlayerTakedTrap += Game_PlayerTakedTrap;
            game.PotionFound += Game_PotionFound;
            game.MonsterEncounter += Game_MonsterEncounter;

            while (game.Player.IsAlive)
            {
                game.Play();
                Thread.Sleep(150);
            }

            // Unsuscribe
            game.PlayerMoved -= Game_PlayerMoved;
            game.PlayerTakedTrap -= Game_PlayerTakedTrap;
            game.MonsterEncounter -= Game_MonsterEncounter;
        }


        private void UpdateInfo(Game game)
        {
            // tbInfoName.Text = $"{game.Player.Name} (lvl 1)";
            tbInfoHp.Text = $"{game.Player.HealthPoint} / 100 HP";
            tbInfoXp.Text = $"{game.Player.Experience} / 1500 XP";
            tbInfoHit.Text = $"{game.Player.AttackPoint} Hit Point";
            tbInfoCC.Text = $"5% Critical Chance";
            RaisePropertyChanged("CurrentPlayer");
            Debug.WriteLine($"Info updated");

        }


        private void Game_MonsterEncounter(Game game, Monster monster)
        {
            Debug.WriteLine($"Vous rencontrez un monstrer {monster.Name}, il vous reste {game.Player.HealthPoint} PV");
            UpdateInfo(game);

        }

        private void Game_PotionFound(Game game, Potion potion)
        {
            Debug.WriteLine($"Vous avez trouvé une {potion.Name}, vous gagnez {potion.Amount}, il vous avez {game.Player.HealthPoint} PV");
            UpdateInfo(game);

        }

        private void Game_PlayerTakedTrap(Game game, Trap trap)
        {
            Debug.WriteLine($"Vous vous êtes pris un {trap.Name}, vous perdez {trap.AttackPoint}, il vous reste {game.Player.HealthPoint} PV");
            UpdateInfo(game);

        }

        private void Game_PlayerMoved(object? sender, PlayerMovedEventArgs args)
        {
            if (sender is Game game)
            {
                Debug.Write($"{game.Player} se dirige ");
                switch (args.Direction)
                {
                    case PlayerDirection.Left:
                        Debug.WriteLine("vers la gauche");
                        break;

                    case PlayerDirection.Right:
                        Debug.WriteLine("vers la droite");
                        break;

                    case PlayerDirection.Up:
                        Debug.WriteLine("tout droit");
                        break;

                    default:
                        Debug.WriteLine($"------- {nameof(MainWindow)}.{Game_PlayerMoved} Erreur de direction");
                        break;
                }
                UpdateInfo(game);
            }

        }

        private void Menu_Click_AutoMode(object sender, RoutedEventArgs e)
        {
            LaunchMaze();
        }
        private void ActionButton_Click_Top(object sender, RoutedEventArgs e)
        {
            LogClick("Top");
        }
        private void ActionButton_Click_Left(object sender, RoutedEventArgs e)
        {
            LogClick("Left");
        }
        private void ActionButton_Click_Right(object sender, RoutedEventArgs e)
        {
            LogClick("Right");
        }

        void LogClick(string context)
        {
            Debug.WriteLine($"\t~~~~~~\t Button clicked : {context} ");
        }



        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    LogClick("Left");
                    break;
                case Key.Right:
                    LogClick("Right");
                    break;
                case Key.Up:
                    LogClick("Top");
                    break;
                case Key.Down:
                    LogClick("Down");
                    break;
                case Key.Enter:
                    LogClick("Enter");
                    break;
                case Key.Escape:
                    LogClick("Escape");
                    App.Current.MainWindow.Close();
                    break;
                default:
                    break;
            }
        }

    }
}