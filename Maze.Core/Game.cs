using System.Diagnostics;

namespace Maze.Core
{
    public enum PlayerDirection
    { Left, Right, Up };

    public class PlayerMovedEventArgs : EventArgs
    {
        public PlayerDirection Direction { get; set; }
    }

    public delegate void PlayerMoveDelegte(Game game, PlayerDirection direction);

    //public enum RoomType { Nothing, Trap};

    // Action<Game, PlayerDirection> -> délégé qui retourne void
    // Func<int, bool> -> délégé générique qui retourne le dernier générique (ici bool)

    public class Game
    {
        public Player Player { get; } // read only

        //public event PlayerMoveDelegte? PlayerMoved;
        //public event Action<Game, PlayerDirection>? PlayerMoved;
        //public event EventHandler<PlayerDirection>? PlayerMoved;
        public event EventHandler<PlayerMovedEventArgs>? PlayerMoved;

        public event Action<Game, Trap>? PlayerTakedTrap;
        public event Action<Game, Potion>? PotionFound;

        public Game(string name)
        {
            Player = new Player(name);
        }

        public void Play()
        {
            Random random = new Random();

            switch (random.Next(0, 5))
            {
                case 0: // trap encouter
                    Trap trap = Trap.CreateRandom();
                    Player.TakeDamage(trap.HitPoint);
                    PlayerTakedTrap?.Invoke(this, trap);
                    break;

                    case 1: // Potion
                        Potion potion = Potion.CreateRandom();
                    potion.Affect(Player);
                    PotionFound?.Invoke(this, potion);

                    break;
                default: // nothing happend
                    break;
            }

            if (!Player.IsAlive)
            {
                return;
            }

            switch (random.Next(0, 3))
            {
                case 0: // left
                    //TODO try / catch events, it is foreing code
                    //BP: (Bonne pratique): toujours les ? + try catch
                    PlayerMoved?.Invoke(this, new PlayerMovedEventArgs() { Direction = PlayerDirection.Left });
                    break;

                case 1: // forward
                    PlayerMoved?.Invoke(this, new PlayerMovedEventArgs() { Direction = PlayerDirection.Up });
                    break;

                case 2: // right
                    PlayerMoved?.Invoke(this, new PlayerMovedEventArgs() { Direction = PlayerDirection.Right });
                    break;

                default:
                    Debug.WriteLine($"---- {nameof(Game)} Erreur de tirage");
                    break;
            }
        }
    }
}