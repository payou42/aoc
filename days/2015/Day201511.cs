using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201511 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string _input;

        public Day201511()
        {
            Codename = "2015-11";
            Name = "Corporate Policy";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetString(this);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                char[] password = _input.ToCharArray();
                while (NextPassword(password))
                {
                    if (IsPasswordValid(password))
                    {
                        return string.Join("", password);
                    }
                }
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                char[] password = _input.ToCharArray();
                while (NextPassword(password))
                {
                    if (IsPasswordValid(password))
                    {
                        break;
                    }
                }
                while (NextPassword(password))
                {
                    if (IsPasswordValid(password))
                    {
                         return string.Join("", password);
                    }
                }
            }

            return "";
        }

        private bool NextPassword(char[] password)
        {
            return IncrementPassword(password, password.Length - 1);
        }

        private bool IncrementPassword(char[] password, int position)
        {
            if (password[position] == 'z')
            {
                password[position] = 'a';
                if (position == 0)
                {
                    return false;
                }
                return IncrementPassword(password, position - 1);
            }
            password[position]++;
            return true;
        }

        private bool IsPasswordValid(char[] password)
        {
            // First requirement
            bool firstRequirement = false;
            for (int i = 0; i < password.Length - 2; ++i)
            {
                if ((password[i + 1] == (char)(1 + password[i])) && (password[i + 2] == (char)(2 + password[i])))
                {
                    firstRequirement = true;
                    break;
                }
            }

            if (!firstRequirement)
            {
                return false;
            }

            // Second requirement
            for (int i = 0; i < password.Length; ++i)
            {
                if ((password[i] == 'i') || (password[i] == 'o') || (password[i] == 'l'))
                {
                    return false;
                }
            }

            // Third requirement
            int pairCount = 0;
            for (int i = 0; i < password.Length - 1; ++i)
            {
                if (password[i] == password[i + 1])
                {
                    i++;
                    pairCount++;
                    if (pairCount == 2)
                    {
                        break;
                    }
                }
            }

            return pairCount >= 2;
        }
    }   
}