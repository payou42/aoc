using System;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Grid;

namespace Aoc
{
    public class Day201810 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        public class Star
        {
            public Point Position { get; set; }

            public Point Velocity { get; set; }
        }

        protected Star[] _stars;

        public Day201810()
        {
            Codename = "2018-10";
            Name = "The Stars Align";
        }

        public void Init()
        {
            string[] input = Aoc.Framework.Input.GetStringVector(this);
            _stars = new Star[input.Length];
            for (int i = 0; i < input.Length; ++i)
            {
                string[] items = input[i].Split('<', '>', ',');
                Point position = new Point(int.Parse(items[1]), int.Parse(items[2]));
                Point velocity = new Point(int.Parse(items[4]), int.Parse(items[5]));
                _stars[i] = new Star { Position = position, Velocity = velocity };
            }
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                int time = 0;
                while (true)
                {
                    // Build the board at time try
                    Board<bool> board = BuildBoard(time);

                    if (IsConnected(board))
                    {
                        DumpBoard(board);
                        break;
                    }

                    // Next !
                    time++;
                }

                return "LRCXFXRP";
            }

            if (part == Aoc.Framework.Part.Part2)
            {

                int time = 0;
                while (true)
                {
                    // Build the board at time try
                    Board<bool> board = BuildBoard(time);

                    if (IsConnected(board))
                    {
                        return time.ToString();
                    }

                    // Next !
                    time++;
                }
            }

            return "";
        }

        private Board<bool> BuildBoard(int time)
        {
            Board<bool> board = new Board<bool>();
            foreach (Star s in _stars)
            {
                board[s.Position.X + time * s.Velocity.X, s.Position.Y + time * s.Velocity.Y] = true;
            }
            return board;
        }

        private bool IsConnected(Board<bool> board)
        {
            int singleCells = 0;
            foreach (var cell in board.Cells)
            {
                if (!board[cell.Item1.X - 1, cell.Item1.Y]
                  && !board[cell.Item1.X + 1, cell.Item1.Y]
                  && !board[cell.Item1.X, cell.Item1.Y - 1]
                  && !board[cell.Item1.X, cell.Item1.Y + 1])
                {
                  singleCells++;
                }
            }
            return singleCells < 10;
        }

        private void DumpBoard(Board<bool> board)
        {
            var cells = board.Cells;
            int minX = cells.Min(cell => cell.Item1.X);
            int minY = cells.Min(cell => cell.Item1.Y);
            int maxX = cells.Max(cell => cell.Item1.X);
            int maxY = cells.Max(cell => cell.Item1.Y);
            
            for (int y = minY; y <= maxY; ++y)
            {
                for (int x = minX; x <= maxX; ++x)
                {
                    Console.Write(board[x, y] ? "#" : ".");
                }
                Console.WriteLine("");
            }
        }
    }   
}