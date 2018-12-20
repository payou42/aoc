using System;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Grid;

namespace Aoc
{
    public class Day201820 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string _regex;

        public Day201820()
        {
            Codename = "2018-20";
            Name = "A Regular Map";
        }

        public void Init()
        {
            _regex = Aoc.Framework.Input.GetString(this);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                Queue<(string, Point)> queue = new Queue<(string, Point)>();
                queue.Enqueue((_regex, new Point(0,0)));

                while (queue.TryDequeue(out var from))
                {
                    bool straight = true;
                    int index = 0;
                    while (straight)
                    {
                        switch (from.Item1[index])
                        {
                            case '^':
                            {
                                // Ignore
                                index++;
                                break;
                            }

                            case '$':
                            {
                                // Finished !
                                straight = false;
                                break;
                            }

                            case 'N':
                            {
                                MoveNorth(from.Item2);
                                index++;
                                break;
                            }

                            case 'W':
                            {
                                MoveWest(from.Item2);
                                index++;
                                break;
                            }

                            case 'E':
                            {
                                MoveEast(from.Item2);
                                index++;
                                break;
                            }

                            case 'S':
                            {
                                MoveSouth(from.Item2);
                                index++;
                                break;
                            }

                            case '(':
                            {
                                // Start a new fork
                                StartFork(index, from, queue);
                                straight = false;
                                break;
                            }
                        }
                    }
                }
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                return "part2";
            }

            return "";
        }

        private void MoveNorth(Point from)
        {

        }

        private void MoveSouth(Point from)
        {

        }

        private void MoveEast(Point from)
        {

        }

        private void MoveWest(Point from)
        {

        }

        private void StartFork(int index, (string, Point) src, Queue<(string, Point)> queue)
        {
            // Look for closing parenthesis and group
            int groupIndex = index + 1;
            int level = 0;
            List<string> groups = new List<string>();

            for (int i = index + 1; i < src.Item1.Length; ++i)
            {
                switch (src.Item1[i])
                {
                    case ')':
                    {
                        if (level == 0)
                        {
                            // Matching closing parenthesis
                            groups.Add(src.Item1.Substring(groupIndex, i - groupIndex));

                            // Enqueue all next tasks
                            string remaining = src.Item1.Substring(i + 1);
                            foreach (string group in groups)
                            {
                                queue.Enqueue((group + remaining, src.Item2));
                            }
                            return;
                        }
                        else
                        {
                            level--;
                        }
                        break;
                    }

                    case '(':
                    {
                        level++;
                        break;
                    }

                    case '|':
                    {
                        if (level == 0)
                        {
                            // Add this group
                            groups.Add(src.Item1.Substring(groupIndex, i - groupIndex));

                            // Move to the next one
                            groupIndex = i + 1;
                        }
                        break;
                    }
                }
            }
        }
    }   
}