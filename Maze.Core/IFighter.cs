using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze.Core
{

    public interface ILiveEntity
    {
        string Name { get; }
        bool IsAlive { get; }

        int HealthPoint { get; }

        void TakeDamage(int hitPoint);
    }

    public interface ICanDamage
    {
        int AttackPoint { get; }

        void Attack(ILiveEntity entity);
    }

    public interface ICanCriticalDamage : ICanDamage
    {
        double CriticalChance { get; }
    }

    public interface IFighter : ILiveEntity, ICanCriticalDamage
    {
    }


    public interface IExperienceAmount
    {
        int ExperienceReward { get; }
    }

    public interface IPlayer : IFighter
    {
        public int MaxHealthPoint { get; }
        public int Experience { get; }
        public void Heal(int amount);
        public void GainExperience(int amount);

    }
}