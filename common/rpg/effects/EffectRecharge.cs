using System;
using Aoc.Common.Rpg;

namespace Aoc.Common.Rpg
{  
    public class EffectRecharge : Effect
    {
        public EffectRecharge()
        {
            Name = "Recharge";
            Duration = 5;
            Remaining = 5;
            Mana = 101;
        }

        public override Effect Clone()
        {
            return new EffectRecharge { Name = Name, Duration = Duration, Remaining = Remaining, Mana = Mana };
        }
    }
}