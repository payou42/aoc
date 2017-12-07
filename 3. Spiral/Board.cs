using System;
using System.Collections.Generic;

namespace _3._Spiral
{
    class Board
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
                return null;
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
    }
}
