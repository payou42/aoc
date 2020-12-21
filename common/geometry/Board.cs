using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace Aoc.Common.Geometry
{
    /// <summary>
    /// Variable number of dimensions board
    /// </summary>
    /// <typeparam name="Coordinates">The coordinates system of the board</typeparam>
    /// <typeparam name="Cell">The content of the cell of the board</typeparam>
    public class Board<Cell>
    {
        /// <summary>
        /// The content of the board
        /// </summary>
        protected Dictionary<long[], Cell> _board;

        /// <summary>
        /// Crete a new board.
        /// </summary>
        /// <returns>The list of cells</returns>
        public Board()
        {
            _board = new Dictionary<long[], Cell>(new CoordinatesEqualityComparer());
        }

        /// <summary>
        /// Get or set the content of a cell
        /// When getting an empty cell, a default new Cell is resturned
        /// </summary>
        /// <value>The cell content</value>
        public Cell this[long[] coordinates]
        { 
            get
            {
                if (_board.ContainsKey(coordinates))
                {                    
                    return _board[coordinates];
                }

                return default;
            }

            set
            {                
                _board[coordinates] = value;               
            }
        }

        /// <summary>
        /// Get the list of used values.
        /// </summary>
        /// <returns>The list of cells</returns>
        public IEnumerable<Cell> Values
        {
            get
            {                
                return _board.Values;
            }
        }

        /// <summary>
        /// Get the list of used coordinates.
        /// </summary>
        /// <returns>The list of cells</returns>
        public IEnumerable<long[]> Coordinates
        {
            get
            {                
                return _board.Keys;
            }
        }

        /// <summary>
        /// Get the list of cells with their coordinates and value.
        /// </summary>
        /// <returns>The list of cells</returns>
        public List<(long[] Coordinate, Cell Cell)> Cells
        {
            get
            {
                List<(long[], Cell)> result = new List<(long[], Cell)>();
                foreach (var kvp in _board)
                {
                    result.Add((kvp.Key, kvp.Value));
                }

                return result;
            }
        }

        /// <summary>
        /// Remove a cell from the board, given its coordinates.
        /// </summary>
        /// <param name="coordinates">The position to remove</param>
        public void Remove(long[] coordinates)
        {
            _board.Remove(coordinates);
        }

        /// <summary>
        /// Get the distance L1 of a position in the board from the center
        /// (also called Manhattan distance)
        /// </summary>
        /// <param name="coordinates">Position from the center</param>
        /// <returns>The distance</returns>
        public static long GetManhattanDistance(long[] coordinates)
        {
            return coordinates.Sum(l => Math.Abs(l));
        }

        /// <summary>
        /// Get the bounds on each axes of the usd cell coordinates.
        /// </summary>
        /// <returns>The min and max bounds</returns>
        public (long[] min, long[] max) GetBounds()
        {
            if (_board.Count == 0)
                return (null, null);

            long[] min = new long[_board.Keys.First().Length];
            long[] max = new long[_board.Keys.First().Length];
            foreach (var c in _board.Keys.Skip(1))
            {
                for (int i = 0; i < min.Length; ++i)
                {
                    min[i] = Math.Min(min[i], c[i]);
                    max[i] = Math.Max(max[i], c[i]);
                }
            }

            return (min, max);
        }

        /// <summary>
        /// Get the number of neighbours mathing a given predicate.
        /// </summary>
        /// <returns>The number of matching neighbours</returns>
        public int CountNeighbours(long[] coordinates, Func<long[], Cell, bool> predicate)
        {
            long[] min = coordinates.Select(c => c - 1).ToArray();
            long[] max = coordinates.Select(c => c + 1).ToArray();             
            int count = 0;
            var comparer = new CoordinatesEqualityComparer();

            this.Traverse(min, max, (pos) =>
            {
                // Check that this is not the center
                if (!comparer.Equals(pos, coordinates) && predicate(pos, this[pos]))
                {
                    count++;
                }
            });

            return count;
        }

        /// <summary>
        /// Count the number of cells that matches a predicate.
        /// </summary>
        /// <returns>The result of the count</returns>
        public long CountCells(Func<long[], Cell, bool> predicate)
        {
            return _board.Sum(c => predicate(c.Key, c.Value) ? 1L : 0L);
        }

        /// <summary>
        /// Parse an hypercube of the grid, given min and max coordinates.
        /// </summary>
        public void Traverse(long[] min, long[] max, Action<long[]> action)
        {
            switch (min.Length)
            {
                case 1: Traverse1(min[0], max[0], action); break;
                case 2: Traverse2(min[0], min[1], max[0], max[1], action); break;
                case 3: Traverse3(min[0], min[1], min[2], max[0], max[1], max[2], action); break;
                case 4: Traverse4(min[0], min[1], min[2], min[3], max[0], max[1], max[2], max[3], action); break;
                default:
                {
                    long[] pos = new long[min.Length];
                    this.TraverseN(pos, 0, min, max, action);
                    break;
                }
            }
        }

        /// <summary>
        /// Parse an hypercube of the grid, given min and max coordinates.
        /// </summary>
        private void Traverse1(long x0, long x1, Action<long[]> action)
        {
            long[] pos = new long[1];
            for (long x = x0; x <= x1; ++x)
            {
                pos[0] = x;
                action(pos);
            }
        }

        /// <summary>
        /// Parse an hypercube of the grid, given min and max coordinates.
        /// </summary>
        private void Traverse2(long x0, long y0, long x1, long y1, Action<long[]> action)
        {
            long[] pos = new long[2];
            for (long x = x0; x <= x1; ++x)
            {
                pos[0] = x;
                for (long y = y0; y <= y1; ++y)
                {
                    pos[1] = y;
                    action(pos);
                }
            }
        }

        /// <summary>
        /// Parse an hypercube of the grid, given min and max coordinates.
        /// </summary>
        private void Traverse3(long x0, long y0, long z0, long x1, long y1, long z1, Action<long[]> action)
        {
            long[] pos = new long[3];
            for (long x = x0; x <= x1; ++x)
            {
                pos[0] = x;
                for (long y = y0; y <= y1; ++y)
                {
                    pos[1] = y;
                    for (long z = z0; z <= z1; ++z)
                    {
                        pos[2] = z;
                        action(pos);
                    }   
                }
            }
        }

        /// <summary>
        /// Parse an hypercube of the grid, given min and max coordinates.
        /// </summary>
        private void Traverse4(long x0, long y0, long z0, long w0, long x1, long y1, long z1, long w1, Action<long[]> action)
        {
            long[] pos = new long[4];
            for (long x = x0; x <= x1; ++x)
            {
                pos[0] = x;
                for (long y = y0; y <= y1; ++y)
                {
                    pos[1] = y;
                    for (long z = z0; z <= z1; ++z)
                    {
                        pos[2] = z;
                        for (long w = w0; w <= w1; ++w)
                        {
                            pos[3] = w;
                            action(pos);
                        }   
                    }   
                }
            }
        }

        /// <summary>
        /// Parse an hypercube of the grid, given min and max coordinates.
        /// </summary>
        private void TraverseN(long[] pos, int index, long[] min, long[] max, Action<long[]> action)
        {
            if (index >= pos.Length)
            {
                action(pos);
            }
            else
            {
                for (long l = min[index]; l <= max[index]; ++l)
                {
                    pos[index] = l;
                    this.TraverseN(pos, index + 1, min, max, action);
                }
            }
        }

        /// <summary>
        /// Internal class to handle the key hashing.
        /// </summary>
        private class CoordinatesEqualityComparer : IEqualityComparer<long[]>
        {
            public bool Equals(long[] x, long[] y)
            {
                if (x.Length != y.Length)
                {
                    return false;
                }

                for (int i = 0; i < x.Length; i++)
                {
                    if (x[i] != y[i])
                    {
                        return false;
                    }
                }

                return true;
            }

            public int GetHashCode(long[] obj)
            {
                int result = 17;
                for (int i = 0; i < obj.Length; i++)
                {
                    unchecked
                    {
                        result = result * 23 + (int)obj[i];
                    }
                }

                return result;
            }
        }
    }
}
