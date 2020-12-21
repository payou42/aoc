using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Geometry;

namespace Aoc
{
    public class Day202017 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private Board<bool> _pocket3D;

        private Board<bool> _pocket4D;

        public Day202017()
        {
            Codename = "2020-17";
            Name = "Conway Cubes";
        }

        public void Init()
        {
            var input = Aoc.Framework.Input.GetStringVector(this);

            _pocket3D = new Board<bool>();
            _pocket4D = new Board<bool>();

            for (int y = 0; y < input.Length; ++y)
            {
                for (int x = 0; x < input[y].Length; ++x)
                {
                    if (input[y][x] == '#')
                    {
                        _pocket3D[new long[] {x, y, 0}] = true;
                        _pocket4D[new long[] {x, y, 0, 0}] = true;
                    }
                }
            }
        }

        public string Run(Aoc.Framework.Part part)
        {
            long actives = 0;
            Board<bool> current = (part == Aoc.Framework.Part.Part1) ? _pocket3D : _pocket4D;
            for (int i = 0; i < 6; ++i)
            {
                (current, actives) = this.Round(current);
            }

            return actives.ToString();
        }

        private (Board<bool>, long) Round(Board<bool> previous)
        {
            var (boundmin, boundmax) = previous.GetBounds();
            var traversemin = boundmin.Select(c => c - 1).ToArray();
            var traversemax = boundmax.Select(c => c + 1).ToArray();
            Board<bool> next = new Board<bool>();
            long actives = 0;

            next.Traverse(traversemin, traversemax, (c) =>
            {                
                int nbNeighboors = previous.CountNeighbours(c, (p, v) => v);
                if (previous[c] && (nbNeighboors == 2 || nbNeighboors == 3))
                {
                    next[c.ToArray()] = true;
                    actives++;
                }

                if (!previous[c] && (nbNeighboors == 3))
                {
                    next[c.ToArray()] = true;
                    actives++;
                }
            });

            return (next, actives);
        }
    }   
}