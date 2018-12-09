using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Rpg;

namespace Aoc
{
    public class Day201521 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private List<Item> _store;


        public Day201521()
        {
            Codename = "2015-21";
            Name = "RPG Simulator 20XX";
        }

        public void Init()
        {
            _store = new List<Item>
            {
                new Item(GearType.Weapon, "Dagger", 8, 4, 0),
                new Item(GearType.Weapon, "Shortsword", 10, 5, 0),
                new Item(GearType.Weapon, "Warhammer", 25, 6, 0),
                new Item(GearType.Weapon, "Longsword", 40, 7, 0),
                new Item(GearType.Weapon, "Greataxe", 74, 8, 0),
                new Item(GearType.Armor, "Leather", 13, 0, 1),
                new Item(GearType.Armor, "Chainmail", 31, 0, 2),
                new Item(GearType.Armor, "Splintmail", 53, 0, 3),
                new Item(GearType.Armor, "Bandedmail", 75, 0, 4),
                new Item(GearType.Armor, "Platemail", 102, 0, 5),
                new Item(GearType.Ring, "Damage +1", 25, 1, 0),
                new Item(GearType.Ring, "Damage +2", 50, 2, 0),
                new Item(GearType.Ring, "Damage +3", 100, 3, 0),
                new Item(GearType.Ring, "Defense +1", 20, 0, 1),
                new Item(GearType.Ring, "Defense +2", 40, 0, 2),
                new Item(GearType.Ring, "Defense +3", 80, 0, 3)
            };
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return BestCost(true).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                return BestCost(false).ToString();
            }

            return "";
        }

        private int BestCost(bool winner)
        {
            // Prepare the final cost
            int best = winner ? int.MaxValue : 0;

            // Prepare our player
            Character me = new Character();
            me.Health = 100;

            // Prepare the enemy
            Character boss = new Character();
            boss.Health = 103;
            boss.Damage = 9;
            boss.Armor = 2;

            // Pick one weapon
            var weapons = _store.Where(item => item.Type == GearType.Weapon);
            foreach (var weapon in weapons)
            {
                // Pick one or no armor
                var armors = _store.Where(item => item.Type == GearType.Armor).Append(null);
                foreach (var armor in armors)
                {
                    // Pick zero, one or 2 rings
                    var rings = _store.Where(item => item.Type == GearType.Ring).Append(null);
                    foreach (var ring1 in rings)
                    {
                        foreach (var ring2 in rings)
                        {
                            if (ring2 != null && ring1 == ring2)
                            {
                                continue;
                            }

                            // Check if the current setup allows us to win
                            me.Damage = weapon.Damage + (ring1 != null ? ring1.Damage : 0) + (ring2 != null ? ring2.Damage : 0);
                            me.Armor = (armor != null ? armor.Armor : 0) + (ring1 != null ? ring1.Armor : 0) + (ring2 != null ? ring2.Armor : 0);

                            // Check if we win
                            if (winner == (me == GetWinner(me, boss)))
                            {
                                int cost = GetCost(weapon, armor, ring1, ring2);
                                best = winner ? Math.Min(cost, best) : Math.Max(cost, best);
                            }
                        }
                    }
                }
            }

            return best;
        }

        private Character GetWinner(Character a, Character b)
        {
            int turnsA = (int)Math.Ceiling((float)b.Health / (float)Math.Max(a.Damage - b.Armor, 1));
            int turnsB = (int)Math.Ceiling((float)a.Health / (float)Math.Max(b.Damage - a.Armor, 1));
            return (turnsA <= turnsB) ? a : b;
        }

        private int GetCost(Item w, Item a, Item r1, Item r2)
        {
            int cost = 0;
            cost += w?.Cost ?? 0;
            cost += a?.Cost ?? 0;
            cost += r1?.Cost ?? 0;
            cost += r2?.Cost ?? 0;
            return cost;
        }
    }   
}