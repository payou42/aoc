using System;
using System.Drawing;
using System.Collections.Generic;

namespace Aoc.Common.Grid
{
    /// <summary>
    /// Square board
    /// </summary>
    /// <typeparam name="Cell">The content of the cell of the board</typeparam>
    public class Board<Cell>
    {
        protected Dictionary<Int32, Dictionary<Int32, Cell>> _board;

       
        public Board()
        {
            _board = new Dictionary<Int32, Dictionary<Int32, Cell>>();
        }

        /// <summary>
        /// Get or set the content of a cell
        /// When getting an empty cell, a default new Cell is resturned
        /// </summary>
        /// <value>The cell content</value>
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

        public List<Cell> Values
        {
            get
            {
                List<Cell> result = new List<Cell>();
                foreach (var dict in _board.Values)
                {
                    result.AddRange(dict.Values);
                }
                return result;
            }
        }

        public List<(Point, Cell)> Cells
        {
            get
            {
                List<(Point, Cell)> result = new List<(Point, Cell)>();
                foreach (var xs in _board)
                {
                    foreach (var ys in xs.Value)
                    {
                        result.Add((new Point(xs.Key, ys.Key), ys.Value));
                    }                    
                }
                return result;
            }
        }

        public void Remove(int x, int y)
        {
            if (_board.ContainsKey(x))
            {
                if (_board[x].Remove(y))
                {
                    if (_board[x].Count == 0)
                    {
                        _board.Remove(x);
                    }
                }
            }
        }

        /// <summary>
        /// Get the distance L1 of a position in the board from the center
        /// </summary>
        /// <param name="position">Position from the center</param>
        /// <returns>The distance</returns>
        public static Int64 GetDistance(Point position)
        {
            return (Int64)Math.Abs(position.X) + (Int64)Math.Abs(position.Y);
        }

        /// <summary>
        /// Get the direction of a turtle in the board atfter turning in a given direction
        /// </summary>
        /// <param name="current">The current direction of the turtle</param>
        /// <param name="where">The way the turtle turn</param>
        /// <returns>The new direction of the turtle</returns>
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

        /// <summary>
        /// Move a turtle forward in the board
        /// </summary>
        /// <param name="position">The current position of the turtle</param>
        /// <param name="direction">The current direction of the turtle</param>
        /// <param name="amount">The amount of cells to move</param>
        /// <param name="inverseY">Is the Y inversed ? (typical grid starting at the top of the screen)</param>
        /// <returns></returns>
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
        /// <summary>
        /// Move a turtle backward in the board
        /// </summary>
        /// <param name="position">The current position of the turtle</param>
        /// <param name="direction">The current direction of the turtle</param>
        /// <param name="amount">The amount of cells to move</param>
        /// <param name="inverseY">Is the Y inversed ? (typical grid starting at the top of the screen)</param>
        /// <returns></returns>
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
