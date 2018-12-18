using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201814 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string _target;

        public Day201814()
        {
            Codename = "2018-14";
            Name = "Chocolate Charts";
        }

        public void Init()
        {
            _target = Aoc.Framework.Input.GetString(this);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                var recipes = new List<int> { 3, 7 };
                var elf1 = 0;
                var elf2 = 1;
                var target = int.Parse(_target);
                while (recipes.Count < target + 10)
                {
                    var sum = recipes[elf1] + recipes[elf2];
                    if (sum >= 10)
                    {
                        recipes.Add(sum / 10);
                    }
                    recipes.Add(sum % 10);
                    elf1 = (elf1 + recipes[elf1] + 1) % recipes.Count;
                    elf2 = (elf2 + recipes[elf2] + 1) % recipes.Count;
                }
                string result = new string(recipes.Skip(target).Take(10).Select(x => x.ToString()[0]).ToArray());
                return result;
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                var recipes = new List<int> { 3, 7 };
                var elf1 = 0;
                var elf2 = 1;
                while (true)
                {
                    var s = new string(recipes.Skip(recipes.Count - _target.Length-1).Select(x => x.ToString()[0]).ToArray());
                    if (s.Contains(_target))
                    {
                        s = new string(recipes.Select(x => x.ToString()[0]).ToArray());
                        return s.IndexOf(_target).ToString();
                    }
                    var sum = recipes[elf1] + recipes[elf2];
                    if (sum >= 10)
                    {
                        recipes.Add(sum / 10);
                    }
                    recipes.Add(sum % 10);
                    elf1 = (elf1 + recipes[elf1] + 1) % recipes.Count;
                    elf2 = (elf2 + recipes[elf2] + 1) % recipes.Count;
                }
            }

            return "";
        }
    }   
}