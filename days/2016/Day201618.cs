using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201618 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string _input;

        private bool[] _row;

        public Day201618()
        {
            Codename = "2016-18";
            Name = "Like a Rogue";
        }

        public void Init()
        {
            // Load the data
            _input = Aoc.Framework.Input.GetString(this);

            // Init the first row
            _row = new bool[_input.Length + 2];
            for (int i = 0; i < _input.Length; ++i)
            {
                _row[i + 1] = (_input[i] == '^');
            }
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return CountSafe(_row, 40).ToString();;
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                return CountSafe(_row, 400000).ToString();
            }

            return "";
        }

        private long CountSafe(bool[] initial, long height)
        {
            bool[] current = initial;
            long count = 0;
            for (int i = 0; i < height; ++i)
            {
                // Count the current row
                count += current.LongCount(tile => !tile);

                // Generate the next row
                bool[] next = new bool[current.Length];
                for (int j = 1; j < current.Length - 1; ++j)
                {
                    next[j] = (current[j - 1] != current[j + 1]);
                }

                current = next;
            }

            return (count - (2 * height));
        }
    }   
}