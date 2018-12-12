using System;
using Aoc.Common.Rpg;

namespace Aoc.Common.Rpg
{  
    public class SpellMagicMissile : Spell
    {
        public SpellMagicMissile()
        {
            Name = "Magic Missile";
            Cost = 53;
        }

        public override void Apply(Character me, Character other)
        {
            base.Apply(me, other);
            other.Health -= 4;
        }
    }
}