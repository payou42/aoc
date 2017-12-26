using System;
using System.Drawing;
using System.Collections.Generic;

namespace Aoc.Common
{
    public class Board<Cell>
    {
        public Dictionary<Int32, Dictionary<Int32, Cell>> _board;

        public Board()
        {
            _board = new Dictionary<Int32, Dictionary<Int32, Cell>>();
        }

        public Cell this[int x, int y]
        { 
            get
            {
                if (_board.ContainsKey(x))
                {                    
                    if (_board[x].ContainsKey(y))
                    {
                        return _board[x][y];
                    }
                }
                return default(Cell);
            }

            set
            {
                if (!_board.ContainsKey(x))
                {
                    _board[x] = new Dictionary<int, Cell>();
                }
                _board[x][y] = value;               
            }
        }

        public static Int64 GetDistance(Point position)
        {
            return (Int64)Math.Abs(position.X) + (Int64)Math.Abs(position.Y);
        }

        public static Direction Turn(Direction current, Direction where)
        {
            int increment = 0;
            switch (where)
            {
                case Direction.Left: increment = -1; break;
                case Direction.Right: increment = 1; break;
                case Direction.Down: increment = 2; break;
            }
            return (Direction)(((int)current + (int)Direction.Count + increment) % (int)Direction.Count);
        }

        public static Point MoveForward(Point position, Direction direction, int amount = 1, bool inverseY = false)
        {
            Point p = position;
            switch (direction)
            {
                case Direction.Up:
                {
                    if (inverseY)
                    {
                        p.Y -= amount;
                    }
                    else
                    {
                        p.Y += amount;
                    }
                    break;
                }

                case Direction.Right:
                {
                    p.X += amount;
                    break;
                }

                case Direction.Down:
                {
                    if (inverseY)
                    {
                        p.Y += amount;
                    }
                    else
                    {
                        p.Y -= amount;
                    }
                    break;
                }

                case Direction.Left:
                {
                    p.X -= amount;
                    break;
                }
            }
            return p;
        }
        public static Point MoveBackward(Point position, Direction direction, int amount = 1, bool inverseY = false)
        {
            Point p = position;
            switch (direction)
            {
                case Direction.Up:
                {
                    if (inverseY)
                    {
                        p.Y += amount;
                    }
                    else
                    {
                        p.Y -= amount;
                    }
                    break;
                }

                case Direction.Right:
                {
                    p.X -= amount;
                    break;
                }

                case Direction.Down:
                {
                    if (inverseY)
                    {
                        p.Y -= amount;
                    }
                    else
                    {
                        p.Y += amount;
                    }
                    break;
                }

                case Direction.Left:
                {
                    p.X += amount;
                    break;
                }
            }
            return p;
        }
    }
}
