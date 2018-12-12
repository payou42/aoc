using System;
using Aoc.Common.Rpg;

namespace Aoc.Common.Rpg
{  
    public class EffectPoison : Effect
    {
        public EffectPoison()
        {
            Name = "Poison";
            Duration = 6;
            Remaining = 6;
            Health = -3;
        }

        public override Effect Clone()
        {
            return new EffectPoison { Name = Name, Duration = Duration, Remaining = Remaining, Health = Health };
        }
    }
}