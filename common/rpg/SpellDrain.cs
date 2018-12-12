using System;
using Aoc.Common.Rpg;

namespace Aoc.Common.Rpg
{  
    public class SpellDrain : Spell
    {
        public SpellDrain()
        {
            Name = "Drain";
            Cost = 73;
        }

        public override void Apply(Character me, Character other)
        {
            base.Apply(me, other);
            other.Health -= 2;
            me.Health += 2;
        }
    }
}