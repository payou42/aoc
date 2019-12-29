using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Simulators;

namespace Aoc
{
    public class Day201925 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private IntCpu _cpu;

        private long[] _code;

        private long _result;

        private bool _first;

        public Day201925()
        {
            Codename = "2019-25";
            Name = "Cryostasis";
        }

        public void Init()
        {
            _code = Aoc.Framework.Input.GetLongVector(this, ",");
            _cpu = new IntCpu();
            _result = 0;
            _first = true;
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                // Fetch all the item
                string[] commands =
                {
                    "west", "west", "west", "west", "take dark matter", "east", "south", "take fixed point", "west", "take food ration",
                    "east", "north", "east", "south", "take astronaut ice cream", "south", "take polygon", "east", "take easter egg",
                    "east", "take weather machine", "north", "inv"
                };

                _cpu.Reset(_code);
                Step();
                foreach (string command in commands)
                {
                    Step(command);
                }

                // Now try any combinaison of them
                string[] items = { "polygon", "fixed point", "astronaut ice cream", "easter egg", "dark matter", "food ration", "weather machine" };
                int inventory = 0;

                // Drop evrything
                foreach (string item in items)
                {
                    Step($"drop {item}");
                }

                while (_cpu.State != IntCpu.RunningState.Halted)
                {
                    int next = inventory + 1;
                    for (int i = 0; i < 7; ++i)
                    {
                        if ((((next >> i) & 0x01) == 0x1) && (((inventory >> i) & 0x01) == 0x0))
                        {
                            Step($"take {items[i]}");
                        }

                        if ((((next >> i) & 0x01) == 0x0) && (((inventory >> i) & 0x01) == 0x1))
                        {
                            Step($"drop {items[i]}");
                        }
                    }

                    Step($"north");
                    inventory = next;
                }

                return _result.ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                return "Victory!";
            }

            return "";
        }

        private void Step(string command = null)
        {
            if (command != null)
            {
                foreach (char c in command)
                {
                    _cpu.Input.Enqueue((long)c);
                }
                _cpu.Input.Enqueue(10);
            }

            _cpu.Run();
            while (_cpu.Output.Count > 0)
            {
                char c = (char)_cpu.Output.Dequeue();
                if (char.IsDigit(c))
                {  
                    if (!_first)
                        _result = _result * 10 + int.Parse(c.ToString());

                    _first = false;
                }
            }
        }
    }   
}