using System;
using System.Text;
using System.Collections.Generic;

namespace _10._Knot
{
    class Program
    {
        static int[] LENGTHES;

        static void ParseRaw()
        {
            Byte[] input = Encoding.ASCII.GetBytes(Input.RAW);
            LENGTHES = new int[input.Length + Input.FOOTER.Length];
            for (int i = 0; i <input.Length; ++i)
            {
                LENGTHES[i] = input[i];
            }
            for (int i = 0; i < Input.FOOTER.Length; ++i)
            {
                LENGTHES[i + input.Length] = Input.FOOTER[i];
            }
        }

       
        static void Main(string[] args)
        {
            // Read the data
            ParseRaw();

            // Prepare the content
            Knot knot = new Knot(Input.SIZE);            

            // Twist and shout
            int current = 0;
            int skip = 0;
            for (int round = 0; round < Input.ROUNDS; ++round)
            {
                for (int i = 0; i < LENGTHES.Length; ++i)
                {
                    knot.Twist(current, LENGTHES[i]);
                    current = (current + skip + LENGTHES[i]) % Input.SIZE;
                    skip += 1;
                }   
            }
            Console.WriteLine("Checksum: {0}", knot.GetDenseHash());
        }
    }
}
