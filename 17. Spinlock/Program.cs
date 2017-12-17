using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace _17._Spinlock
{
    class Program
    {
        static List<int> DATA = null;

        static int ProcessInput()
        {
            DATA = new List<int>();
            DATA.Add(0);
            int cursor = 0;
            for (int i = 1; i <= Input.FINAL; ++i)
            {
                cursor = ((cursor + Input.STEPS) % DATA.Count()) + 1;
                DATA.Insert(cursor, i);
            }           
            return DATA[(cursor + 1) % DATA.Count()];            
        }

        static int ProcessAngry()
        {
            // Since there's too much data, we're gonna fake their insertion
            int cursor    = 0;
            int zeroIndex = 0;
            int result    = 0;
            for (int i = 1; i <= Input.ANGRY; ++i)
            {
                cursor = ((cursor + Input.STEPS) % i) + 1;
                if (cursor == zeroIndex + 1)
                {
                    result = i;
                }
                if (cursor <= zeroIndex)
                {
                    zeroIndex++;
                }
            }
            return result;
        }

        static void Main(string[] args)
        {
            // Create the dance
            Console.WriteLine("Spinlock position:    {0}", ProcessInput());

            // Find the zero
            Console.WriteLine("Angry spinlock value: {0}", ProcessAngry());
        }
    }
}
