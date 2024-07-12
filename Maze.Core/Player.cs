namespace Maze.Core
{
    public class Player : IPlayer
    {
        public int MaxHealthPoint => 100;
        private int healthPoint;

        public string Name { get; } = "Player"; // Propriété

        public int HealthPoint
        {
            get => healthPoint;
            private set { 
                healthPoint = value; 
                if (healthPoint < 0) healthPoint = 0;
            }
        }

        public bool IsAlive => HealthPoint > 0;

        public int Experience { get; private set; } = 0;
        // private int attackPoint;
        public int AttackPoint { get; private set; }

        public double CriticalChance => 20;

        public Player(string name)
        {
            Name = name;
            HealthPoint = MaxHealthPoint;
            AttackPoint = 15;
        }

        public void TakeDamage(int hitPoint)
        {
            HealthPoint -= hitPoint;
        }

        public void Heal(int amount)
        {
            HealthPoint += amount;
        }

        public void GainExperience(int amount)
        {
            Experience += amount;
        }

        public void Attack(ILiveEntity entity)
        {
            int random = Dice.random.Next(0, 100);
            if (random == 0)
            { }
            else if (random < CriticalChance * 100)
            {
                entity.TakeDamage(2 * AttackPoint);
            }
            else
            {
                entity.TakeDamage(AttackPoint);
            }
        }

        public override string ToString()
        {
            return $"{Name} [{HealthPoint} PV, {Experience} XP]";
        }


        public void AttributsHaveChanged() { }
    }
}