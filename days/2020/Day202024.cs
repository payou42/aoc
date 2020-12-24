using System;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Geometry;

namespace Aoc
{
    public class Day202024 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string[] _input;

        private Board2D<bool> _initial;

        public Day202024()
        {
            Codename = "2020-24";
            Name = "Lobby Layout";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetStringVector(this);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                _initial = new Board2D<bool>();
                foreach (string s in _input)
                {
                    HorizontalHexCoordinate hc = new HorizontalHexCoordinate();
                    for (int i = 0; i < s.Length; ++i)
                    {
                        string direction =  s[i..(i + 1)];
                        if (s[i] == 'n' || s[i] == 's')
                        {
                            direction = s[i..(i + 2)];
                            i++;
                        }

                        hc.Move(direction);
                    }

                    _initial[(int)hc.X, (int)hc.Y] = !_initial[(int)hc.X, (int)hc.Y];
                }

                return _initial.Count(v => v).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                Board2D<bool> current = _initial;
                for (int i = 0; i < 100; ++i)
                {
                    current = Round(current);
                }

                return current.Count(v => v).ToString();
            }

            return "";
        }

        private Board2D<bool> Round(Board2D<bool> current)
        {
            Board2D<bool> next = new Board2D<bool>();
            var (topleft, bottomright) = current.GetBounds();
            
            for (int x = topleft.X - 1; x <= bottomright.X + 1; ++x)
            {
                for (int y = topleft.Y - 1; y <= bottomright.Y + 1; ++y)
                {
                    // Check all the neighboor, in an hex grid, of this cell
                    int flipped = 0;
                    for (int i = -1; i <= 1; ++i)
                    {
                        for (int j = -1; j <= 1; ++j)
                        {
                            // Skip center and invalid hex positions
                            if (i == j)
                                continue;

                            if (current[x + i, y + j])
                                flipped++;
                        }
                    }

                    if (current[x , y] && (flipped == 1 || flipped == 2))
                        next[x, y] = true;

                    if (!current[x, y] && (flipped == 2))
                        next[x, y] = true;
                }
            }

            return next;
        }
    }   
}