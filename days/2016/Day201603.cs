using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201603 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }        

        public Day201603()
        {
            Codename = "2016-03";
            Name = "Squares With Three Sides";
        }

        public void Init()
        {            
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                // Read the data
                string[] input = Aoc.Framework.Input.GetStringVector(this).Where(s => s != "").ToArray();
                int[][] triangles = new int[input.Length][];
                for (int i = 0; i < input.Length; ++i)
                {
                    triangles[i] = input[i].Split(" ").Select(s => s.Trim()).Where(s => s != "").Select(s => Int32.Parse(s)).ToArray();
                    Array.Sort(triangles[i]);
                }

                // Count valid
                return CountValid(triangles).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // Read the data
                string[] input = Aoc.Framework.Input.GetStringVector(this).Where(s => s != "").ToArray();
                int[][] content = new int[input.Length][];
                int[][] triangles = new int[input.Length][];
                for (int i = 0; i < input.Length; ++i)
                {
                    content[i] = input[i].Split(" ").Select(s => s.Trim()).Where(s => s != "").Select(s => Int32.Parse(s)).ToArray();
                }

                // Build the triangles
                for (int i = 0; i < content.Length; i += 3)
                {
                    for (int j = 0; j < 3; ++j)
                    {
                        triangles[i + j] = new int[3] { content[i][j], content[i + 1][j], content[i + 2][j] };
                        Array.Sort(triangles[i + j]);
                    }
                }

                // Count valid
                return CountValid(triangles).ToString();
            }

            return "";
        }

        private int CountValid(int[][] triangles)
        {
            // Count valid
            int validTriangles = 0;
            foreach (int[] triangle in triangles)
            {
                if (triangle[0] + triangle[1] > triangle[2])
                {
                    validTriangles++;
                }
            }
            return validTriangles;
        }
    }   
}