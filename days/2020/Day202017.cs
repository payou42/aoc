using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Grid;

namespace Aoc
{
    public class Day202017 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private Board3D<bool> _pocket3D;

        private Board4D<bool> _pocket4D;

        public Day202017()
        {
            Codename = "2020-17";
            Name = "Conway Cubes";
        }

        public void Init()
        {
            var input = Aoc.Framework.Input.GetStringVector(this);

            _pocket3D = new Board3D<bool>();
            _pocket4D = new Board4D<bool>();

            for (int y = 0; y < input.Length; ++y)
            {
                for (int x = 0; x < input[y].Length; ++x)
                {
                    _pocket3D[x, y, 0] = (input[y][x] == '#');
                    _pocket4D[x, y, 0, 0] = (input[y][x] == '#');
                }
            }
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                long actives = 0;
                Board3D<bool> current = _pocket3D;
                for (int i = 0; i < 6; ++i)
                {
                    (current, actives) = this.Round3D(current);
                }

                return actives.ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                long actives = 0;
                Board4D<bool> current = _pocket4D;
                for (int i = 0; i < 6; ++i)
                {
                    (current, actives) = this.Round4D(current);
                }

                return actives.ToString();
            }

            return "";
        }

        private (Board3D<bool>, long) Round3D(Board3D<bool> previous)
        {
            var (boundminx, boundminy, boundminz, boundmaxx, boundmaxy, boundmaxz) = previous.GetBounds();
            Board3D<bool> next = new Board3D<bool>();
            long actives = 0;

            for (int x = boundminx - 1; x <= boundmaxx + 1; ++x)
            {
                for (int y = boundminy - 1; y <= boundmaxy + 1; ++y)
                {
                    for (int z = boundminz - 1; z <= boundmaxz + 1; ++z)
                    {
                        int nbNeighboors = previous.CountNeighbours(x, y, z, c => c);
                        if (previous[x, y, z] && (nbNeighboors == 2 || nbNeighboors == 3))
                        {
                            next[x, y, z] = true;
                            actives++;
                        }

                        if (!previous[x, y, z] && (nbNeighboors == 3))
                        {
                            next[x, y, z] = true;
                            actives++;
                        }
                    }
                }
            }

            return (next, actives);
        }

        private (Board4D<bool>, long) Round4D(Board4D<bool> previous)
        {
            var (boundminx, boundminy, boundminz, boundminw, boundmaxx, boundmaxy, boundmaxz, boundmaxw) = previous.GetBounds();
            Board4D<bool> next = new Board4D<bool>();
            long actives = 0;

            for (int x = boundminx - 1; x <= boundmaxx + 1; ++x)
            {
                for (int y = boundminy - 1; y <= boundmaxy + 1; ++y)
                {
                    for (int z = boundminz - 1; z <= boundmaxz + 1; ++z)
                    {
                        for (int w = boundminw - 1; w <= boundmaxw + 1; ++w)
                        {
                            int nbNeighboors = previous.CountNeighbours(x, y, z, w, c => c);
                            if (previous[x, y, z, w] && (nbNeighboors == 2 || nbNeighboors == 3))
                            {
                                next[x, y, z, w] = true;
                                actives++;
                            }

                            if (!previous[x, y, z, w] && (nbNeighboors == 3))
                            {
                                next[x, y, z, w] = true;
                                actives++;
                            }
                        }
                    }
                }
            }

            return (next, actives);
        }
    }   
}