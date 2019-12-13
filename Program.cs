using System;
using Aoc.Framework;

namespace Aoc
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get the command line
            string[] command = new string[3] { "execute", "2019", "13" };
            if (args.Length > 0)
            {
                command = args;
            }

            // Register all days
            Days.RegisterAll();

            // Execute command
            switch (command[0])
            {
                case "execute":
                {
                    Execute(command[1], command.Length > 2 ? command[2] : null);
                    return;
                }

                case "generate":
                {
                    Generate(command[1], command[2]);
                    return;
                }
            }
        }

        static void Execute(string year, string day)
        {
            if (year == "all")
            {
                Days.RunAll();
            }
            else
            {
                Days.RunSingle(year + "-" + day.PadLeft(2, '0'));
            }
        }

        static void Generate(string year, string day)
        {
            // Generate the code and input
            Generator.Generate(year, day);
        }
    }
}
