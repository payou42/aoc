using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day202002 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string[] _input;

        public Day202002()
        {
            Codename = "2020-02";
            Name = "Password Philosophy";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetStringVector(this);
        }

        public string Run(Aoc.Framework.Part part)
        {
            int valid = 0;
            foreach (string s in _input)
            {
                if (IsPasswordValid(s, part))
                {
                    valid++;
                }
            }

            return valid.ToString();
        }

        private bool IsPasswordValid(string s, Aoc.Framework.Part part)
        {
            // Extact the info from the string
            string[] items = s.Split(new string[] {"-", ":", " "}, StringSplitOptions.RemoveEmptyEntries);
            int low = int.Parse(items[0]);
            int high = int.Parse(items[1]);
            char letter = char.Parse(items[2]);
            string password = items[3];

            if (part == Aoc.Framework.Part.Part1)
            {
                // Check the password by counting the letter
                int count = password.Count(c => c == letter);
                return count >= low && count <= high;
            }
            else
            {
                // Check the password by verifying the positions
                int count = 0;
                count += (password[low - 1] == letter) ? 1 : 0;
                count += (password[high - 1] == letter) ? 1 : 0;
                return count == 1;
            }
        }
    }   
}