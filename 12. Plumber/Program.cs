using System;
using System.Text;
using System.Collections.Generic;

namespace _11._Hex
{
    class Program
    {
        static Dictionary<int, List<int>> LINKS;

        static void ParseRaw()
        {
            LINKS = new Dictionary<int, List<int>>();
            String[] lines = Input.RAW.Split("\r\n");        
            for (int i = 0; i < lines.Length; ++i)
            {
                ParseLine(lines[i]);
            }
        }

        static void ParseLine(string line)
        {
            string[] items = line.Split(" <-> ");
            int id = Int32.Parse(items[0]);
            LINKS[id] = new List<int>();

            string[] links = items[1].Split(", ");
            for (int i = 0; i < links.Length; ++i)
            {
                int link = Int32.Parse(links[i]);
                LINKS[id].Add(link);
            }
        }

        static void BuildGroup(HashSet<int> group, int id)
        {
            if (!group.Contains(id))
            {
                group.Add(id);
                foreach (int link in LINKS[id])
                {
                    BuildGroup(group, link);
                }
            }
        }
       
        static void Main(string[] args)
        {
            // Prepare the grig
            ParseRaw();

            // Build the group of id 0
            HashSet<int> group = new HashSet<int>();
            Console.WriteLine("Group 0 has {0} programs", group.Count);

            // Count the number of group
            int groupCount = 0;
            HashSet<int> all = new HashSet<int>();
            foreach (int id in LINKS.Keys)
            {
                if (!all.Contains(id))
                {
                    groupCount++;
                    BuildGroup(all, id);
                }
            }
            Console.WriteLine("Number of groups: {0}", groupCount);            
        }
    }
}
