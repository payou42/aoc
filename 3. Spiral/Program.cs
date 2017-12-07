using System;
using System.Collections.Generic;

namespace _3._Spiral
{
    class Program
    {
        static int GetLayer(int position)
        {
            for (int i = 0; true; ++i)
            {
                if ((2*i + 1)*(2*i + 1) >= position)
                {
                    return i;
                }
            }
        }

        static int GetOffsetInLayer(int position, int layer)
        {
            if (position == 1)
            {
                return 0;
            }
            return position - ((2*(layer - 1) + 1)*(2*(layer - 1) + 1)) - 1;
        }

        static int GetMoveCountFromCenter(int layer, int offset)
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

        static Board BOARD = new Board();
        
        static Cell MoveNext(Cell current)
        {
            // Initialisation
            if (current == null)
            {
                Cell origin = new Cell(0, 0, 1);
                BOARD[origin.X, origin.Y] = origin;
                return origin;
            }

            // Get the state of adjacent cells
            Cell left = BOARD[current.X - 1, current.Y];
            Cell right = BOARD[current.X + 1, current.Y];
            Cell up = BOARD[current.X, current.Y - 1];
            Cell down = BOARD[current.X, current.Y + 1];

            // Move rules according to adjacent cells content
            Cell cell = null;
            if ((left != null) && (up == null))
            {
                // Move UP
                cell = new Cell(current.X, current.Y - 1, current.Content + 1);
            }
            else if (down != null)
            {
                // Move LEFT
                cell = new Cell(current.X - 1, current.Y, current.Content + 1);
            }
            else if (right != null)
            {
                // Move DOWN
                cell = new Cell(current.X, current.Y + 1, current.Content + 1);
            }
            else
            {
                // Move RIGHT
                cell = new Cell(current.X + 1, current.Y, current.Content + 1);
            }

            // Add the new cell in the dictionary
            BOARD[cell.X, cell.Y] = cell;
            return cell;
        }

        static void Main(string[] args)
        {
            /* MATH WAY */
            int test = 1234567;
            Console.WriteLine("Position = {0}", test);
            Console.WriteLine("Layer = {0}", GetLayer(test));
            Console.WriteLine("Offset = {0}", GetOffsetInLayer(test, GetLayer(test)));
            Console.WriteLine("Moves = {0}", GetMoveCountFromCenter(GetLayer(test), GetOffsetInLayer(test, GetLayer(test))));
            Console.WriteLine("Total = {0}", GetLayer(test) + GetMoveCountFromCenter(GetLayer(test), GetOffsetInLayer(test, GetLayer(test))));

            /* ALGO WAY */
            Cell latest = null;
            for (int i = 0; i < test; ++i)
            {
                latest = MoveNext(latest);
            }
            Console.WriteLine("Final cell : ({0},{1}) with index {2} and distance {3}", latest.X, latest.Y, latest.Content, Math.Abs(latest.X) + Math.Abs(latest.Y));
        }
    }
}
