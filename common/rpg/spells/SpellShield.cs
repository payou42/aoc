using System;
using System.Linq;
using Aoc.Common.Rpg;
using Aoc.Common.Rpg.Effects;

namespace Aoc.Common.Rpg.Spells
{  
    public class SpellShield : Spell
    {
        public SpellShield()
        {
            Name = "Shield";
            Cost = 113;
        }

        public override void Apply(Character me, Character other)
        {
            base.Apply(me, other);
            Effect effect = new EffectShield();
            me.Effects.Add(effect);
            effect.Init(me);
        }

        public override bool IsCastable(Character me, Character other)
        {
            if (me.Effects.Any(effect => effect.GetType() == typeof(EffectShield)))
            {
                return false;
            }
            return base.IsCastable(me, other);
        }
    }
}