using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace _16._Promenade
{
    class Program
    {
        static Dance DANCE = null;

        static void ProcessInput(string input)
        {
            string[] moves = input.Split(",");
            foreach(string move in moves)
            {
                switch (move[0])
                {
                    case 's':
                    {
                        DANCE.Spin(int.Parse(move.Substring(1)));
                        break;
                    }
                    case 'x':
                    {
                        string[] p = move.Substring(1).Split("/");
                        DANCE.Exchange(int.Parse(p[0]), int.Parse(p[1]));
                        break;
                    }
                    case 'p':
                    {
                        string[] p = move.Substring(1).Split("/");
                        DANCE.Partner(p[0][0], p[1][0]);
                        break;
                    }                    
                }
            }
        }

        static void Main(string[] args)
        {
            // Create the dance
            DANCE = new Dance(Input.SIZE);
            string before = DANCE.ToString();

            // Build an history of positions
            Dictionary<string, int> positions = new Dictionary<string, int>();
            positions[DANCE.ToString()] = 0;

            // Look for a cycle
            int counter = 0;
            int target = Input.TARGET;
            while (counter < target)
            {
                counter++;
                ProcessInput(Input.RAW);
                string hash = DANCE.ToString();
                if (positions.ContainsKey(hash))
                {
                    // Found a cycle, skip forward
                    int from = counter;
                    int length = counter - positions[hash];
                    while (target - counter > length)
                    {
                        counter += length;
                    }

                    Console.WriteLine("Skipped {0} moves because of a cycle of length {1}", counter - from, length);
                }
            }

            Console.WriteLine("Dancer positions: {0}", DANCE);
        }
    }
}
