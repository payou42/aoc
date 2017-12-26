using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Aoc
{
    public class Day201712 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private Dictionary<int, List<int>> _links;

        public Day201712()
        {
            Codename = "2017-12";
            Name = "Digital Plumber";
        }

        public void Init()
        {
            BuildLinks();
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                HashSet<int> group = new HashSet<int>();
                BuildGroup(group, 0);
                return group.Count.ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                int groupCount = 0;
                HashSet<int> all = new HashSet<int>();
                foreach (int id in _links.Keys)
                {
                    if (!all.Contains(id))
                    {
                        groupCount++;
                        BuildGroup(all, id);
                    }
                }
                return groupCount.ToString();
            }

            return "";
        }

        private void BuildLinks()
        {
            _links = new Dictionary<int, List<int>>();
            foreach (string line in Aoc.Framework.Input.GetStringVector(this))
            {
                string[] items = line.Split(" <-> ");
                int id = Int32.Parse(items[0]);
                _links[id] = new List<int>();

                string[] links = items[1].Split(", ");
                for (int i = 0; i < links.Length; ++i)
                {
                    int link = Int32.Parse(links[i]);
                    _links[id].Add(link);
                }
            }            
        }

        private void BuildGroup(HashSet<int> group, int id)
        {
            if (!group.Contains(id))
            {
                group.Add(id);
                foreach (int link in _links[id])
                {
                    BuildGroup(group, link);
                }
            }
        }
    }   
}