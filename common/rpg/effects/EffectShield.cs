using System;
using Aoc.Common.Rpg;

namespace Aoc.Common.Rpg.Effects
{  
    public class EffectShield : Effect
    {
        public EffectShield()
        {
            Name = "Shield";
            Duration = 6;
            Remaining = 6;
            Armor = 7;
        }

        public override Effect Clone()
        {
            return new EffectShield { Name = Name, Duration = Duration, Remaining = Remaining, Armor = Armor };
        }

        public override void Init(Character me)
        {
            me.Armor += Armor;
        }

        public override void Clean(Character me)
        {
            me.Armor -= Armor;
        }
    }
}