using System;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Grid;

namespace Aoc
{
    public class Day202012 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string[] _input;

        public Day202012()
        {
            Codename = "2020-12";
            Name = "Rain Risk";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetStringVector(this);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                Direction currentDir = Direction.Right;
                Point currentPos = new Point(0, 0);

                foreach (string s in _input)
                {
                    char command =  s[0];
                    int distance = int.Parse(s[1..]);
                    switch (command)
                    {
                        case 'N': currentPos = Board2D<long>.MoveForward(currentPos, Direction.Up, distance); break;
                        case 'S': currentPos = Board2D<long>.MoveForward(currentPos, Direction.Down, distance); break;
                        case 'E': currentPos = Board2D<long>.MoveForward(currentPos, Direction.Right, distance); break;
                        case 'W': currentPos = Board2D<long>.MoveForward(currentPos, Direction.Left, distance); break;
                        case 'F': currentPos = Board2D<long>.MoveForward(currentPos, currentDir, distance); break;
                        case 'L': currentDir = Board2D<long>.Turn(currentDir, Direction.Left, distance / 90); break;
                        case 'R': currentDir = Board2D<long>.Turn(currentDir, Direction.Right, distance / 90); break;
                    }
                }

                return Board2D<long>.GetDistance(currentPos).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                Point currentPos = new Point(0, 0);
                Point waypoint = new Point(10, 1);

                foreach (string s in _input)
                {
                    char command =  s[0];
                    int distance = int.Parse(s[1..]);
                    switch (command)
                    {
                        case 'N': waypoint = Board2D<long>.MoveForward(waypoint, Direction.Up, distance); break;
                        case 'S': waypoint = Board2D<long>.MoveForward(waypoint, Direction.Down, distance); break;
                        case 'E': waypoint = Board2D<long>.MoveForward(waypoint, Direction.Right, distance); break;
                        case 'W': waypoint = Board2D<long>.MoveForward(waypoint, Direction.Left, distance); break;
                        case 'L': waypoint = Board2D<long>.Rotate(waypoint, Direction.Left, distance / 90); break;
                        case 'R': waypoint = Board2D<long>.Rotate(waypoint, Direction.Right, distance / 90); break;
                        case 'F': currentPos = new Point(currentPos.X + (distance * waypoint.X), currentPos.Y + (distance * waypoint.Y)); break;
                    }

                }

                return Board2D<long>.GetDistance(currentPos).ToString();
            }

            return "";
        }
    }   
}