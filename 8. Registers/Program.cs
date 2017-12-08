using System;
using System.Text;
using System.Collections.Generic;

namespace _8._Registers
{
    class Program
    {
        static Registers REGISTERS = new Registers();
        static Int32 Highest = Int32.MinValue;
        
        static bool CheckCondition(string register, string op, Int32 value)
        {
            Int32 r = REGISTERS[register];
            switch (op)
            {
                case "==": return r == value;
                case "!=": return r != value;
                case ">": return r > value;
                case "<": return r < value;
                case ">=": return r >= value;
                case "<=": return r <= value;
                default: return true;
            }
        }

        static void ApplyOperation(string register, string op, Int32 value)
        {
            switch (op)
            {
                case "inc":
                {
                    REGISTERS[register] += value;
                    return;
                }

                case "dec":
                {
                    REGISTERS[register] -= value;
                    return;
                }
            }
        }

        static void ProcessLine(string line)
        {
            string[] items = line.Split(" ");

            if (CheckCondition(items[4], items[5], Int32.Parse(items[6])))
            {
                ApplyOperation(items[0], items[1], Int32.Parse(items[2]));
                Highest = Math.Max(Highest, REGISTERS.GetLargest());
            }            
        }
        
        static void ProcessFile()
        {
            String[] lines = Input.RAW.Split("\r\n");
            for (int i = 0; i < lines.Length; ++i)
            {
                ProcessLine(lines[i]);
            }
        }

        static void Main(string[] args)
        {
            // Process the data
            ProcessFile();

            // Find the largest
            int largest = REGISTERS.GetLargest();
            Console.WriteLine("Largest final register: {0}", largest);
            Console.WriteLine("Largest alltime register: {0}", Highest);
        }
    }
}
