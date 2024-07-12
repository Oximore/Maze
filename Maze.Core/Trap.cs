namespace Maze.Core
{
    public class Trap : ICanDamage
    {
        public string Name { get; private set; } = "";
        public int AttackPoint { get; private set; }
                

        internal Trap(string name, int hitPoint)
        {
            Name = name;
            AttackPoint = hitPoint;
        }

        public void Attack(ILiveEntity entity)
        {
            entity.TakeDamage(AttackPoint);
        }
    }
}