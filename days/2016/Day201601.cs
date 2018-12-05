using System;
using System.Text;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using Aoc.Common.Grid;
using Aoc.Common.Simulators;

namespace Aoc
{
    public class Day201601 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string[] _input;

        public Day201601()
        {
            Codename = "2016-01";
            Name = "No Time for a Taxicab";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetStringVector(this, ", ");            
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                Direction direction = Direction.Up;
                Point position;
                foreach (string s in _input)
                {
                    direction = Board<Int64>.Turn(direction, (s[0] == 'R') ? Direction.Right : Direction.Left);
                    position = Board<Int64>.MoveForward(position, direction, Int32.Parse(s.Substring(1, s.Length - 1)));
                }
                return Board<Int64>.GetDistance(position).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                Direction direction = Direction.Up;
                Point position;
                Registers<Int64> history = new Registers<Int64>();
                history[position.X.ToString() + "_" + position.Y.ToString()]++;
                foreach (string s in _input)
                {
                    direction = Board<Int64>.Turn(direction, (s[0] == 'R') ? Direction.Right : Direction.Left);
                    int amount = Int32.Parse(s.Substring(1, s.Length - 1));
                    for (int i = 0; i < amount; ++i)
                    {
                        position = Board<Int64>.MoveForward(position, direction);
                        history[position.X.ToString() + "_" + position.Y.ToString()]++;
                        if (history[position.X.ToString() + "_" + position.Y.ToString()] == 2)
                        {
                            return Board<Int64>.GetDistance(position).ToString();
                        }
                    }
                }
                return "Not found";
            }

            return "";
        }
    }   
}