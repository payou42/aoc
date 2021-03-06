using System;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Geometry;

namespace Aoc
{
    public class Day201820 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        public enum Cell
        {
            Wall = 0,
            Door = 1,
            Room = 2
        };

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
                Board2D<Cell> map = BuildMap();
                // Dump(map);
                Board2D<int>  pathes = BuildPathes(map);
                return (pathes.Cells.Max(cell => cell.Item2) - 1).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                Board2D<Cell> map = BuildMap();
                Board2D<int>  pathes = BuildPathes(map);
                return (pathes.Cells.Count(cell => cell.Item2 > 1000)).ToString();
            }

            return "";
        }

        private Board2D<int> BuildPathes(Board2D<Cell> map)
        {
            Board2D<int> pathes = new Board2D<int>();
            Queue<Point> queue= new Queue<Point>();
            pathes[0, 0] = 1;
            queue.Enqueue(new Point(0, 0));

            while (queue.TryDequeue(out var point))
            {
                EnqueueIfValid(point.X, point.Y, 0, -1, pathes, map, queue);
                EnqueueIfValid(point.X, point.Y, 0, +1, pathes, map, queue);
                EnqueueIfValid(point.X, point.Y, -1, 0, pathes, map, queue);
                EnqueueIfValid(point.X, point.Y, +1, 0, pathes, map, queue);                
            }
            return pathes;
        }

        private void EnqueueIfValid(int x, int y, int xoffset, int yoffset, Board2D<int> pathes, Board2D<Cell> map, Queue<Point> queue)
        {
            if (map[x + xoffset, y + yoffset] == Cell.Door && pathes[x + 2 * xoffset, y + yoffset * 2] <= 0)
            {
                pathes[x + 2 * xoffset, y + yoffset * 2] = pathes[x, y] + 1;
                queue.Enqueue(new Point(x + 2 * xoffset, y + yoffset * 2));
            }
        }

        private Board2D<Cell> BuildMap()
        {
            Board2D<Cell> map = new Board2D<Cell>();
            Queue<(string, Point)> queue = new Queue<(string, Point)>();
            queue.Enqueue((_regex, new Point(0, 0)));
            map[0, 0] = Cell.Room;

            while (queue.TryDequeue(out var from))
            {
                bool straight = true;
                int index = 0;
                Point position = from.Item2;

                while (straight && index < from.Item1.Length)
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
                            position = Move(position, map, 0, -1);
                            index++;
                            break;
                        }

                        case 'W':
                        {
                            position = Move(position, map, -1, 0);
                            index++;
                            break;
                        }

                        case 'E':
                        {
                            position = Move(position, map, 1, 0);
                            index++;
                            break;
                        }

                        case 'S':
                        {
                            position = Move(position, map, 0, 1);
                            index++;
                            break;
                        }

                        case '(':
                        {
                            // Start a new fork
                            StartFork(index, from.Item1, position, queue);
                            straight = false;
                            break;
                        }
                    }
                }
            }

            return map;
        }

        private Point Move(Point from, Board2D<Cell> map, int xoffset, int yoffset)
        {
            map[from.X + xoffset, from.Y + yoffset] = Cell.Door;
            map[from.X + 2*xoffset, from.Y + 2*yoffset] = Cell.Room;
            return new Point(from.X + 2*xoffset, from.Y + 2*yoffset);
        }

        private void StartFork(int index, string regex, Point from, Queue<(string, Point)> queue)
        {
            // Look for closing parenthesis and group
            int groupIndex = index + 1;
            int level = 0;
            List<string> groups = new List<string>();

            for (int i = index + 1; i < regex.Length; ++i)
            {
                switch (regex[i])
                {
                    case ')':
                    {
                        if (level == 0)
                        {
                            // Matching closing parenthesis
                            string remaining = regex.Substring(i + 1);
                            
                            // Optimisation based on the description of the problem
                            if (groupIndex == i)
                            {
                                // This is an empty group
                                // We assume that ALL pathes from the groups are loop, as suggested by the description
                                // So instead of forking the regex for each group, we play each group individually,
                                // then resume as if all the groups were not there
                                foreach (string group in groups)
                                {
                                    queue.Enqueue((group, from));
                                }
                                queue.Enqueue((remaining, from));
                            }
                            else
                            {
                                // Create the last group
                                groups.Add(regex[groupIndex..i]);

                                // Enqueue all next tasks
                                foreach (string group in groups)
                                {
                                    queue.Enqueue((group + remaining, from));
                                }
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
                            groups.Add(regex[groupIndex..i]);

                            // Move to the next one
                            groupIndex = i + 1;
                        }
                        break;
                    }
                }
            }
        }

        private void Dump(Board2D<Cell> map)
        {
            char[] walls = new char[] { '═', '═', '═', '═', '║', '╝', '╚', '╩', '║', '╗', '╔', '╦', '║', '╣', '╠', '╬'};
            int xmin = map.Cells.Min(cell => cell.Item1.X) - 1;
            int xmax = map.Cells.Max(cell => cell.Item1.X) + 1;
            int ymin = map.Cells.Min(cell => cell.Item1.Y) - 1;
            int ymax = map.Cells.Max(cell => cell.Item1.Y) + 1;

            for (int y = ymin; y <= ymax; ++y)
            {
                for (int x = xmin; x <= xmax; ++x)
                {
                    char c = '#';
                    switch (map[x, y])
                    {
                        case Cell.Room: c = ' '; break;                        
                        case Cell.Door: c = map[x - 1, y] == Cell.Room ? '│' : '─'; break;
                        case Cell.Wall:
                        {
                            int wallType = (x > xmin && map[x - 1, y] == Cell.Wall ? 1 : 0) 
                                         + (x < xmax && map[x + 1, y] == Cell.Wall ? 2 : 0) 
                                         + (y > ymin && map[x, y - 1] == Cell.Wall ? 4 : 0) 
                                         + (y < ymax && map[x, y + 1] == Cell.Wall ? 8 : 0);
                            c = walls[wallType];
                            break;
                        }
                    }

                    Console.Write(c);
                }
                System.Console.WriteLine("");
            }
        }
    }   
}