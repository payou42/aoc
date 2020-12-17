using System;
using System.Drawing;

using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Grid;

namespace Aoc
{
    public class Day202011 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        public enum Seat
        {
            Floor = 0,
            Empty,
            Occupied,
        }

        private Board2D<Seat> _input;

        private Point _tl;

        private Point _br;

        private (int, int)[] _directions = new (int, int)[] { (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1) };

        public Day202011()
        {
            Codename = "2020-11";
            Name = "Seating System";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetBoard2D<Seat>(this, (c) => c switch
            {
                'L' => Seat.Empty,
                '#' => Seat.Occupied,
                _ => Seat.Floor,
            });

            (_tl, _br) = _input.GetBounds();
            
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                Board2D<Seat> current = _input;
                bool changed = true;
                while (changed)
                {
                    changed = this.Round(current, out var next, (board, x, y) => board.CountNeighbours(new Point(x, y), (s) => s == Seat.Occupied, true), 4);
                    current = next;
                }

                return current.Count((s) => s == Seat.Occupied).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                Board2D<Seat> current = _input;
                bool changed = true;
                while (changed)
                {
                    changed = this.Round(current, out var next, this.CountVisibles, 5);
                    current = next;
                }

                return current.Count((s) => s == Seat.Occupied).ToString();
            }

            return "";
        }

        private bool Round(Board2D<Seat> previous, out Board2D<Seat> next, Func<Board2D<Seat>, int, int, int> counter, int threshold)
        {
            bool changed = false;
            next = new Board2D<Seat>();

            for (int x = _tl.X; x <= _br.X; ++x)
            {
                for (int y = _tl.Y; y <= _br.Y; ++y)
                {
                    switch (previous[x, y])
                    {
                        case Seat.Empty:
                        {
                            int nb = counter(previous, x, y);
                            if (nb == 0)
                            {
                                next[x, y] = Seat.Occupied;
                                changed = true;
                            }
                            else
                            {
                                next[x, y] = Seat.Empty;
                            }

                            break;
                        }

                        case Seat.Occupied:
                        {
                            int nb = counter(previous, x, y);
                            if (nb >= threshold)
                            {
                                next[x, y] = Seat.Empty;
                                changed = true;
                            }
                            else
                            {
                                next[x, y] = Seat.Occupied;
                            }

                            break;
                        }

                        case Seat.Floor:
                        {
                            next[x, y] = Seat.Floor;
                            break;
                        }
                    }
                }
            }

            return changed;
        }

        private int CountVisibles(Board2D<Seat> board, int x, int y)
        {
            int count = 0;
            foreach (var (xdir, ydir) in _directions)
            {
                count += this.CountVisibles(board, x, y, xdir, ydir);
            }

            return count;
        }

        private int CountVisibles(Board2D<Seat> board, int x, int y, int xdir, int ydir)
        {
            var xcur = x + xdir;
            var ycur = y + ydir;
            while (xcur >= _tl.X && xcur <= _br.X && ycur >= _tl.Y && ycur <= _br.Y)
            {
                switch (board[xcur, ycur])
                {
                    case Seat.Occupied: return 1;
                    case Seat.Empty: return 0;
                    case Seat.Floor:
                    {
                        xcur += xdir;
                        ycur += ydir;
                        break;
                    }
                }
            }

            return 0;
        }
    }
}