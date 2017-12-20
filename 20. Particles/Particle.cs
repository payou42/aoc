using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace _20._Particles
{
    class Particle
    {
        private Point3D[] _components = null;

        public Particle(string desc)
        {
            _components = new Point3D[3];           
            string[] components = desc.Split(", ");
            for (int cmp = 0; cmp < 3;  ++cmp)
            {
                _components[cmp] = new Point3D();
                Int64[] vector = components[cmp].Substring(3, components[cmp].Length - 4).Split(",").Select(s => Int64.Parse(s)).ToArray();
                for (int vec = 0; vec < 3; vec++)
                {
                    _components[cmp][vec] = vector[vec];
                }
            }
        }

        public Int64 Distance
        {
            get
            {
                return _components[0].Distance;
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

        public override string ToString()
        {
            return String.Format("p={0}, v={1}, a={2}", _components[0], _components[1], _components[2]);
        }

        public static bool Collide(Particle a, Particle b)
        {
            return (a._components[0][0] == b._components[0][0]) && (a._components[0][1] == b._components[0][1]) && (a._components[0][2] == b._components[0][2]);
        }
    }
}