using System;
using System.Text;
using System.Drawing;
using System.Collections.Generic;

namespace _19._Tubes
{
    class Tubes
    {
        private string[] _network = null;
        private Point _position;
        private Direction _direction = Direction.NONE;
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

        public Tubes(string input)
        {
            _network = input.Split("\r\n");
            _width = _network[0].Length;
            _height = _network.Length;
        }

        public void Walk()
        {
            // Init position and direction
            Init();

            // Run the maze
            while (_direction != Direction.NONE)
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
            _direction = Direction.DOWN;
            _letters = "";
            _steps = 1;
        }

        private bool Next(Direction direction)
        {
            switch (direction)
            {
                case Direction.LEFT:
                {
                    return ((_position.X > 0) && (_network[_position.Y][_position.X - 1] != ' '));
                }

                case Direction.RIGHT:
                {
                    return ((_position.X < _width - 1) && (_network[_position.Y][_position.X + 1] != ' '));
                }

                case Direction.UP:
                {
                    return ((_position.Y > 0) && (_network[_position.Y - 1][_position.X] != ' '));
                }

                case Direction.DOWN:
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
                case Direction.LEFT:
                {
                    _position.X--;
                    break;
                }

                case Direction.RIGHT:
                {
                    _position.X++;
                    break;
                }

                case Direction.UP:
                {
                    _position.Y--;
                    break;
                }

                case Direction.DOWN:
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
                case Direction.LEFT:
                case Direction.RIGHT:
                {
                    if (Next(Direction.UP))
                    {
                        return Direction.UP;
                    }
                    if (Next(Direction.DOWN))
                    {
                        return Direction.DOWN;
                    }
                    return Direction.NONE;
                }

                case Direction.UP:
                case Direction.DOWN:
                {
                    if (Next(Direction.LEFT))
                    {
                        return Direction.LEFT;
                    }
                    if (Next(Direction.RIGHT))
                    {
                        return Direction.RIGHT;
                    }
                    return Direction.NONE;
                }
            }
            return Direction.NONE;
        }
    }
}