using System;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common.Geometry;

namespace Aoc
{
    public class Day201719 : Aoc.Framework.IDay
    {
        public class Tubes
        {
            private readonly string[] _network = null;

            private Point _position;

            private Direction _direction = Direction.Count;

            private string _letters = null;  

            private readonly int _width = 0;

            private readonly int _height = 0;

            private int _steps = 0;

            public string Path 
            {
                get
                {
                    return _letters;
                }
            }

            public int Steps
            {
                get
                {
                    return _steps;
                }
            }

            public Tubes(string[] network)
            {
                _network = network;
                _width = _network[0].Length;
                _height = _network.Length;
            }

            public void Walk()
            {
                // Init position and direction
                Init();

                // Run the maze
                while (_direction != Direction.Count)
                {
                    while (Next(_direction))
                    {
                        Move();
                    }
                    _direction = Turn();
                }
            }

            private void Init()
            {
                _position.X = _network[0].IndexOf("|");
                _position.Y = 0;
                _direction = Direction.Down;
                _letters = "";
                _steps = 1;
            }

            private bool Next(Direction direction)
            {
                switch (direction)
                {
                    case Direction.Left:
                    {
                        return ((_position.X > 0) && (_network[_position.Y][_position.X - 1] != ' '));
                    }

                    case Direction.Right:
                    {
                        return ((_position.X < _width - 1) && (_network[_position.Y][_position.X + 1] != ' '));
                    }

                    case Direction.Up:
                    {
                        return ((_position.Y > 0) && (_network[_position.Y - 1][_position.X] != ' '));
                    }

                    case Direction.Down:
                    {
                        return ((_position.Y < _height - 1) && (_network[_position.Y + 1][_position.X] != ' '));
                    }            
                }
                return false;
            }

            private void Move()
            {
                _steps++;
                _position = Board2D<Int64>.MoveForward(_position, _direction, 1, true);
                char c = _network[_position.Y][_position.X];
                if ((c >= 'A') && (c <= 'Z'))
                {
                    _letters += c;
                }
            }

            private Direction Turn()
            {
                switch (_direction)
                {
                    case Direction.Left:
                    case Direction.Right:
                    {
                        if (Next(Direction.Up))
                        {
                            return Direction.Up;
                        }
                        if (Next(Direction.Down))
                        {
                            return Direction.Down;
                        }
                        return Direction.Count;
                    }

                    case Direction.Up:
                    case Direction.Down:
                    {
                        if (Next(Direction.Left))
                        {
                            return Direction.Left;
                        }
                        if (Next(Direction.Right))
                        {
                            return Direction.Right;
                        }
                        return Direction.Count;
                    }
                }
                return Direction.Count;
            }
        }
        
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private Tubes _tubes;

        public Day201719()
        {
            Codename = "2017-19";
            Name = "A Series of Tubes";
        }

        public void Init()
        {
            _tubes = new Tubes(Aoc.Framework.Input.GetStringVector(this));
            _tubes.Walk();
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return _tubes.Path.ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                return _tubes.Steps.ToString();
            }

            return "";
        }
    }   
}