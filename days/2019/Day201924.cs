using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201924 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private uint _initial;

        private static List<(int, int)>[] _neighbours = new List<(int, int)>[25]
        {
            new List<(int, int)>() { (0, 1), (0, 5), (-1, 11), (-1, 7)},
            new List<(int, int)>() { (0, 0), (0, 2), (0, 6), (-1, 7)},
            new List<(int, int)>() { (0, 1), (0, 3), (0, 7), (-1, 7)},
            new List<(int, int)>() { (0, 2), (0, 4), (0, 8), (-1, 7)},
            new List<(int, int)>() { (0, 3), (0, 9), (-1, 7), (-1, 13)},
            new List<(int, int)>() { (0, 0), (0, 6), (0, 10), (-1, 11)},
            new List<(int, int)>() { (0, 1), (0, 5), (0, 7), (0, 11)},
            new List<(int, int)>() { (0, 6), (0, 2), (0, 8), (1, 0), (1, 1), (1, 2), (1, 3), (1, 4)},
            new List<(int, int)>() { (0, 3), (0, 7), (0, 9), (0, 13)},
            new List<(int, int)>() { (0, 4), (0, 8), (0, 14), (-1, 13)},
            new List<(int, int)>() { (0, 5), (0, 11), (0, 15), (-1, 11)},
            new List<(int, int)>() { (0, 6), (0, 10), (0, 16), (1, 0), (1, 5), (1, 10), (1, 15), (1, 20)},
            new List<(int, int)>() { },
            new List<(int, int)>() { (0, 8), (0, 14), (0, 18), (1, 4), (1, 9), (1, 14), (1, 19), (1, 24)},
            new List<(int, int)>() { (0, 9), (0, 13), (0, 19), (-1, 13)},
            new List<(int, int)>() { (0, 10), (0, 16), (0, 20), (-1, 11)},
            new List<(int, int)>() { (0, 15), (0, 11), (0, 17), (0, 21)},
            new List<(int, int)>() { (0, 16), (0, 18), (0, 22), (1, 20), (1, 21), (1, 22), (1, 23), (1, 24)},
            new List<(int, int)>() { (0, 13), (0, 17), (0, 19), (0, 23)},
            new List<(int, int)>() { (0, 14), (0, 18), (0, 24), (-1, 13)},
            new List<(int, int)>() { (0, 15), (0, 21), (-1, 11), (-1, 17)},
            new List<(int, int)>() { (0, 16), (0, 20), (0, 22), (-1, 17)},
            new List<(int, int)>() { (0, 17), (0, 21), (0, 23), (-1, 17)},
            new List<(int, int)>() { (0, 18), (0, 22), (0, 24), (-1, 17)},
            new List<(int, int)>() { (0, 19), (0, 23), (-1, 13), (-1, 17)},
        };

        public Day201924()
        {
            Codename = "2019-24";
            Name = "Planet of Discord";
        }

        public void Init()
        {
            string[] lines = Aoc.Framework.Input.GetStringVector(this);
            int i = 0;
            _initial = 0;
            foreach (string line in lines)
            {
                foreach (char c in line)
                {
                    if (c == '#')
                    {
                        _initial |= (uint)(1 << i);
                    }

                    i++;
                }
            }
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                HashSet<uint> _history = new HashSet<uint>();
                _history.Add(_initial);
                uint current = _initial;
                while (true)
                {
                    current = SimpleStep(current);
                    if (_history.Contains(current))
                    {
                        return current.ToString();
                    }

                    _history.Add(current);
                }
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                Dictionary<int, uint> levels = new Dictionary<int, uint>();
                levels[0] = _initial;
                for (int i = 0; i < 200; ++i)
                {
                    levels = RecursiveStep(levels);                    
                }

                return CountBugs(levels).ToString();
            }

            return "";
        }

        private uint SimpleStep(uint state)
        {
            uint next = 0;
            for (int i = 0; i < 25; ++i)
            {
                int count = SimpleCountAdjacent(state, i);
                if ((state & (1 << i)) == 0)
                {
                    if ((count == 1) || (count == 2))
                    {
                        next |= (uint)(1 << i);
                    }
                }
                else
                {
                    if (count == 1)
                    {
                        next |= (uint)(1 << i);
                    }
                }
            }

            return next;
        }

        private int SimpleCountAdjacent(uint state, int index)
        {
            int count = 0;
            if (index - 5 >= 0)
                count += (state & (1 << (index - 5))) == 0 ? 0 : 1;

            if ((index - 1 >= 0) && ((index  - 1) % 5 != 4))
                count += (state & (1 << (index - 1))) == 0 ? 0 : 1;

            if ((index + 1 < 25) && ((index + 1) % 5 != 0))
                count += (state & (1 << (index + 1))) == 0 ? 0 : 1;

            if (index + 5 < 25)
                count += (state & (1 << (index + 5))) == 0 ? 0 : 1;

            return count;
        }

        private int RecursiveCountAdjacent(uint state, int index, uint outer, uint inner)
        {
            int count = 0;
            foreach (var p in  _neighbours[index])
            {
                uint level = p.Item1 == 0 ? state : (p.Item1 == -1 ? outer : inner);
                count += (level & (1 << p.Item2)) == 0 ? 0 : 1;
            }

            return count;
        }

        private void Dump(uint state)
        {
            for (int h = 0; h < 5; ++h)
            {
                for (int w = 0; w < 5; ++w)
                {
                    if ((state & (1 << (h * 5 + w))) != 0)
                    {
                        Console.Write('#');
                    }
                    else
                    {
                        Console.Write('.');
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        private Dictionary<int, uint> RecursiveStep(Dictionary<int, uint> state)
        {
            Dictionary<int, uint> nexts = new Dictionary<int, uint>();
            int min = state.Keys.Min() - 1;
            int max = state.Keys.Max() + 1;
            for (int l = min; l <= max; ++l)
            {
                uint outer = state.ContainsKey(l - 1) ? state[l - 1] : 0;
                uint inner = state.ContainsKey(l + 1) ? state[l + 1] : 0;
                uint current = state.ContainsKey(l) ? state[l] : 0;                
                uint next = 0;
                for (int i = 0; i < 25; ++i)
                {
                    if (i == 12)
                        continue;
                    
                    int count = RecursiveCountAdjacent(current, i, outer, inner);
                    if ((current & (1 << i)) == 0)
                    {
                        if ((count == 1) || (count == 2))
                        {
                            next |= (uint)(1 << i);
                        }
                    }
                    else
                    {
                        if (count == 1)
                        {
                            next |= (uint)(1 << i);
                        }
                    }
                }

                nexts[l] = next;
            }

            foreach (int i in nexts.Keys.ToList())
            {
                if (nexts[i] == 0)
                {
                    nexts.Remove(i);
                }
            }
            return nexts;
        }

        private int CountBugs(Dictionary<int, uint> levels)
        {
            int count = 0;
            foreach (uint level in levels.Values)
            {
                for (int i = 0; i < 25; ++i)
                {
                    if ((level & (1 << i)) != 0)
                    {
                        count++;
                    }
                }
            }

            return count;
        }
    }   
}