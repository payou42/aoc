using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201716 : Aoc.Framework.IDay
    {
        public class Dance
        {
            private readonly char[] _programs = null;

            private int _start = 0;

            public Dance(int size)
            {
                // Init the "dancers"
                _programs = new char[size];
                for (int i = 0; i < size; ++i)
                {
                    _programs[i] = (char)('a' + i);
                }

                // Instead of moving a lot of stuff in the array, let's keep a "start" offset
                _start = 0;
            }

            public void Spin(int offset)
            {
                // Just move the _start pointer
                _start = (_start + _programs.Length - offset) % _programs.Length;
            }

            public void Exchange(int i, int j)
            {
                char temp = _programs[(i + _start) % _programs.Length];
                _programs[(i + _start) % _programs.Length] = _programs[(j + _start) % _programs.Length];
                _programs[(j + _start) % _programs.Length] = temp;
            }

            public void Partner(char a, char b)
            {
                int x = 0;
                int y = 0;
                for (int i = 0; i < _programs.Length; ++i)
                {
                    if (_programs[i] == a)
                    {
                        x = i;
                    }
                    if (_programs[i] == b)
                    {
                        y = i;
                    }                
                }
                char temp = _programs[x];
                _programs[x] = _programs[y];
                _programs[y] = temp;
            }

            public override String ToString()
            {
                string output = "";
                for (int i = 0; i < _programs.Length; ++i)
                {
                    output += _programs[(i + _start) % _programs.Length];
                }
                return output;
            }
        }

        public string Codename { get; private set; }

        public string Name { get; private set; }

        public Day201716()
        {
            Codename = "2017-16";
            Name = "Permutation Promenade";
        }

        public void Init()
        {
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return Promenade(1);
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                return Promenade(1000000000);
            }

            return "";
        }

        private string Promenade(int target)
        {
            // Create the dance
            Dance dance = new Dance(16);
            string[] moves = Aoc.Framework.Input.GetStringVector(this, ",");

            // Build an history of positions
            Dictionary<string, int> positions = new Dictionary<string, int>
            {
                [dance.ToString()] = 0
            };

            // Repeat dance until exhaustion
            int counter = 0;
            while (counter < target)
            {
                // Dance ~
                counter++;
                DanceSingle(dance, moves);

                // Look for a cycle
                string hash = dance.ToString();
                if (positions.ContainsKey(hash))
                {
                    // Found a cycle, skip forward
                    int length = counter - positions[hash];

                    // Skip the cycle as many times as possible
                    counter = target - ((target - counter) % length);
                }
            }
            return dance.ToString();           
        }

        private void DanceSingle(Dance dance, string[] moves)
        {
            foreach (string move in moves)
            {
                switch (move[0])
                {
                    case 's':
                    {
                        dance.Spin(int.Parse(move.Substring(1)));
                        break;
                    }
                    case 'x':
                    {
                        string[] p = move.Substring(1).Split("/");
                        dance.Exchange(int.Parse(p[0]), int.Parse(p[1]));
                        break;
                    }
                    case 'p':
                    {
                        string[] p = move.Substring(1).Split("/");
                        dance.Partner(p[0][0], p[1][0]);
                        break;
                    }                    
                }
            }
        }
    }   
}