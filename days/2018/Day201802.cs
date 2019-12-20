using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Simulators;
using Aoc.Common.Strings;

namespace Aoc
{
    public class Day201802 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string[] _ids;

        public Day201802()
        {
            Codename = "2018-02";
            Name = "Inventory Management System";
        }

        public void Init()
        {
            _ids = Aoc.Framework.Input.GetStringVector(this);            
        }

        protected (bool, bool) HasDuplicatesOrTriplicates(string id)
        {
            Registers<Int64> counter = new Registers<Int64>();
            foreach (char c in id)
            {
                counter[c.ToString()]++;
            }

            bool hasDuplicates = counter.Storage.Values.Where(v => v == 2).Any();
            bool hasTriplicates = counter.Storage.Values.Where(v => v == 3).Any();
            return (hasDuplicates, hasTriplicates);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                var counts = _ids.Select(id => HasDuplicatesOrTriplicates(id));
                return (counts.Count(c => c.Item1) * counts.Count(c => c.Item2)).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                foreach (string id1 in _ids)
                {
                    string id2 = _ids.Where(s => Difference.Count(id1, s) == 1).FirstOrDefault();
                    if (id2 != null)
                    {
                        return Difference.GetCommonPart(id1, id2);
                    }
                }
            }

            return "";
        }
    }   
}