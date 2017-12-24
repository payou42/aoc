using System;
using System.Text;
using System.Reflection;
using System.Collections.Generic;

namespace Aoc
{
    public class Days
    {
        private static Dictionary<string, Day> _registry;

        static Days()
        {
            _registry = new Dictionary<string, Day>();
        }

        public static void Register(Type type)
        {
            Day instance = (Day)Activator.CreateInstance(type);
            _registry[instance.Codename] = instance;
        }

        public static void RegisterAll()
        {
            Assembly current = Assembly.GetExecutingAssembly();
            foreach (Type type in current.GetTypes())
            {
                Type[] interfaces = type.FindInterfaces((typeObj, criteriaObj) => typeObj.ToString() == (string)criteriaObj, "Aoc.Day");
                if (interfaces.Length > 0)
                {
                    Register(type);
                }                
            }
        }

        public static void RunAll()
        {
            foreach (Day day in _registry.Values)
            {
                RunSingle(day.Codename);
            }
        }
        
        public static void RunSingle(string codename)
        {
            int width = 80;
            Day day = _registry[codename];
            Console.WriteLine("+" + "".PadLeft(width / 2 - 1, '-').PadRight(width - 2, '-') + "+");
            Console.WriteLine("|" + day.Codename.PadLeft(width / 2 - 1 + day.Codename.Length / 2, ' ').PadRight(width - 2, ' ') + "|");
            Console.WriteLine("|" + day.Name.PadLeft(width / 2 - 1 + day.Name.Length / 2, ' ').PadRight(width - 2, ' ') + "|");
            Console.WriteLine("+" + "".PadLeft(width / 2 - 1, '-').PadRight(width - 2, '-') + "+");
            Console.WriteLine("Part 1: {0}", day.Run(Part.Part1));
            Console.WriteLine("Part 2: {0}", day.Run(Part.Part2));
            Console.WriteLine("");
        }
    }
}
