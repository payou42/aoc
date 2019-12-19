using System;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Intrinsics.X86;
using Aoc.Common;
using Aoc.Common.Graphes;
using Aoc.Common.Grid;

namespace Aoc
{
    public class Day201918 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private Board<char> _board;

        private Dictionary<char, Point> _items;

        private HashSet<long> _history;

        public struct State
        {
            public State (Point initial)
            {
                Position = initial;
                Keys = 0;
                Distance = 0;
            }

            public State(State other, Point position)
            {
                Position = position;
                Keys = other.Keys;
                Distance = other.Distance + 1;
            }

            public void AddKey(char k)
            {
                Keys |= (uint)1 << (int)(k - 'a');
            }

            public bool ContainsKey(char k)
            {
                return (Keys & (uint)1 << (int)(k - 'a')) != 0;
            }

            public uint CountKeys()
            {
                return Popcnt.PopCount(Keys);
            }

            public Point Position { get; set; }

            public uint Keys { get; private set; }

            public long Distance { get; set; }

            public long GetHashLong()
            {
                long a = (long)Position.X << 48;
                long b = (long)Position.Y << 32;
                long c = (long)Keys;
                return a + b + c;
            }
        }

        public Day201918()
        {
            Codename = "2019-18";
            Name = "Many-Worlds Interpretation";
        }

        public void Init()
        {
            // Init values
            _board = new Board<char>();
            _items = new Dictionary<char, Point>();
            string[] lines = Aoc.Framework.Input.GetStringVector(this);
            for (int h = 0; h < lines.Length; ++h)
            {
                string line = lines[h];
                for (int w = 0; w < line.Length; ++w)
                {
                    _board[w, h] = line[w];
                    if (line[w] != '#' && line[w] != '.')
                    {
                        _items.Add(line[w], new Point(w, h));
                    }
                }
            }
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                // Search the best outcome
                return Search(_items['@'], _items.Keys.Count(i => char.IsLower(i))).ToString();   
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // THANKS GOD PART 2 IS LMUCH EASIER THAN PART1 !
                // Update the grid
                Point center = _items['@'];
                _board[center] = '#';
                _board[center.X, center.Y + 1] = '#';
                _board[center.X, center.Y - 1] = '#';
                _board[center.X + 1, center.Y] = '#';
                _board[center.X - 1, center.Y] = '#';
                _board[center.X - 1, center.Y - 1] = '@';
                _board[center.X - 1, center.Y + 1] = '@';
                _board[center.X + 1, center.Y - 1] = '@';
                _board[center.X + 1, center.Y + 1] = '@';

                // For each quadrant, ignore doors which key is another quadrant
                foreach (var kv in _items.Where(kv => char.IsUpper(kv.Key)))
                {
                    Point door = kv.Value;
                    Point key = _items[char.ToLower(kv.Key)];
                    if ((door.X < center.X && key.X > center.X) || (door.Y < center.Y && key.Y > center.Y) || (door.X > center.X && key.X < center.X) || (door.Y > center.Y && key.Y < center.Y))
                    {
                        // Remove the door
                        _board[door] = '.';
                    }
                }

                // Now run each quadrant independently
                // Top-left vault
                int nbKeys = _items.Count(kv => char.IsLower(kv.Key) && kv.Value.X < center.X && kv.Value.Y < center.Y);
                long minSteps = Search(new Point(center.X - 1, center.Y - 1), nbKeys);

                // Top-right vault
                nbKeys = _items.Count(kv => char.IsLower(kv.Key) && kv.Value.X > center.X && kv.Value.Y < center.Y);
                minSteps += Search(new Point(center.X + 1, center.Y - 1), nbKeys);

                // Bottom-left vault
                nbKeys = _items.Count(kv => char.IsLower(kv.Key) && kv.Value.X < center.X && kv.Value.Y > center.Y);
                minSteps += Search(new Point(center.X - 1, center.Y + 1), nbKeys);

                // Bottom-right vault
                nbKeys = _items.Count(kv => char.IsLower(kv.Key) && kv.Value.X > center.X && kv.Value.Y > center.Y);
                minSteps += Search(new Point(center.X + 1, center.Y + 1), nbKeys);
                return minSteps.ToString();;
            }

            return "";
        }

        private long Search(Point origin, int nbKeys)
        {
            // Clear history
            _history = new HashSet<long>();

            // Prepare initial state
            State initial = new State(origin);
            Queue<State> queue = new Queue<State>();
            queue.Enqueue(initial);

            while (queue.Count > 0)
            {
                // Get  the state to process
                State current = queue.Dequeue();

                // Abort if we've already been here
                long hash = current.GetHashLong();
                if (_history.Contains(hash))
                    continue;
                _history.Add(hash);

                // Add the new key if we're on a key
                char c = _board[current.Position];
                if (char.IsLower(c))
                {
                    current.AddKey(c);
                    if (current.CountKeys() == nbKeys)
                    {
                        // We just add the final key, we have a solution
                        return current.Distance;
                    }
                }

                // Add the next states
                for (int d = 0; d < (int)Direction.Count; ++d)
                {
                    Point np = Board<long>.MoveForward(current.Position, (Direction)d);
                    char nc = _board[np];
                    if (nc == '#')
                        continue;

                    if (char.IsUpper(nc) && !current.ContainsKey(char.ToLower(nc)))
                        continue;

                    State nextState = new State(current, np);
                    queue.Enqueue(nextState);
                }
            }

            return 0;
        }
    }   
}