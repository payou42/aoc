using System;
using System.Drawing;
using System.Collections.Generic;

namespace Aoc.Common.Grid
{
    /// <summary>
    /// Square board
    /// </summary>
    /// <typeparam name="Cell">The content of the cell of the board</typeparam>
    public class Board3D<Cell>
    {
        protected Dictionary<Int32, Dictionary<Int32, Dictionary<Int32, Cell>>> _board;

       
        public Board3D()
        {
            _board = new Dictionary<Int32, Dictionary<Int32, Dictionary<Int32, Cell>>>();
        }

        /// <summary>
        /// Get or set the content of a cell
        /// When getting an empty cell, a default new Cell is resturned
        /// </summary>
        /// <value>The cell content</value>
        public Cell this[int x, int y, int z]
        { 
            get
            {
                if (_board.ContainsKey(x))
                {                    
                    if (_board[x].ContainsKey(y))
                    {
                        if (_board[x][y].ContainsKey(z))
                        {
                            return _board[x][y][z];
                        }
                    }
                }
                return default(Cell);
            }

            set
            {
                if (!_board.ContainsKey(x))
                {
                    _board[x] = new Dictionary<int, Dictionary<int, Cell>>();
                }
                if (!_board[x].ContainsKey(y))
                {
                    _board[x][y] = new Dictionary<int, Cell>();
                }
                _board[x][y][z] = value;
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
                        result.AddRange(ys.Values);
                    }
                }
                return result;
            }
        }

        public List<(int, int, int, Cell)> Cells
        {
            get
            {
                List<(int, int, int, Cell)> result = new List<(int, int, int, Cell)>();
                foreach (var xs in _board)
                {
                    foreach (var ys in xs.Value)
                    {
                        foreach (var zs in ys.Value)
                        {   
                            result.Add((xs.Key, ys.Key, zs.Key, zs.Value));
                        }
                    }                    
                }
                return result;
            }
        }

        public void Remove(int x, int y, int z)
        {
            if (_board.ContainsKey(x))
            {
                if (_board[x].ContainsKey(y))
                {                    
                    if (_board[x][y].Remove(z))
                    {
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

        /// <summary>
        /// Get the distance L1 of a position in the board from the center
        /// </summary>
        /// <param name="position">Position from the center</param>
        /// <returns>The distance</returns>
        public static Int64 GetDistance(int x, int y, int z)
        {
            return (Int64)Math.Abs(x) + (Int64)Math.Abs(y) + (Int64)Math.Abs(z);
        }
    }
}
