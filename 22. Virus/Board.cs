using System;
using System.Linq;
using System.Collections.Generic;

namespace _22._Virus
{
    class Board
    {
        public Dictionary<Int32, Dictionary<Int32, Cell>> _board;

        public Board()
        {
            _board = new Dictionary<Int32, Dictionary<Int32, Cell>>();
        }

        public void Init(string input)
        {
            string[] lines = input.Split("\r\n");
            int start = - ((lines.Length - 1) / 2);
            for (int j = 0; j < lines.Length; ++j)
            {
                for (int i = 0; i < lines[j].Length; ++i)
                {
                    this[start + i, start + j] = (lines[lines.Length - 1 - j][i] == '#') ? Cell.INFECTED : Cell.CLEAN;
                }
            }
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
                return Cell.CLEAN;
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

        public void Draw(Virus virus)
        {
            // Get the min and max of the coordinates            
            int xmin = _board.Keys.Min();
            int xmax = _board.Keys.Max();
            int ymin = Int32.MaxValue;
            int ymax = Int32.MinValue;
            foreach (KeyValuePair<Int32, Dictionary<Int32, Cell>> kvp in _board)
            {
                ymin = Math.Min(ymin, kvp.Value.Keys.Min());
                ymax = Math.Max(ymax, kvp.Value.Keys.Max());
            }

            // Draw the board
            for (int j = ymax; j >= ymin; --j)
            {
                for (int i = xmin; i <= xmax; ++i)
                {
                    bool isVirus = (virus.X == i) && (virus.Y == j);
                    Console.Write("{0}{1}{2}", isVirus ? "[" : " ", GetState(i, j), isVirus ? "]" : " " );
                }
                Console.WriteLine("");
            }
        }

        private string GetState(int i, int j)
        {
            switch (this[i, j])
            {
                case Cell.CLEAN: return ".";
                case Cell.INFECTED: return "#";
                case Cell.WEAKENED: return "W";
                case Cell.FLAGGED: return "F";
            }
            return " ";
        }
    }
}
