using System;
using System.Linq;
using System.Collections.Generic;

namespace Aoc.Common.Rpg
{  
    public class Character
    {
        public int Damage { get; set; }

        public int Armor { get; set; }

        public int Health { get; set; }

        public int Mana { get; set; }

        public List<Effect> Effects { get; set; }

        public Character()
        {            
        }

        public Character Clone()
        {
            return new Character {
                Damage = Damage,
                Armor = Armor,
                Health = Health,
                Mana = Mana,
                Effects = Effects.Select(effect => effect.Clone()).ToList()
            };
        }

        public void ApplyEffects()
        {
            foreach (Effect effect in Effects)
            {
                effect.Apply(this);
            }

            foreach (Effect effect in Effects.Where(effect => effect.Remaining == 0))
            {
                effect.Clean(this);
            }

            Effects = Effects.Where(effect => effect.Remaining > 0).ToList();
        }
    }
}