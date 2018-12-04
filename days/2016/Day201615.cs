using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201615 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        public class Disc
        {
            public int Id { get; set; }

            public int Slots { get; set; }

            public int Initial { get; set; }

            public int PositionAtTime(int time)
            {
                return (Initial + time) % Slots;                
            }
        }

        private string[] _input;

        private Disc[] _discs;

        public Day201615()
        {
            Codename = "2016-15";
            Name = "Timing is Everything";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetStringVector(this);
            _discs = new Disc[_input.Length];
            for (int i = 0; i < _input.Length; ++i)
            {
                string[] items = _input[i].Split(" ");
                int id = int.Parse(items[1].Substring(1));
                int slots = int.Parse(items[3]);
                int initial = int.Parse(items[items.Length - 1].Substring(0, items[items.Length - 1].Length - 1));
                _discs[i] = new Disc { Id = id, Slots = slots, Initial = initial };
            }
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                int time = 0;
                while (true)
                {
                    if (CanGoThrough(_discs, time + 1))
                    {
                        return time.ToString();
                    }
                    time++;
                }
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // Add the extra disc
                Disc[] discs = new Disc[_discs.Length + 1];
                for (int i = 0; i < _discs.Length; ++i)
                {
                    discs[i] = _discs[i];
                }
                discs[_discs.Length] = new Disc { Id = _discs.Length + 1, Slots = 11, Initial = 0 };
                
                // Check like in part 1
                int time = 0;
                while (true)
                {
                    if (CanGoThrough(discs, time + 1))
                    {
                        return time.ToString();
                    }
                    time++;
                }
            }

            return "";
        }

        private bool CanGoThrough(Disc[] discs, int time)
        {
            for (int i = 0; i < discs.Length; ++i)
            {
                if (discs[i].PositionAtTime(time + i) != 0)
                {
                    return false;
                }
            }
            return true;
        }
    }   
}