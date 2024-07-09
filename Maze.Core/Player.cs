namespace Maze.Core
{
    public class Player
    {
        private int maxHealthPoint = 100;
        private int healthPoint;

        public string Name { get; } = ""; // Propriété

        public int HealthPoint
        {
            get => healthPoint;
            private set { 
                healthPoint = value; 
                if (healthPoint < 0) healthPoint = 0;
            }
        }

        public bool IsAlive { get => HealthPoint > 0; }

        public int Experience { get; set; } = 0;


        public Player(string name)
        {
            Name = name;
            HealthPoint = maxHealthPoint;
        }

        public void TakeDamage(int hitPoint)
        {
            HealthPoint -= hitPoint;
        }
        public void Heal(int healthPoint)
        {
            HealthPoint += healthPoint;
        }
    }
}