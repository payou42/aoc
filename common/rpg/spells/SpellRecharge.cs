using System;
using System.Linq;
using Aoc.Common.Rpg;
using Aoc.Common.Rpg.Effects;

namespace Aoc.Common.Rpg.Spells
{  
    public class SpellRecharge : Spell
    {
        public SpellRecharge()
        {
            Name = "Recharge";
            Cost = 229;
        }

        public override void Apply(Character me, Character other)
        {
            base.Apply(me, other);
            Effect effect = new EffectRecharge();
            me.Effects.Add(effect);
            effect.Init(me);
        }

        public override bool IsCastable(Character me, Character other)
        {
            if (me.Effects.Any(effect => effect.GetType() == typeof(EffectRecharge)))
            {
                return false;
            }
            return base.IsCastable(me, other);
        }
    }
}