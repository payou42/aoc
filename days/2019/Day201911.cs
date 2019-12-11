using System;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Grid;
using Aoc.Common.Simulators;

namespace Aoc
{
    public class Day201911 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private Board<long> _hull;

        private Board<bool> _painted;

        private IntCpu _cpu;

        private Direction _direction;

        private Point _position;

        public Day201911()
        {
            Codename = "2019-11";
            Name = "Space Police";
        }

        public void Init()
        {
            
            _cpu = new IntCpu();
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                // Run the robot
                return Paint(0).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // Run the robot
                Paint(1);

                // Draw the result
                StringBuilder result = new StringBuilder();
                result.AppendLine();
                for (int h = 0; h >= -5; --h)
                {
                    for (int w = 0; w < 40; ++w)
                    {
                        result.Append((_hull[w, h] == 1) ? "#" : " ");
                    }

                    result.AppendLine();
                }
                
                return result.ToString();
            }

            return "";
        }
        private long Paint(long initial)
        {
            long painted = 0;
            _hull = new Board<long>();
            _hull[0, 0] = initial;
            _painted = new Board<bool>();
            _direction = Direction.Up;
            _position = new Point(0, 0);
            _cpu.Reset(Aoc.Framework.Input.GetLongVector(this, ","));
            _cpu.Input.Enqueue(_hull[_position.X, _position.Y]);
            while (!_cpu.Halted)
            {
                _cpu.Run();

                if (!_cpu.Halted)
                {
                    if (!_painted[_position.X, _position.Y])
                    {
                        _painted[_position.X, _position.Y] = true;
                        painted++;
                    }

                    _hull[_position.X, _position.Y] = _cpu.Output.Dequeue();
                    long turn = _cpu.Output.Dequeue();
                    if (turn == 0)
                        _direction = Board<long>.Turn(_direction, Direction.Left);

                    if (turn == 1)
                        _direction = Board<long>.Turn(_direction, Direction.Right);

                    _position = Board<long>.MoveForward(_position, _direction);
                    _cpu.Input.Enqueue(_hull[_position.X, _position.Y]);
                }
            }

            return painted;
        }
    }   
}