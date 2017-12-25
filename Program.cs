using System;

namespace Aoc
{
    class Program
    {
        static void Main(string[] args)
        {
            string current = "all";
            if (args.Length > 0)
            {
                current = args[0];
            }

            Days.RegisterAll();

            if (current == "all")
            {
                Days.RunAll();
            }
            else
            {
                Days.RunSingle(current);
            }            
        }
    }
}
