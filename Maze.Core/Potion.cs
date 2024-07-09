using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Core
{
    public class Potion
    {
        public static Potion CreateRandom()
        {
            Random random = new Random();
            return random.Next(0, 5) switch
            {
                0 => new ExperiencePotion(),
                1 => new MajorHealthPotion(),
                _ => new MinorHealthPotion(),
            };
        }

        public string Name { get; private set; } = "";
        public int Amount { get; private set; }

        protected Potion(string name, int healthPoint)
        {
            Name = name;
            Amount = healthPoint;
        }

        public virtual void Affect(Player player) { }
    }

    public class MinorHealthPotion : Potion
    {
        public MinorHealthPotion() : base("petite potion de soin", 5) { }
        public override void Affect(Player player) { 
            base.Affect(player);
            player.Heal(Amount);
        }
    }
    public class MajorHealthPotion : Potion
    {
        public MajorHealthPotion() : base("petite potion de soin", 25) { }
        public override void Affect(Player player)
        {
            base.Affect(player);
            player.Heal(Amount);
        }
    }
    public class ExperiencePotion : Potion
    {
        public ExperiencePotion() : base("potion d'expérience", 25) { }
        public override void Affect(Player player)
        {
            base.Affect(player);
            player.Experience += Amount;
        }
    }
}
