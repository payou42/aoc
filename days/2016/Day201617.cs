using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Crypto;

namespace Aoc
{
    public class Day201617 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string _input;

        private readonly int _width = 4;

        private readonly int _height = 4;

        public Day201617()
        {
            Codename = "2016-17";
            Name = "Two Steps Forward";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetString(this);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return Scan(true);
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                return Scan(false).Length.ToString();
            }

            return "";
        }

        private string Scan(bool shortest = true)
        {
            // Keep the longest path
            string bestPath = "";

            // Prepare the moves queue with initial position
            Queue<(string, int, int)> queue = new Queue<(string, int, int)>();
            queue.Enqueue(("", 0, 0));

            // Scan all possible way
            while (queue.TryDequeue(out var state))
            {
                // Calculate current hash
                string hash = Md5.ComputeString(_input + state.Item1);

                // Enqueue all moves from there
                if ((state.Item3 > 0) && (hash[0] >= 'b'))
                {
                    // We can go up
                    queue.Enqueue((state.Item1 + "U", state.Item2, state.Item3 - 1));
                }

                if ((state.Item3 < _height - 1) && (hash[1] >= 'b'))
                {
                    // We can go down
                    if (state.Item2 == _width - 1 && state.Item3 == _height - 2)
                    {
                        if (shortest)
                        {
                            return state.Item1 + "D";
                        }
                        else
                        {
                            if (bestPath.Length <= state.Item1.Length)
                            {
                                bestPath = state.Item1 + "D";
                            }
                        }
                    }
                    else
                    {
                        queue.Enqueue((state.Item1 + "D", state.Item2, state.Item3 + 1));
                    }
                }

                if ((state.Item2 > 0) && (hash[2] >= 'b'))
                {
                    // We can go left
                    queue.Enqueue((state.Item1 + "L", state.Item2 - 1, state.Item3));
                }

                if ((state.Item2 < _width - 1) && (hash[3] >= 'b'))
                {
                    // We can go right
                    if (state.Item2 == _width - 2 && state.Item3 == _height - 1)
                    {
                        if (shortest)
                        {
                            return state.Item1 + "R";
                        }
                        else
                        {
                            if (bestPath.Length <= state.Item1.Length)
                            {
                                bestPath = state.Item1 + "R";
                            }
                        }
                    }
                    else
                    {
                        queue.Enqueue((state.Item1 + "R", state.Item2 + 1, state.Item3));
                    }
                }
            }
            return bestPath;
        }
    }   
}