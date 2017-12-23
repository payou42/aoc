using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace _23._Coprocessor
{
    class Program
    {
        static void Main(string[] args)
        {            
            // Create the computer
            Cpu A = new Cpu(0);

            // Part 1
            string[] instructions = Input.CODE.Split("\r\n");
            bool exited = false;
            while (!exited)
            {
                exited = A.Execute(instructions);
            }
            Console.WriteLine("Mul count:  {0}", A.MulCount);

            // Part 2
            int result = 0;
            for (int b = 107900; b <= 124900; b += 17)
            {
                for (int d = 2; d <= Math.Sqrt(b); ++d)
                {
                    if (b % d == 0)
                    {
                        result++;
                        break;
                    }
                }
            }
            Console.WriteLine("Value in H: {0}", result);
        }
    }
}
