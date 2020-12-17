using System;
using System.Drawing;
using System.Collections.Generic;

namespace Aoc.Common.Grid
{
    /// <summary>
    /// 4D board
    /// </summary>
    /// <typeparam name="Cell">The content of the cell of the board</typeparam>
    public class Board4D<Cell>
    {
        protected Dictionary<Int32, Dictionary<Int32, Dictionary<Int32, Dictionary<Int32, Cell>>>> _board;

       
        public Board4D()
        {
            _board = new Dictionary<Int32, Dictionary<Int32, Dictionary<Int32, Dictionary<Int32, Cell>>>>();
        }

        /// <summary>
        /// Get or set the content of a cell
        /// When getting an empty cell, a default new Cell is resturned
        /// </summary>
        /// <value>The cell content</value>
        public Cell this[int x, int y, int z, int w]
        { 
            get
            {
                if (_board.ContainsKey(x))
                {                    
                    if (_board[x].ContainsKey(y))
                    {
                        if (_board[x][y].ContainsKey(z))
                        {
                            if (_board[x][y][z].ContainsKey(w))
                        {
                            return _board[x][y][z][w];
                        }
                        }
                    }
                }
                return default;
            }

            set
            {
                if (!_board.ContainsKey(x))
                {
                    _board[x] = new Dictionary<int, Dictionary<int, Dictionary<int, Cell>>>();
                }
                if (!_board[x].ContainsKey(y))
                {
                    _board[x][y] = new Dictionary<int, Dictionary<int, Cell>>();
                }
                if (!_board[x][y].ContainsKey(z))
                {
                    _board[x][y][z] = new Dictionary<int, Cell>();
                }
                _board[x][y][z][w] = value;
            }
        }

        public List<Cell> Values
        {
            get
            {
                List<Cell> result = new List<Cell>();
                foreach (var xs in _board.Values)
                {
                    foreach (var ys in xs.Values)
                    {
                        foreach (var zs in ys.Values)
                        {
                            result.AddRange(zs.Values);
                        }
                    }
                }

                return result;
            }
        }

        public List<(int, int, int, int, Cell)> Cells
        {
            get
            {
                List<(int, int, int, int, Cell)> result = new List<(int, int, int, int, Cell)>();
                foreach (var xs in _board)
                {
                    foreach (var ys in xs.Value)
                    {
                        foreach (var zs in ys.Value)
                        {   
                            foreach (var ws in zs.Value)
                            {   
                                result.Add((xs.Key, ys.Key, zs.Key, ws.Key, ws.Value));
                            }
                        }
                    }                    
                }
                return result;
            }
        }

        public void Remove(int x, int y, int z, int w)
        {
            if (_board.ContainsKey(x))
            {
                if (_board[x].ContainsKey(y))
                { 
                    if (_board[x][y].ContainsKey(z))
                    {                    

                        if (_board[x][y][z].Remove(w))
                        {
                            if (_board[x][y][z].Count == 0)
                            {
                                _board[x][y].Remove(z);
                                if (_board[x][y].Count == 0)
                                {
                                    _board[x].Remove(y);
                                    if (_board[x].Count == 0)
                                    {
                                        _board.Remove(x);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get the distance L1 of a position in the board from the center
        /// </summary>
        /// <param name="position">Position from the center</param>
        /// <returns>The distance</returns>
        public static Int64 GetDistance(int x, int y, int z, int w)
        {
            return (Int64)Math.Abs(x) + (Int64)Math.Abs(y) + (Int64)Math.Abs(z) + (Int64)Math.Abs(w);
        }

        /// <summary>
        /// Get the bounds of the used cells
        /// </summary>
        /// <returns>The bounds coordinates</returns>
        public (int xmin, int ymin, int zmin, int wmin, int xmax, int ymax, int zmax, int wmax) GetBounds()
        {
            int xmin = int.MaxValue;
            int xmax = int.MinValue;
            int ymin = int.MaxValue;
            int ymax = int.MinValue;
            int zmin = int.MaxValue;
            int zmax = int.MinValue;
            int wmin = int.MaxValue;
            int wmax = int.MinValue;

            bool empty = true;
            foreach (var xs in _board)
            {
                foreach (var ys in xs.Value)
                {
                    foreach (var zs in ys.Value)
                    {
                        foreach (var ws in zs.Value)
                        {
                            empty = false;
                            xmin = Math.Min(xmin, xs.Key);
                            ymin = Math.Min(ymin, ys.Key);
                            zmin = Math.Min(zmin, zs.Key);
                            wmin = Math.Min(wmin, ws.Key);
                            xmax = Math.Max(xmax, xs.Key);
                            ymax = Math.Max(ymax, ys.Key);
                            zmax = Math.Max(zmax, zs.Key);
                            wmax = Math.Max(wmax, ws.Key);
                        }
                    }
                }
            }

            if (empty)
            {
                return (0, 0, 0, 0, 0, 0, 0, 0);
            }

            return (xmin, ymin, zmin, wmin, xmax, ymax, zmax, wmax);
        }

        /// <summary>
        /// Count the neighboors matching a predicate; including the diagonals
        /// </summary>
        /// <param name="x">The x position</param>
        /// <param name="y">The y position</param>
        /// <param name="z">The z position</param>
        /// <param name="z">The w position</param>
        /// <param name="predicate">The predicate</param>
        /// <returns>The number of neighboors that passed the predicate</returns>
        public int CountNeighbours(int x, int y, int z, int w, Predicate<Cell> predicate)
        {
            int count = 0;
            for (int i = x - 1; i <= x + 1; ++i)
            {
                for (int j = y - 1; j <= y + 1; ++j)
                {
                    for (int k = z - 1; k <= z + 1; ++k)
                    {
                        for (int l = w - 1; l <= w + 1; ++l)
                        {
                            if ((i != x) || (j != y) || (k != z) || (l != w))
                            {
                                if (predicate(this[i, j, k, l]))
                                {
                                    count++;
                                }
                            }
                        }
                    }
                }
            }

            return count;
        }
    }
}
