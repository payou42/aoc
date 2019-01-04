using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201825 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        public class Star
        {
            private int[] _coordinates;

            public int this[int index]
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

            public Star()
            {
                _coordinates = new int[4];
            }


            public static int Distance(Star a, Star b)
            {
                int d = 0;
                for (int i = 0; i <4; ++i)
                {
                    d += Math.Abs(a[i] - b[i]);
                }
                return d;
            }

            public static Star Parse(string line)
            {
                string[] items = line.Split(',');
                Star s = new Star();
                for (int i = 0; i < 4; ++i)
                {
                    s[i] = int.Parse(items[i]);
                }
                return s;
            }
        }

        public class Constellation
        {
            private List<Star> _stars;

            public List<Star> Stars
            {
                get
                {
                    return _stars;
                }
            }

            public Constellation()
            {
                _stars = new List<Star>();
            }

            public int Distance(Star star)
            {
                if (_stars.Count == 0)
                {
                    return int.MaxValue;
                }

                return _stars.Min(s => Star.Distance(star, s));
            }

            public static int Distance(Constellation a, Constellation b)
            {
                if (b.Stars.Count == 0)
                {
                    return int.MaxValue;
                }

                return b.Stars.Min(s => a.Distance(s));
            }
        }

        private Star[] _stars;

        public Day201825()
        {
            Codename = "2018-25";
            Name = "Four-Dimensional Adventure";
        }

        public void Init()
        {
            // Build the stars list
            _stars = Aoc.Framework.Input.GetStringVector(this).Select(line => Star.Parse(line)).ToArray();
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                // Build the list of constellation
                List<Constellation> constellations = new List<Constellation>();

                // Process each star
                foreach (Star s in _stars)
                {
                    List<Constellation> candidates = constellations.Where(c => c.Distance(s) <= 3).ToList();
                    if (candidates.Count == 0)
                    {
                        // Create a new constellation
                        Constellation c = new Constellation();
                        c.Stars.Add(s);
                        constellations.Add(c);
                    }
                    else
                    {
                        // Add the star to the first constellation
                        Constellation main = candidates[0];
                        main.Stars.Add(s);

                        // Merge the others constellations
                        for (int i = 1; i < candidates.Count; ++i)
                        {
                            main.Stars.AddRange(candidates[i].Stars);
                            constellations.Remove(candidates[i]);
                        }
                    }

                }

                // All done, already ??
                return constellations.Count.ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                return "Victory!";
            }

            return "";
        }
    }   
}