using System;
using System.Text;
using System.Collections.Generic;

namespace _6._Memory
{
    class Program
    {
        static string RAW = @"4	1	15	12	0	9	9	5	5	8	7	3	14	5	12	3";
        // static string RAW = @"0	2	7	0";

        static Int32[] BANKS;
        static Dictionary<string, int> HISTORY;

        static void ParseRaw()
        {            
            String[] lines = RAW.Split("\t");
            BANKS = new Int32[lines.Length];
            for (int i = 0; i < lines.Length; ++i)
            {
                BANKS[i] = Int32.Parse(lines[i]);
            }
        }

        static void PrepareHistory()
        {
            HISTORY = new Dictionary<string, int>();           
        }

        static string GenerateHash()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < BANKS.Length; ++i)
            {
                sb.Append(BANKS[i].ToString());
                sb.Append(",");
            }
            return sb.ToString();
        }

        static void PushHistory(int index)
        {
            HISTORY.Add(GenerateHash(), index);
        }

        static bool CheckHistory()
        {
            return HISTORY.ContainsKey(GenerateHash());
        }

        static int FindMax()
        {
            int max = 0;
            for (int i = 0; i < BANKS.Length; ++i)
            {
                if (BANKS[i] > BANKS[max])
                {
                    max = i;
                }
            }
            return max;
        }

        static void Reallocate()
        {
            int selected = FindMax();
            int blocks = BANKS[selected];
            BANKS[selected] = 0;
            while (blocks > 0)
            {
                selected = (selected + 1) % BANKS.Length;
                BANKS[selected] += 1;
                blocks--;
            }
        }

        static void Main(string[] args)
        {
            // Load the data
            ParseRaw();
            PrepareHistory();
            PushHistory(0);

            // Initialize the counters
            Int32 steps = 0;
            for (;;)
            {
                for (int i = 0; i < BANKS.Length; ++i)
                {
                    Console.Write("{0}\t", BANKS[i]);                    
                }
                Console.WriteLine("");                
                Reallocate();
                steps++;
                if (CheckHistory())
                {
                    break;
                }
                PushHistory(steps);
            }
            
            // Finished !
            for (int i = 0; i < BANKS.Length; ++i)
            {
                Console.Write("{0}\t", BANKS[i]);                    
            }
            Console.WriteLine("");              
            Console.WriteLine("Reallocation steps: {0}", steps);
            Console.WriteLine("Loop size: {0}", steps - HISTORY[GenerateHash()]);
        }
    }
}
