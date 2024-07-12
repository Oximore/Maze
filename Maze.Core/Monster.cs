namespace Maze.Core
{
    public abstract class Monster : IFighter, IExperienceAmount
    {
        //public string Name { get; private set; }
        protected abstract int InitialHealthPoint { get; }

        public int HealthPoint { get; private set; }
        //public int AttackPoint { get; private set;  }

        public bool IsAlive => HealthPoint > 0;

        public abstract string Name { get; }
        public abstract double CriticalChance { get; }
        public abstract int AttackPoint { get; }
        public abstract int ExperienceReward { get; }

        protected Monster()
        { HealthPoint = InitialHealthPoint; }

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

        public void TakeDamage(int hitPoint)
        {
            HealthPoint -= hitPoint;
        }
    }

    public class Rat : Monster
    {
        public override string Name => "Rat";
        protected override int InitialHealthPoint => 10;
        public override int AttackPoint => 3;
        public override double CriticalChance => 1;
        public override int ExperienceReward => 50;
    }

    public class Gobelin : Monster
    {
        public override string Name => "Gobelin";
        protected override int InitialHealthPoint => 10;
        public override int AttackPoint => 5;
        public override double CriticalChance => 5;
        public override int ExperienceReward => 100;
    }
    public class Dragon : Monster
    {
        public override string Name => "Dragon";
        protected override int InitialHealthPoint => 50;
        public override int AttackPoint => 20;
        public override double CriticalChance => 15;
        public override int ExperienceReward => 1000;
    }
}