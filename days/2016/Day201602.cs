using System;
using System.Text;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using Aoc.Common.Grid;

namespace Aoc
{
    public class Day201602 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string[] _input;

        public Day201602()
        {
            Codename = "2016-02";
            Name = "Bathroom Security";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetStringVector(this);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                // Init the board
                Point position;
                Board<int> board = new Board<int>();
                position.X = 1;
                position.Y = 1;
                for (int i = 0; i < 9; ++i)
                {
                    board[i % 3, 2 - (i / 3)] = i + 1;
                }

                // Look for the code
                return FindCode(board, position);
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // Init the board                
                Point position;
                Board<int> board = new Board<int>();
                position.X = 0;
                position.Y = 2;
                board[2, 4] = 1;
                for (int i = 1; i <= 3; i++) board[i, 3] = i + 1;
                for (int i = 0; i <= 4; i++) board[i, 2] = i + 5;
                for (int i = 1; i <= 3; i++) board[i, 1] = i + 9;
                board[2, 0] = 13;

                // Look for the code
                return FindCode(board, position);
            }

            return "";
        }

        private string FindCode(Board<int> board, Point position)
        {
            // Look for the code
            string chars = "0123456789ABCD";
            string code = "";
            foreach (string s in _input)
            {
                if (s != "")
                {
                    foreach (char c in s)
                    {
                        Direction d = Direction.Up;
                        if (c == 'L') d = Direction.Left;
                        if (c == 'R') d = Direction.Right;
                        if (c == 'D') d = Direction.Down;

                        position = Board<Int64>.MoveForward(position, d);
                        if (board[position.X, position.Y] == 0)
                        {
                            position = Board<Int64>.MoveBackward(position, d);
                        }                        
                    }
                    code += chars[board[position.X, position.Y]];
                }
            }
            return code;
        }
    }   
}