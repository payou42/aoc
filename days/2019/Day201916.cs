using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201916 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private int[] _input;

        private readonly int[] _pattern = {0, 1, 0, -1};

        public Day201916()
        {
            Codename = "2019-16";
            Name = "Flawed Frequency Transmission";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetString(this).ToCharArray().Select(c => int.Parse(c.ToString())).ToArray();
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                int[] current = _input;
                for (int i = 0; i < 100; ++i)
                {
                    current = ApplyPhase(current, 1);
                }

                return string.Join("", current.Take(8).Select(c => c.ToString()));
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // Set a few constants
                int count = 10000;
                int phases = 100;

                // Extract the offset of the message
                int offset = int.Parse(string.Join("", _input.Take(7).Select(c => c.ToString())));

                // Build the whole long input
                int[] current = new int[_input.Length * count];
                for (int i = 0; i < count; ++i)
                {
                    Buffer.BlockCopy(_input, 0, current, i * _input.Length * sizeof(int), _input.Length * sizeof(int));
                }

                // Process each phase
                for (int phase = 0; phase < phases; ++phase)
                {
                    // We don't need to process the whole string hopefully !
                    // The second half of the string follows a pattern : new[i] = old[i] + old[i+1]
                    // This is not true for the first half but luckily, the offset given is big enough
                    for (int i = current.Length - 2; i >= offset; i--)
                    {
                        int value = current[i + 1] + current[i];
                        current[i] = Math.Abs(value % 10);
                    }
                }

                return string.Join("", current.Skip(offset).Take(8).Select(c => c.ToString()));
            }

            return "";
        }

        private int[] ApplyPhase(int[] input, int offset)
        {
            int[] next = new int[input.Length];
            for (int i = 0; i < next.Length; ++i)
            {
                next[i] = ApplyFFT(input, i, offset);
            }

            return next;
        }

        private int ApplyFFT(int[] input, int index, int offset)
        {

            int output = 0;
            for (int i = 0; i < input.Length; ++i)
            {
                int mask = _pattern[((i + offset)/(index + 1)) % _pattern.Length];
                int value = input[i];
                output += mask * value;
            }

            return Math.Abs(output) % 10;
        }
    }   
}