using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Aoc
{
    public class Day201715 : Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private Generator[] _generators;

        public Day201715()
        {            
            Codename = "2017-15";
            Name = "Dueling Generators";            
        }

        public string Run(Part part)
        {
            if (part == Part.Part1)
            {
                CreateGenerators(part);
                int match = 0;
                for (int i = 0; i < 40000000; ++i)
                {
                    _generators[0].Generate();
                    _generators[1].Generate();
                    if (_generators[0].GetLowWord() == _generators[1].GetLowWord())
                    {
                        match++;
                    }
                }
                return match.ToString();
            }

            if (part == Part.Part2)
            {
                CreateGenerators(part);
                int match = 0;
                for (int i = 0; i < 5000000; ++i)
                {
                    _generators[0].Generate();
                    _generators[1].Generate();
                    if (_generators[0].GetLowWord() == _generators[1].GetLowWord())
                    {
                        match++;
                    }
                }
                return match.ToString();
            }

            return "";
        }

        private void CreateGenerators(Part part)
        {
            string[] lines = Input.GetStringVector(this);
            int[] factors = new int[2] { 16807, 48271 };
            int[] modulo = new int[2] { 2147483647, 2147483647 };
            int[] checks = new int[2] { 4, 8 };

            _generators = new Generator[2];
            for (int i = 0; i < _generators.Length; ++i)
            {
                _generators[i] = new Generator(
                    Int32.Parse(lines[i].Split(" ")[4]),
                    factors[i],
                    modulo[i],
                    (part == Part.Part1) ? 0 : checks[i]
                );
            }
        }
    }   
}