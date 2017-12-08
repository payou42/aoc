using System;

namespace _5._Maze
{
    class Program
    {        
        static Int32[] OFFSETS;

        static void ParseRaw()
        {            
            String[] lines = Input.RAW.Split("\r\n");
            OFFSETS = new Int32[lines.Length];
            for (int i = 0; i < lines.Length; ++i)
            {
                OFFSETS[i] = Int32.Parse(lines[i]);
            }            
        }

        static void Main(string[] args)
        {
            // Load the data
            ParseRaw();

            // Initialize the counters
            Int32 steps = 0;
            Int32 current = 0;
            while ((current >= 0) && (current < OFFSETS.Length))
            {
                Int32 jump = OFFSETS[current];
                if (OFFSETS[current] >= 3)
                {
                    OFFSETS[current]--;
                }
                else
                {
                    OFFSETS[current]++;
                }
                
                current = current + jump;
                
                steps++;
            }

            // Finished !
            Console.WriteLine("Exit found in {0} steps", steps);
        }
    }
}
