namespace Maze.Core
{
    public class Trap
    {
        public static Trap CreateRandom()
        {
            Random random = new Random();
            int hitPoint = random.Next(1, 34);

            return new Trap("piège à pointe", hitPoint);
        }

        public string Name { get; private set; } = "";
        public int HitPoint { get; private set; }

        public Trap(string name, int hitPoint)
        {
            Name = name;
            HitPoint = hitPoint;
        }
    }
}