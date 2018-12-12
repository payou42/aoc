using System;

namespace Aoc.Common.Rpg
{  
    public abstract class Spell
    {
        public string Name { get; set; }

        public int Cost { get; set; }

        public virtual void Apply(Character me, Character other)
        {
            me.Mana -= Cost;
        }

        public virtual bool IsCastable(Character me, Character other)
        {
            return me.Mana >= Cost;
        }
    }
}