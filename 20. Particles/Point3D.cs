using System;
using System.Text;
using System.Collections.Generic;

namespace _20._Particles
{
    class Point3D
    {
        private Int64[] _coordinates;

        public Point3D()
        {
            _coordinates = new Int64[3] {0, 0, 0};
        }

        public Point3D(Int64 x, Int64 y, Int64 z)
        {
            _coordinates = new Int64[3] {x, y, z};
        }

        public Int64 this[int index]
        {
            get
            {
                return _coordinates[index];
            }

            set
            {
                _coordinates[index] = value;
            }
        }

        public Int64 Distance
        {
            get
            {
                return Math.Abs(_coordinates[0]) + Math.Abs(_coordinates[1]) + Math.Abs(_coordinates[2]);
            }
        }

        public override string ToString()
        {
            return String.Format("<{0},{1},{2}>", _coordinates[0], _coordinates[1], _coordinates[2]);
        }
        
    }
}