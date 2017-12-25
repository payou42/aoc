using System;
using System.Text;
using System.Drawing;
using System.Collections.Generic;

namespace Aoc
{
    public class Tubes
    {
        private string[] _network = null;

        private Point _position;

        private Direction _direction = Direction.Count;

        private string _letters = null;  

        private int _width = 0;

        private int _height = 0;

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
            switch (_direction)
            {
                case Direction.Left:
                {
                    _position.X--;
                    break;
                }

                case Direction.Right:
                {
                    _position.X++;
                    break;
                }

                case Direction.Up:
                {
                    _position.Y--;
                    break;
                }

                case Direction.Down:
                {
                    _position.Y++;
                    break;
                }
            }

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
}