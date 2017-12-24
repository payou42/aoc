using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Aoc
{
    public class Particle
    {
        private Int64[][] _components = null;

        public Particle(string desc)
        {
            _components = new Int64[3][];
            string[] components = desc.Split(", ");
            for (int cmp = 0; cmp < 3;  ++cmp)
            {
                _components[cmp] = components[cmp].Substring(3, components[cmp].Length - 4).Split(",").Select(s => Int64.Parse(s)).ToArray();
            }
        }

        public Int64 Distance
        {
            get
            {
                return Math.Abs(_components[0][0]) + Math.Abs(_components[0][1]) + Math.Abs(_components[0][2]);
            }
        }

        public void Move()
        {
            for (int i = 0; i < 3; ++i)
            {
                _components[1][i] += _components[2][i];
                _components[0][i] += _components[1][i];
            }           
        }

        public static bool Collide(Particle a, Particle b)
        {
            return (a._components[0][0] == b._components[0][0]) && (a._components[0][1] == b._components[0][1]) && (a._components[0][2] == b._components[0][2]);
        }
    }
}