using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Core
{
    public static class GameObjectFactory
    {

        public static Trap CreateTrap()
        {
            int hitPoint = Dice.random.Next(1, 34);
            return new Trap("piège à pointe", hitPoint);
        }

        public static Potion CreatePotion()
        {
            return Dice.random.Next(0, 5) switch
            {
                0 => new ExperiencePotion(),
                1 => new MajorHealthPotion(),
                _ => new MinorHealthPotion(),
            };
        }

        public static Monster CreateMonster()
        {
            return Dice.random.Next(0, 5) switch
            {
                0 => new Dragon(),
                1 or 2 => new Gobelin(),
                _ => new Rat(),
            };
        }

    }
}
