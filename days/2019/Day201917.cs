using System;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Geometry;
using Aoc.Common.Simulators;

namespace Aoc
{
    public class Day201917 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private IntCpu _cpu;

        private Board2D<long> _board;

        public Day201917()
        {
            Codename = "2019-17";
            Name = "Set and Forget";
        }

        public void Init()
        {
            _cpu = new IntCpu();
            _board = new Board2D<long>();
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                // Run the program
                int x = 0;
                int y = 0;
                int width = 0;
                int height = 0;
                _cpu.Reset(Aoc.Framework.Input.GetLongVector(this, ","));
                _cpu.Run();

                // Build the camera view
                while (_cpu.Output.Count > 0)
                {
                    // Get the value
                    long value = _cpu.Output.Dequeue();

                    // Process it
                    // Running the ASCII program on your Intcode computer will provide the current view of the scaffolds.
                    // This is output, purely coincidentally, as ASCII code: 35 means #, 46 means .,
                    // 10 starts a new line of output below the current one, and so on. (Within a line, characters are drawn left-to-right.)
                    // In the camera output, # represents a scaffold and . represents open space.
                    // The vacuum robot is visible as ^, v, <, or > depending on whether it is facing up, down, left, or right respectively.
                    // When drawn like this, the vacuum robot is always on a scaffold; if the vacuum robot ever walks off of a scaffold
                    // and begins tumbling through space uncontrollably, it will instead be visible as X.
                    switch (value)
                    {
                        case 10:
                        {
                            x = 0;
                            y++;
                            height++;
                            break;
                        }

                        default:
                        {
                            _board[x, y] = value;
                            x++;
                            width = Math.Max(width, x);
                            break;
                        }
                    }
                }

                // Debug
                // Draw(width, height - 1);

                // Get the intersections
                var intersections = _board.Cells.Where(c => IsIntersection(c.Item1));

                // Evaluate the calibration value
                long calibration = intersections.Select(c => c.Item1.X * c.Item1.Y).Sum();
                return calibration.ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // R,10,R,8,L,10,L,10,R,8,L,6,L,6,R,8,L,6,L,6,R,10,R,8,L,10,L,10,L,10,R,10,L,6,R,8,L,6,L,6,L,10,R,10,L,6,L,10,R,10,L,6,R,8,L,6,L,6,R,10,R,8,L,10,L,10
                string[] commands = new string[5]
                {
                    "A,B,B,A,C,B,C,C,B,A",  // Main
                    "R,10,R,8,L,10,L,10",   // A
                    "R,8,L,6,L,6",          // B
                    "L,10,R,10,L,6",        // C
                    "n",                    // Live feed mode
                };

                _cpu.Reset(Aoc.Framework.Input.GetLongVector(this, ","));
                _cpu.Code[0] = 2;

                // Send the commands
                foreach (string line in commands)
                {
                    foreach (char c in line)
                    {
                        _cpu.Input.Enqueue((long)c);
                    }

                    _cpu.Input.Enqueue(10);
                }

                _cpu.Run();
                long dust = 0;
                while (_cpu.Output.Count > 0)
                {
                    dust =_cpu.Output.Dequeue();
                }

                return dust.ToString();
            }

            return "";
        }

        private bool IsIntersection(Point p)
        {
            if (_board[p] != 35)
                return false;

            if (_board[p.X, p.Y + 1] != 35)
                return false;

            if (_board[p.X, p.Y - 1] != 35)
                return false;

            if (_board[p.X + 1, p.Y] != 35)
                return false;

            if (_board[p.X - 1, p.Y] != 35)
                return false;

            return true;
        }
    }
}