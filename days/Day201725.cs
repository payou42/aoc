using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Aoc
{
    public class Day201725 : Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }
        
        private string[] _input;

        private Int64 _steps;

        private TuringMachine _machine;

        public Day201725()
        {
            Codename = "2017-25";
            Name = "The Halting Problem";
        }

        public void Init()
        {
            _input = Input.GetStringVector(this);
            _steps = Int64.Parse(_input[1].Split(" ")[5]);

            string initial = _input[0].Split(" ")[3];
            _machine = new TuringMachine(initial.Substring(0, initial.Length - 1));
            AddRules();
        }

        public string Run(Part part)
        {            
            if (part == Part.Part1)
            {
                for (int i = 0; i < _steps; ++i)
                {
                    _machine.Step();
                }
                return _machine.CountOnes().ToString();
            }

            if (part == Part.Part2)
            {
                return "Victory!";
            }

            return "";
        }

        private void AddRules()
        {
            for (int i = 3; i < _input.Length; i += 10)
            {
                string state = _input[i].Split(" ")[2].Substring(0, 1);
                for (int j = 0; j < 2; ++j)
                {
                    string check = _input[(i + 1) + (4 * j)].Trim().Split(" ")[5].Substring(0, 1);
                    Int64 write = Int64.Parse(_input[(i + 2) + (4 * j)].Trim().Split(" ")[4].Substring(0, 1));
                    string direction = _input[(i + 3) + (4 * j)].Trim().Split(" ")[6].Substring(0, 1);
                    string next = _input[(i + 4) + (4 * j)].Trim().Split(" ")[4].Substring(0, 1);
                    _machine.AddRule(state + "_" + check, write, (direction == "r") ? Direction.Right : Direction.Left, next);
                }
            }
        }
    }   
}