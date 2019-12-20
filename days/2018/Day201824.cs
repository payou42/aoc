using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201824 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        [Flags]
        public enum Element
        {
            None         = 0,
            Slashing     = 1,
            Bludgeoning  = 2,
            Fire         = 4,
            Cold         = 8,
            Radiation    = 16
        }

        public enum Clan
        {
            None,
            Reinder,
            Infection
        }

        public class Group
        {
            public Clan Clan { get; set; }

            public int Count { get; set; }

            public int HP { get; set; }

            public int Damage { get; set; }

            public int Initiative { get; set; }

            public Element Weak { get; set; }

            public Element Immune { get; set; }

            public Element Element { get; set; }

            public int EffectivePower => Count * Damage;

            public int DamageFrom(Group group)
            {
                if (Immune.HasFlag(group.Element))
                {
                    return 0;
                }

                if (Weak.HasFlag(group.Element))
                {
                    return 2 * group.EffectivePower;
                }

                return group.EffectivePower;
            }
        }

        private List<Group> _armies;

        public Day201824()
        {
            Codename = "2018-24";
            Name = "Immune System Simulator 20XX";
        }

        public void Init()
        {
        }

        private void Reset(int boost)
        {
            _armies = new List<Group>
            {
                new Group { Clan = Clan.Reinder, Count = 1117, HP = 5042, Damage = 44 + boost,  Initiative = 15, Element = Element.Fire, Weak = Element.Slashing, Immune = Element.Fire | Element.Radiation | Element.Bludgeoning },
                new Group { Clan = Clan.Reinder, Count = 292,  HP = 2584, Damage = 81 + boost,  Initiative = 18, Element = Element.Bludgeoning, Weak = Element.None, Immune = Element.None },
                new Group { Clan = Clan.Reinder, Count = 2299, HP = 8194, Damage = 35 + boost,  Initiative = 7,  Element = Element.Radiation, Weak = Element.None, Immune = Element.None },
                new Group { Clan = Clan.Reinder, Count = 1646, HP = 6315, Damage = 37 + boost,  Initiative = 9,  Element = Element.Slashing, Weak = Element.Slashing, Immune = Element.None },
                new Group { Clan = Clan.Reinder, Count = 2313, HP = 6792, Damage = 29 + boost,  Initiative = 9,  Element = Element.Bludgeoning, Weak = Element.Fire | Element.Radiation, Immune = Element.Cold },
                new Group { Clan = Clan.Reinder, Count = 2045, HP = 8634, Damage = 36 + boost,  Initiative = 13, Element = Element.Fire, Weak = Element.Radiation, Immune = Element.None },
                new Group { Clan = Clan.Reinder, Count = 31,   HP = 1019, Damage = 295 + boost, Initiative = 6,  Element = Element.Cold, Weak = Element.Bludgeoning, Immune = Element.None },
                new Group { Clan = Clan.Reinder, Count = 157,  HP = 6487, Damage = 362 + boost, Initiative = 3,  Element = Element.Radiation, Weak = Element.Slashing | Element.Cold, Immune = Element.None },
                new Group { Clan = Clan.Reinder, Count = 1106, HP = 4504, Damage = 39 + boost,  Initiative = 12, Element = Element.Slashing, Weak = Element.Cold, Immune = Element.None },
                new Group { Clan = Clan.Reinder, Count = 5092, HP = 8859, Damage = 12 + boost,  Initiative = 16, Element = Element.Radiation, Weak = Element.None, Immune = Element.Cold | Element.Slashing },
                new Group { Clan = Clan.Infection, Count = 3490, HP = 20941, Damage = 9,    Initiative = 5,  Element = Element.Bludgeoning, Weak = Element.None, Immune = Element.Fire },
                new Group { Clan = Clan.Infection, Count = 566,  HP = 11571, Damage = 40,   Initiative = 10, Element = Element.Bludgeoning, Weak = Element.Cold | Element.Bludgeoning, Immune = Element.None },
                new Group { Clan = Clan.Infection, Count = 356,  HP = 30745, Damage = 147,  Initiative = 8,  Element = Element.Slashing, Weak = Element.Radiation, Immune = Element.None },
                new Group { Clan = Clan.Infection, Count = 899,  HP = 49131, Damage = 93,   Initiative = 19, Element = Element.Cold, Weak = Element.Slashing, Immune = Element.Radiation | Element.Bludgeoning | Element.Fire },
                new Group { Clan = Clan.Infection, Count = 1203, HP = 27730, Damage = 43,   Initiative = 4,  Element = Element.Slashing, Weak = Element.Cold, Immune = Element.None },
                new Group { Clan = Clan.Infection, Count = 22,   HP = 45002, Damage = 3748, Initiative = 17, Element = Element.Bludgeoning, Weak = Element.Bludgeoning, Immune = Element.None },
                new Group { Clan = Clan.Infection, Count = 3028, HP = 35744, Damage = 18,   Initiative = 11, Element = Element.Fire, Weak = Element.Bludgeoning, Immune = Element.None },
                new Group { Clan = Clan.Infection, Count = 778,  HP = 17656, Damage = 35,   Initiative = 2,  Element = Element.Bludgeoning, Weak = Element.Fire, Immune = Element.None },
                new Group { Clan = Clan.Infection, Count = 47,   HP = 16006, Damage = 645,  Initiative = 20, Element = Element.Cold, Weak = Element.Cold | Element.Radiation, Immune = Element.Bludgeoning },
                new Group { Clan = Clan.Infection, Count = 4431, HP = 13632, Damage = 6,    Initiative = 1,  Element = Element.Bludgeoning, Weak = Element.Fire, Immune = Element.Bludgeoning }
            };
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                Reset(0);
                Fight();
                return _armies.Sum(group => group.Count).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                int boostMin = 0;
                int boostMax = 10000;

                while (boostMax - boostMin > 1)
                {
                    int currentBoost = (boostMin + boostMax) / 2;
                    if (currentBoost == boostMin)
                    {
                        currentBoost++;
                    }
                    Reset(currentBoost);
                    Clan result = Fight();
                    if (result != Clan.Reinder)
                    {
                        boostMin = currentBoost;
                    }
                    else
                    {
                        boostMax = currentBoost;
                    }
                }

                return _armies.Sum(group => group.Count).ToString();
            }

            return "";
        }

        private Clan Fight()
        {
            bool finished = false;

            while (!finished)
            {
                // Step 1 : Target selection
                Dictionary<Group, Group> attacks = new Dictionary<Group, Group>();
                List<Group> attackers = _armies.OrderByDescending(group => group.EffectivePower).ThenByDescending(group => group.Initiative).ToList();
                foreach (Group attacker in attackers)
                {
                    // Select the best target
                    Group target = _armies.Where(group => group.Clan != attacker.Clan && !attacks.ContainsValue(group)).OrderByDescending(group => group.DamageFrom(attacker)).ThenByDescending(group => group.EffectivePower).ThenByDescending(group => group.Initiative).FirstOrDefault();
                    if (target == null || target.DamageFrom(attacker) == 0)
                    {
                        // No suitable target
                        continue;
                    }

                    attacks[attacker] = target;
                }

                // Step 2 : Attaaaaaacks !
                if (attacks.Count == 0)
                {
                    // Draw
                    return Clan.None;
                }

                attackers = _armies.OrderByDescending(group => group.Initiative).ToList();
                foreach (Group attacker in attackers)
                {
                    // Group is dead ?
                    if (attacker.Count == 0)
                    {
                        continue;
                    }

                    // Group has a selected target ?
                    if (!attacks.ContainsKey(attacker))
                    {
                        continue;
                    }

                    // Deal damage
                    Group target = attacks[attacker];
                    int damage = target.DamageFrom(attacker);
                    target.Count = Math.Max(0, target.Count - damage / target.HP);
                }

                // Step 3 : Remove dead armies
                _armies = _armies.Where(group => group.Count > 0).ToList();

                // Step 4 : Check victory
                finished = (_armies.Count(group => group.Clan == Clan.Infection) == 0) || (_armies.Count(group => group.Clan == Clan.Reinder) == 0);
            }

            if (_armies.Count(group => group.Clan == Clan.Infection) == 0)
            {
                return Clan.Reinder;
            }
            return Clan.Infection;
        }
    }   
}