using System;
using System.Linq;
using Aoc.Common.Rpg;

namespace Aoc.Common.Rpg
{  
    public class SpellPoison : Spell
    {
        public SpellPoison()
        {
            Name = "Poison";
            Cost = 173;
        }

        public override void Apply(Character me, Character other)
        {
            base.Apply(me, other);
            Effect effect = new EffectPoison();
            other.Effects.Add(effect);
            effect.Init(other);
        }

        public override bool IsCastable(Character me, Character other)
        {
            if (other.Effects.Any(effect => effect.GetType() == typeof(EffectPoison)))
            {
                return false;
            }
            return base.IsCastable(me, other);
        }
    }
}