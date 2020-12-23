using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Containers;

namespace Aoc
{
    public class Day202023 : Aoc.Framework.IDay
    {
        public class Cup
        {
            public int Value { get; set; }
            
            public Cup Next { get; set; }
            
            public Cup(int v)
            {
                Value = v;
            }
        }
        
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string _input;

        public Day202023()
        {
            Codename = "2020-23";
            Name = "Crab Cups";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetString(this);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                var result = Game(_input, 0, 100);
                return GetResult(result, 1);
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                Cup cup = Game(_input, 1_000_000, 10_000_000);
                ulong res = (ulong)cup.Next.Value * (ulong)cup.Next.Next.Value;
                return res.ToString();
            }

            return "";
        }

        private int FindDestination(int start, Cup cut, int highestID)
        {
            int dest = start == 1 ? highestID: start - 1;
            int a = cut.Value;
            int b = cut.Next.Value;
            int c = cut.Next.Next.Value;
  
            while (dest == a || dest == b || dest == c)
            {
                --dest;
                if (dest <= 0)
                {
                    dest = highestID;
                }
            }

            return dest;
        }

        private string GetResult(Cup node, int startVal)
        {
            while (node.Value != startVal)
            {
                node = node.Next;
            }

            // Skip the first one
            node = node.Next;

            var str = "";            
            do
            {
                str += node.Value.ToString();
                node = node.Next;
            } while (node.Value != startVal);

            return str;
        }

        private Cup Game(string initial, int max, int rounds)
        {
            var nums = initial.ToCharArray().Select(n => (int)n - (int)'0').ToList();
            int highestID = nums.Max();

            if (max > 0)
            {
                nums.AddRange(Enumerable.Range(highestID + 1, max - nums.Count));
            }

            Cup[] index = new Cup[max == 0 ? 10 : max + 1];
            Cup start = new Cup(nums.First());
            index[nums.First()] = start;
            Cup prev = start;            
            foreach (int v in nums.Skip(1))
            {
                Cup n = new Cup(v);
                index[v] = n;
                prev.Next = n;
                prev = n;
                if (v > highestID)
                {
                    highestID = v;
                }
            }

            prev.Next = start;            
            Cup curr = start;
            for (int j = 0; j < rounds; j++)
            {
                // Remove the three times after curr from the list
                Cup cut = curr.Next;
                curr.Next = cut.Next.Next.Next;
                
                // Find the val where we want to insert the cut nodes
                int destVal = FindDestination(curr.Value, cut, highestID);
                Cup ip = index[destVal];                
                Cup ipn = ip.Next;
                Cup tail = cut.Next.Next;
                tail.Next = ipn;
                ip.Next = cut;                
                curr = curr.Next;
            }

            return index[1];
        }
    }   
}