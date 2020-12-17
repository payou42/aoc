using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using Aoc.Common;
using Aoc.Common.Grid;

namespace Aoc
{
    public class Day201613 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private int _input;

        private Board2D<int?> _board;

        public Day201613()
        {
            Codename = "2016-13";
            Name = "A Maze of Twisty Little Cubicles";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetInt(this);
            _board = new Board2D<int?>();
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                _board[1, 1] = 0;
                Queue<Point> queue = new Queue<Point>();
                queue.Enqueue(new Point(1, 1));
                while (_board[31, 39] == null)
                {
                    var position = queue.Dequeue();
                    EnqueueNeighbors(queue, position);
                }
                return _board[31, 39].Value.ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                int count = 0;
                for (int i = 0; i < 100; ++i)
                {
                    for (int j = 0; j < 100; ++j)
                    {
                        if ((_board[i, j] >= 0) && (_board[i, j] <= 50))
                        {
                            count++;
                        }
                    }
                }
                return count.ToString();
            }

            return "";
        }

        private void EnqueueNeighbors(Queue<Point> queue, Point position)
        {
            for (int i = 0; i < (int)Direction.Count; ++i)
            {
                Point newPos = Board2D<int?>.MoveForward(position, (Direction)i, 1, true);
                if (!IsValid(newPos))
                {
                    // Off-limit
                    continue;
                }

                if (_board[newPos.X, newPos.Y] != null)
                {
                    // Already done
                    continue;
                }

                if (IsWall(newPos))
                {
                    // Unreachable
                    _board[newPos.X, newPos.Y] = -1;
                    continue;
                }

                int weight = _board[position.X, position.Y].Value;
                _board[newPos.X, newPos.Y] = weight + 1;
                queue.Enqueue(newPos);
            }
        }

        private bool IsValid(Point position)
        {
            return (position.X >= 0 && position.Y >= 0);
        }

        private bool IsWall(Point position)
        {
            long x = position.X;
            long y = position.Y;
            long sum = x*x + 3*x + 2*x*y + y + y*y + _input;

            int count = 0;            
            while (sum > 0)
            {
                count += 1;
                sum &= (sum - 1);
            }
            return (count % 2) == 1;
        }
    }   
}