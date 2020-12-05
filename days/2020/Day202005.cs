using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day202005 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string[] _input;

        private int[] _seats;

        public Day202005()
        {
            Codename = "2020-05";
            Name = "Binary Boarding";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetStringVector(this, "\r\n");
            _seats = _input.Select(s => Convert.ToInt32(s.Replace("F", "0").Replace("B", "1").Replace("L", "0").Replace("R", "1"), 2)).ToArray();
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return _seats.Max().ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                int i = 1;
                bool active = false;
                while (true)
                {
                    if (!_seats.Contains(i))
                    {
                        if (active)
                        {
                            return i.ToString();
                        }
                    }
                    else
                    {
                        active = true;
                    }

                    i++;
                }
            }

            return "";
        }
    }   
}