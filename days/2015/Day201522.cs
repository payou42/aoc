using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Rpg;
using Aoc.Common.Rpg.Spells;

namespace Aoc
{
    public class Day201522 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private List<Spell> _spells;

        public Day201522()
        {
            Codename = "2015-22";
            Name = "Wizard Simulator 20XX";
        }

        public void Init()
        {
            _spells = new List<Spell> { new SpellMagicMissile(), new SpellDrain(), new SpellShield(), new SpellPoison(), new SpellRecharge() };
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return Run(false).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                return Run(true).ToString();
            }

            return "";
        }

        private int Run(bool hard)
        {
            Queue<Combat> queue = new Queue<Combat>();
            queue.Enqueue(new Combat
            {
                Me = new Character { Health = 50, Armor = 0, Damage = 0, Mana = 500, Effects = new List<Effect>() },
                Boss = new Character { Health = 71, Armor = 0, Damage = 10, Mana = 0, Effects = new List<Effect>() },
                TotalMana = 0,
                History = new List<string>()
            });

            int minimumMana = int.MaxValue;
            while (queue.TryDequeue(out var combat))
            {
                foreach (Spell spell in _spells)
                {
                    Combat.Issue issue = combat.Round(spell, hard, out var next);
                    switch (issue)
                    {
                        case Combat.Issue.PlayerWin:
                        {
                            if (next.TotalMana < minimumMana)
                            {
                                minimumMana = next.TotalMana;
                                List<string> minimumHistory = next.History;
                            }
                            continue;
                        }

                        case Combat.Issue.Invalid:
                        case Combat.Issue.BossWin:
                        {
                            continue;
                        }


                        case Combat.Issue.InProgress:
                        {
                            if (next.TotalMana < minimumMana)
                            {
                                queue.Enqueue(next);
                            }
                            break;
                        }
                    }
                }
            }

            // Expected 1824 & 1937 in hard
            return minimumMana;
        }
    }   
}