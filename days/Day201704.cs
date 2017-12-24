using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Aoc
{
    public class Day201704 : Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string[][] _input;

        public Day201704()
        {
            Codename = "2017-04";
            Name = "High-Entropy Passphrases";
        }

        public void Init()
        {
            _input = Input.GetStringMatrix(this);
        }
        
        public string Run(Part part)
        {
            return CountValid(part).ToString();
        }

        private bool CheckWords(string a, string b, Part part)
        {
            if (part == Part.Part1)
            {
                return a == b;
            }
            return Anagram.Check(a, b);
        }

        private bool ValidateRow(int index, Part part)
        {
            for (int i = 0; i < _input[index].Length - 1; ++i)
            {
                for (int j = i + 1; j < _input[index].Length; ++j)
                {
                    if (CheckWords(_input[index][i], _input[index][j], part))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private int CountValid(Part part)
        {
            int valid = 0;
            for (int i = 0; i < _input.Length; ++i)
            {
                if (ValidateRow(i, part))
                {
                    valid++;
                }
            }
            return valid;
        }
    }
}