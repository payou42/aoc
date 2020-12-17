using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using Aoc.Common;
using Aoc.Common.Grid;

namespace Aoc
{
    public class Day201813 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        public class Track
        {
            public List<Direction> Directions;

            public bool IsIntersection
            {
                get
                {
                    return Directions.Count == 4;
                }
            }

            public Track(List<Direction> directions)
            {
                Directions = directions;
            }

            public Direction GetNextDirection(Direction from)
            {
                Direction opposite = Board2D<Track>.Turn(from, Direction.Down);
                Direction next = Directions.Where(d => d != opposite).First();
                return next;
            }
        }

        public class Cart
        {
            public static Direction[] DirectionChooser = new Direction[] { Direction.Left, Direction.Up, Direction.Right };

            public Direction Direction { get; set; }

            public int DirectionIndex { get; set; }

            public Point Move(Point from, Board2D<Track> tracks)
            {
                // Move the cart
                Point next = Board2D<Track>.MoveForward(from, Direction, 1, true);

                // Change the direction if needed
                Track cell = tracks[next.X, next.Y];
                if (cell.IsIntersection)
                {
                    Direction = Board2D<Track>.Turn(Direction, DirectionChooser[DirectionIndex]);
                    DirectionIndex = (DirectionIndex + 1) % DirectionChooser.Length;
                }
                else
                {
                    // We need to follow the track
                    Direction = cell.GetNextDirection(Direction);
                }
                
                return next;
            }
        }

        private Board2D<Track> _tracks;

        private Board2D<Cart> _carts;

        public Day201813()
        {
            Codename = "2018-13";
            Name = "Mine Cart Madness";
        }

        public void Init()
        {
            _tracks = new Board2D<Track>();
            _carts = new Board2D<Cart>();
            string[] input = Aoc.Framework.Input.GetStringVector(this);
            for (int y = 0; y < input.Length; ++y)
            {
                for (int x = 0; x < input[y].Length; ++x)
                {
                    switch (input[y][x])
                    {
                        case '|':
                        {
                            _tracks[x, y] = new Track(new List<Direction> { Direction.Up, Direction.Down });
                            break;
                        }

                        case '^':
                        {
                            _tracks[x, y] = new Track(new List<Direction> { Direction.Up, Direction.Down });
                            _carts[x, y] = new Cart { Direction = Direction.Up };
                            break;
                        }

                        case 'v':
                        {
                            _tracks[x, y] = new Track(new List<Direction> { Direction.Up, Direction.Down });
                            _carts[x, y] = new Cart { Direction = Direction.Down };
                            break;
                        }

                        case '-':
                        {
                            _tracks[x, y] = new Track(new List<Direction> { Direction.Left, Direction.Right });
                            break;
                        }

                        case '>':
                        {
                            _tracks[x, y] = new Track(new List<Direction> { Direction.Left, Direction.Right });
                            _carts[x, y] = new Cart { Direction = Direction.Right };
                            break;
                        }

                        case '<':
                        {
                            _tracks[x, y] = new Track(new List<Direction> { Direction.Left, Direction.Right });
                            _carts[x, y] = new Cart { Direction = Direction.Left };
                            break;
                        }

                        case '/':
                        {
                            if (_tracks[x - 1, y] != null && _tracks[x - 1, y].Directions.Contains(Direction.Right))
                            {
                                _tracks[x, y] = new Track(new List<Direction> { Direction.Left, Direction.Up });
                            }
                            else
                            {
                                _tracks[x, y] = new Track(new List<Direction> { Direction.Down, Direction.Right });
                            }
                            break;
                        }

                        case '\\':
                        {
                            if (_tracks[x - 1, y] != null && _tracks[x - 1, y].Directions.Contains(Direction.Right))
                            {
                                _tracks[x, y] = new Track(new List<Direction> { Direction.Left, Direction.Down });
                            }
                            else
                            {
                                _tracks[x, y] = new Track(new List<Direction> { Direction.Up, Direction.Right });
                            }
                            break;
                        }

                        case '+':
                        {
                            _tracks[x, y] = new Track(new List<Direction> { Direction.Left, Direction.Right, Direction.Up, Direction.Down });
                            break;
                        }
                    }
                }
            }
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                Point? collision = null;
                while (collision == null)
                {
                    collision = Tick(_carts);
                }
                return $"{collision.Value.X},{collision.Value.Y}";
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                while (_carts.Values.Count > 1)
                {
                    Tick(_carts);
                }
            }

            var cell = _carts.Cells.First();
            return $"{cell.Item1.X},{cell.Item1.Y}";
        }

        private Point? Tick(Board2D<Cart> carts)
        {
            List<Cart> crashed = new List<Cart>();
            Point? collision = null;
            foreach (var cell in carts.Cells.OrderBy(c => c.Item1.Y).ThenBy(c => c.Item1.X).ToList())
            {
                // Ignore it if it's crashed
                if (crashed.Contains(cell.Item2))
                {
                    continue;                    
                }

                // Move the cart
                Point position = cell.Item2.Move(cell.Item1, _tracks);

                // Check if it's colliding with another one
                if (carts[position.X, position.Y] != null)
                {
                    // Remove the carts
                    crashed.Add(carts[position.X, position.Y]);
                    carts.Remove(cell.Item1.X, cell.Item1.Y);
                    carts.Remove(position.X, position.Y);
                    collision = (collision == null) ? position : collision;
                }
                else
                {
                    // Set the new cart int the board
                    carts.Remove(cell.Item1.X, cell.Item1.Y);
                    carts[position.X, position.Y] = cell.Item2;
                }
            }
            return collision;
        }
    }   
}