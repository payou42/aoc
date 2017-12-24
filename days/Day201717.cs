using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Aoc
{
    public class Day201717 : Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        public Day201717()
        {            
            Codename = "2017-17";
            Name = "Spinlock";
        }

        public string Run(Part part)
        {
            int step = Input.GetInt(this);
            if (part == Part.Part1)
            {
                List<int> data = new List<int>();
                data.Add(0);
                int cursor = 0;
                for (int i = 1; i <= 2017; ++i)
                {
                    cursor = ((cursor + step) % data.Count()) + 1;
                    data.Insert(cursor, i);
                }           
                return (data[(cursor + 1) % data.Count()]).ToString();
            }

            if (part == Part.Part2)
            {
                int cursor    = 0;
                int zeroIndex = 0;
                int result    = 0;
                for (int i = 1; i <= 50000000; ++i)
                {
                    cursor = ((cursor + step) % i) + 1;
                    if (cursor == zeroIndex + 1)
                    {
                        result = i;
                    }
                    if (cursor <= zeroIndex)
                    {
                        zeroIndex++;
                    }
                }
                return result.ToString();
            }

            return "";
        }
    }   
}