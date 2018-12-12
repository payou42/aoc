using System;

namespace Aoc.Common.Rpg
{
    public abstract class Effect
    {
        public string Name { get; set; }

        public int Duration { get; set; }

        public int Remaining { get; set; }

        public int Health { get; set; }

        public int Armor { get; set; }

        public int Mana { get; set; }

        public virtual void Init(Character me)
        {            
        }

        public virtual void Apply(Character me)
        {
            me.Health += Health;
            me.Mana += Mana;
            Remaining -= 1;
        }

        public virtual void Clean(Character me)
        {            
        }

        public abstract Effect Clone();
    }
}