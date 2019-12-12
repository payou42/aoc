using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Crypto;
using Aoc.Common.Numbers;
using Aoc.Common.Physics;

namespace Aoc
{
    public class Day201912 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private Body[] _bodies;

        public Day201912()
        {
            Codename = "2019-12";
            Name = "The N-Body Problem";
        }

        public void Init()
        {
            // Create the bodies
            string[] input = Aoc.Framework.Input.GetStringVector(this);
            _bodies = new Body[input.Length];
            for (int i = 0; i < input.Length; ++i)
            {
                string[] components = input[i].Split(new char[4] {'<', '>', '=', ','});
                _bodies[i] = new Body() { Position = new Point3D() {X = double.Parse(components[2]), Y = double.Parse(components[4]), Z = double.Parse(components[6])}};
            }
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                for (int i = 0; i < 1000; ++i)
                {
                    Step();
                }

                return _bodies.Sum(b => b.Energy).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // Reinit everything
                Init();

                // Position & velocity history
                Dictionary<string, ulong> history_x = new Dictionary<string, ulong>();
                Dictionary<string, ulong> history_y = new Dictionary<string, ulong>();
                Dictionary<string, ulong> history_z = new Dictionary<string, ulong>();
                ulong cycle_x = 0;
                ulong cycle_y = 0;
                ulong cycle_z = 0;

                // Find the cycle in X, Y and Z separately
                ulong index = 0;
                while (cycle_x == 0 || cycle_y ==0 || cycle_z == 0)
                {
                    (string hash_x, string hash_y, string hash_z) = GenerateHash();
 
                    if (cycle_x == 0 && history_x.ContainsKey(hash_x))
                            cycle_x = index;

                    if (cycle_y == 0 && history_y.ContainsKey(hash_y))
                        cycle_y = index;

                    if (cycle_z == 0 && history_z.ContainsKey(hash_z))
                        cycle_z = index;

                    // Store hashes
                    history_x[hash_x] = index;
                    history_y[hash_y] = index;
                    history_z[hash_z] = index;

                    // Calculate next step
                    Step();
                    index++;
                }

                // The common cycle is the least common multiple of each coordinate cycles
                ulong result = Lcm.Calculate(Lcm.Calculate(cycle_x, cycle_y), cycle_z);
                return result.ToString();
            }

            return "";
        }

        private void Step()
        {
            // Gravity
            for (int i = 0; i < _bodies.Length - 1; ++i)
            {
                for (int j = i + 1; j < _bodies.Length; ++j)
                {
                    Body.ApplyGravity(_bodies[i], _bodies[j]);
                }
            }

            // Move
            foreach (Body b in _bodies)
            {
                b.Move();
            }
        }

        private (string, string, string) GenerateHash()
        {
            string x = "";
            string y = "";
            string z = "";
            foreach (Body b in _bodies)
            {
                x += b.Position.X.ToString() + "*" + b.Velocity.X.ToString() + "*";
                y += b.Position.Y.ToString() + "*" + b.Velocity.Y.ToString() + "*";
                z += b.Position.Z.ToString() + "*" + b.Velocity.Z.ToString() + "*";
            }

            return (x, y, z);
        }
    }   
}