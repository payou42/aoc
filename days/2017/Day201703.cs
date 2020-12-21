using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common.Geometry;

namespace Aoc
{
    public class Day201703 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private int _input;
        
        private Board2D<Int32> _board;

        private int x;

        private int y;

        public Day201703()
        {
            Codename = "2017-03";
            Name = "Spiral Memory";
            
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetInt(this);
            _board = new Board2D<Int32>();
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                int layer = GetLayer(_input);
                return (layer + GetMoveCountFromCenter(layer, GetOffsetInLayer(_input, layer))).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                x = 0;
                y = 0;
                for (int i = 0; i < _input; ++i)
                {
                    MoveNext(i, part);
                    if (_board[x, y] > _input)
                    {
                        return _board[x, y].ToString();
                    }
                }
            }
            return "";
        }

        private int GetLayer(int position)
        {
            for (int i = 0; true; ++i)
            {
                if ((2*i + 1)*(2*i + 1) >= position)
                {
                    return i;
                }
            }
        }

        private int GetOffsetInLayer(int position, int layer)
        {
            if (position == 1)
            {
                return 0;
            }
            return position - ((2*(layer - 1) + 1)*(2*(layer - 1) + 1)) - 1;
        }

        private int GetMoveCountFromCenter(int layer, int offset)
        {
            if (layer == 0)
            {
                return 0;
            }

            int smallOffest = offset % (2 * layer);
            if (smallOffest <= (layer - 1)) {
                return (layer - 1) - smallOffest;
            }
            return smallOffest - (layer - 1);
            
        }

        private void MoveNext(int index, Aoc.Framework.Part part)
        {
            // Initialisation
            if (index == 0)
            {
                x = 0;
                y = 0;                
                _board[0, 0] = 1;
                return;
            }

            // Get the state of adjacent cells
            int left = _board[x - 1, y];
            int right = _board[x + 1, y];
            int up = _board[x, y - 1];
            int down = _board[x, y + 1];

            // Move rules according to adjacent cells content
            if ((left > 0) && (up == 0))
            {
                // Move UP
                y--;
            }
            else if (down > 0)
            {
                // Move LEFT
                x--;
            }
            else if (right > 0)
            {
                // Move DOWN
                y++;
            }
            else
            {
                // Move RIGHT
                x++;
            }

            // Add the new cell in the dictionary
            _board[x, y] = (part == Aoc.Framework.Part.Part1) ? index + 1 : SumAdjacent();
        }

        private int SumAdjacent()
        {
            int sum = 0;
            for (int i = -1; i <= 1;  ++i)
            {
                for (int j = -1; j <= 1; ++j)
                {
                    sum += _board[x + i, y + j];
                }
            }
            return sum;
        }

    }
}