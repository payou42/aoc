using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201716 : Aoc.Framework.Day
    {
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
            Dictionary<string, int> positions = new Dictionary<string, int>();
            positions[dance.ToString()] = 0;

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
                    int from = counter;
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