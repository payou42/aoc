using System;
using System.Linq;
using System.Collections.Generic;

namespace Aoc.Common.Rpg
{
    public class Combat
    {
        public enum Issue
        {
            Invalid,
            InProgress,
            PlayerWin,
            BossWin
        };

        public Character Me { get; set; }

        public Character Boss { get; set; }

        public int TotalMana { get; set; }

        public List<string> History { get; set; }

        public Issue Round(Spell spell, bool hard, out Combat next)
        {
            // New round
            next = new Combat { Me = Me.Clone(), Boss = Boss.Clone(), TotalMana = TotalMana, History = History.Select(h => h).ToList() };
            
            // 1. Apply Hard mode
            if (hard)
            {
                next.Me.Health -= 1;
                if (Outcome != Issue.InProgress)
                {
                    return Outcome;
                }
            }

            // 2. Apply effects
            next.Me.ApplyEffects();
            next.Boss.ApplyEffects();
            if (Outcome != Issue.InProgress)
            {
                return Outcome;
            }


            // 3. Player turn
            if (!spell.IsCastable(next.Me, next.Boss))
            {
                return Issue.Invalid;
            }

            next.History.Add(spell.Name);
            next.TotalMana += spell.Cost;
            spell.Apply(next.Me, next.Boss);
            if (Outcome != Issue.InProgress)
            {
                return Outcome;
            }

            // 4. Apply effects
            next.Me.ApplyEffects();
            next.Boss.ApplyEffects();
            if (Outcome != Issue.InProgress)
            {
                return Outcome;
            }
    
            // 5. Boss turn
            next.Me.Health -= Math.Max(1, next.Boss.Damage - next.Me.Armor);
            return Issue.InProgress;
        }

        private Issue Outcome
        {
            get
            {
                if (Boss.Health <= 0)
                {
                    return Issue.PlayerWin;
                }

                if (Me.Health <= 0)
                {
                    return Issue.BossWin;
                }

                if (Me.Mana < 0)
                {
                    return Issue.Invalid;
                }

                return Issue.InProgress;
            }
        }
    }
}