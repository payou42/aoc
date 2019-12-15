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
    public class Day201915 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        public static long[] DirectionToMovement = new long[4] {1, 4, 2, 3};

        public static long[] DirectionToBacktrack = new long[4] {2, 3, 1, 4};

        private static int Width = 21;

        private static int Height = 21;

        private IntCpu _cpu;

        private Board<int> _board;

        private Point _position;

        private Point _oxygen;

        public Day201915()
        {
            Codename = "2019-15";
            Name = "Oxygen System";
        }

        public void Init()
        {
            _cpu = new IntCpu();
            _board = new Board<int>();
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                // prepare the robot
                _cpu.Reset(Aoc.Framework.Input.GetLongVector(this, ","));
                Queue<(Point, Direction)> movements = new Queue<(Point, Direction)>();
                _position = new Point(0,0);
                _board[0, 0] = 0;
                Move(Direction.Up);
                Move(Direction.Down);
                Move(Direction.Left);
                Move(Direction.Right);
                return _board[_oxygen].ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // Clean the board
                _board.Cells.ForEach(c => _board[c.Item1] = c.Item2 > 0 ? 0 : c.Item2);
                _board[_oxygen] = 1;

                long minutes = 0;
                while (true)
                {
                    // Get the oxygen
                    var oxygen = _board.Cells.Where(c => c.Item2 == 1).ToList();
                    long count = oxygen.Count();

                    // Spread oxygen
                    foreach (var c in oxygen)
                    {
                        Spread(c.Item1);
                    }

                    // Count oxygen again
                    long newCount = _board.Cells.Count(c => c.Item2 == 1);
                    if (newCount == count)
                    {
                        break;
                    }

                    // Add minutes
                    minutes++;
                }

                return minutes.ToString();
            }

            return "";
        }

        private void Move(Direction d)
        {
            // Get the place we want to visit
            Point moved = Board<long>.MoveForward(_position, d);

            // Off the board ?
            if (Math.Abs(moved.X) > Width || Math.Abs(moved.Y) > Height)
            {
                return;
            }

            // Check if there's a wall
            if (_board[moved] == -1)
            {
                return;
            }
            
            // Check if it has been visited
            if (_board[moved] > 0)
            {
                // Already visited, update its distance
                if (_board[moved] > _board[_position])
                {
                    _board[moved] = _board[_position] + 1;
                }

                // Don't propragate
                return;
            }

            // Ask the robot to visit the place
            _cpu.Input.Enqueue(DirectionToMovement[(int)d]);
            _cpu.Run(true);
            long result = _cpu.Output.Dequeue();
            switch (result)
            {
                case 0:
                {
                    // We hit a wall
                    _board[moved] = -1;
                    return;
                }

                case 1:
                case 2:                
                {
                    _board[moved] = _board[_position] + 1;
                    _position = moved;
                    if (result == 2)
                    {
                        _oxygen = _position;
                    }

                    // Process other movements
                    Move(Direction.Up);
                    Move(Direction.Down);
                    Move(Direction.Left);
                    Move(Direction.Right);

                    // Once explored, backtrack
                    BackTrack(d);

                    // All done
                    return;
                }
            }
        }

        private void BackTrack(Direction d)
        {
            _cpu.Input.Enqueue(DirectionToBacktrack[(int)d]);
            _cpu.Run(true);
            _cpu.Output.Dequeue();
            _position = Board<long>.MoveBackward(_position, d);
        }

        private void Spread(Point p)
        {
            Point moved = Board<long>.MoveForward(p, Direction.Up);
            if (_board[moved] == 0)
                _board[moved] = 1;

            moved = Board<long>.MoveForward(p, Direction.Down);
            if (_board[moved] == 0)
                _board[moved] = 1;

            moved = Board<long>.MoveForward(p, Direction.Left);
            if (_board[moved] == 0)
                _board[moved] = 1;

            moved = Board<long>.MoveForward(p, Direction.Right);
            if (_board[moved] == 0)
                _board[moved] = 1;
        }
    }   
}