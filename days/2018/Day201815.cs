using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using Aoc.Common;
using Aoc.Common.Grid;

namespace Aoc
{
    public class Day201815 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        public enum CreatureType
        {
            Elf = 0,
            Goblin = 1
        }

        public class Creature
        {
            public int Health { get; set; }
            
            public int Damage { get; set; }
            
            public CreatureType Type { get; set; }
        }

        private Board<bool> _terrain;

        private Board<Creature> _map;

        public Day201815()
        {
            Codename = "2018-15";
            Name = "Beverage Bandits";            
        }

        public void Init()
        {
        }

        private void Reset(int elfDamage)
        {
            _terrain = new Board<bool>();
            _map = new Board<Creature>();
            string[] input = Aoc.Framework.Input.GetStringVector(this);
            for (int y = 0; y < input.Length; ++y)
            {
                for (int x = 0; x < input[y].Length; ++x)
                {
                    if (input[y][x] != '#')
                    {
                        _terrain[x, y] = true;
                    }

                    if (input[y][x] == 'G')
                    {
                        _map[x, y] = new Creature { Type = CreatureType.Goblin, Health = 200, Damage = 3 };
                    }

                    if (input[y][x] == 'E')
                    {
                        _map[x, y] = new Creature { Type = CreatureType.Elf, Health = 200, Damage = elfDamage };
                    }
                }
            }
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return ResolveFight(3).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                Reset(3);
                int nbElf = _map.Values.Count(creature => creature.Type == CreatureType.Elf);
                int elfAttack = 4;
                long outcome = 0;
                while (true)
                {
                    outcome = ResolveFight(elfAttack);
                    if (nbElf == _map.Values.Count(creature => creature.Type == CreatureType.Elf))
                    {
                        // No casuality on the elf side !
                        break;
                    }
                    elfAttack++;
                }
                return outcome.ToString();
            }

            return "";
        }

        private long ResolveFight(int elfDamage)
        {
            Reset(elfDamage);
            long turn = 0;
            while (!NextTurn())
            {
                turn++;
            }

            return (turn * _map.Values.Sum(creature => creature.Health));
        }

        private bool NextTurn()
        {
            // Mark the dead creature in order to skip them
            List<Creature> dead = new List<Creature>();

            // Process each creature in the reading order at the starting position
            List<(Point, Creature)> alive = _map.Cells.OrderBy(cell => cell.Item1.Y).ThenBy(cell => cell.Item1.X).ToList();
            foreach (var cell in alive)
            {
                if (dead.Contains(cell.Item2))
                {
                    // This one is already dead, skip it
                    continue;
                }

                if (!CreatureTurn(cell, dead))
                {
                    // If unit didn't find any target, the combat is ended
                    return true;
                }
            }

            return false;
        }

        private bool CreatureTurn((Point, Creature) cell, List<Creature> dead)
        {
            //  Get our target
            Point? bestTarget = GetBestTarget(cell, dead);
            if (bestTarget == null)
            {
                return false;
            }

            // Move towards the target
            Point next = MoveToTarget(cell.Item1, bestTarget.Value);
            _map.Remove(cell.Item1.X, cell.Item1.Y);
            _map[next.X, next.Y] = cell.Item2;

            // Attack !
            (Point, Creature) creature = GetBestOpponent(cell.Item2.Type, next);
            if (creature.Item2 == null)
            {
                // Too far away
                return true;
            }

            // Attack !
            creature.Item2.Health -= cell.Item2.Damage;
            if (creature.Item2.Health <= 0)
            {
                dead.Add(creature.Item2);
                _map.Remove(creature.Item1.X, creature.Item1.Y);
            }

            // We still have some targets
            return true;
        }

        private void EnqueueIfValid(Point point, Board<int> distances, Queue<Point> queue, int distance)
        {
            if (_terrain[point.X, point.Y] && _map[point.X, point.Y] == null && distances[point.X, point.Y] == 0)
            {
                // This cell is not a wall, and there's no creature in it
                distances[point.X, point.Y] = distance;
                queue.Enqueue(point);
            }
        }

        private Board<int> BuildDistanceMap(Point from)
        {
            // Build the reachability and distance map
            Board<int> distances = new Board<int>();
            Queue<Point> queue = new Queue<Point>();
            distances[from.X, from.Y] = 1;
            queue.Enqueue(from);

            // Process the queue
            while (queue.TryDequeue(out var current))
            {
                // Enqueue the neighbours if it's doable
                int nextDistance = distances[current.X, current.Y] + 1;
                EnqueueIfValid(new Point(current.X, current.Y - 1), distances, queue, nextDistance);
                EnqueueIfValid(new Point(current.X - 1, current.Y), distances, queue, nextDistance);
                EnqueueIfValid(new Point(current.X + 1, current.Y), distances, queue, nextDistance);
                EnqueueIfValid(new Point(current.X, current.Y + 1), distances, queue, nextDistance);
            }
            return distances;
        }

        private Point? GetBestTarget((Point, Creature) cell, List<Creature> dead)
        {
            // Get the list of potential targets
            var targetCreatures = _map.Cells.Where(c => c.Item2.Type != cell.Item2.Type && !dead.Contains(c.Item2)).ToList();
            if (targetCreatures.Count == 0)
            {
                return null;    
            }

            // Build the list of available targets
            var targetPoints = targetCreatures.SelectMany(c => new List<Point>
            {
                new Point(c.Item1.X, c.Item1.Y - 1),
                new Point(c.Item1.X - 1, c.Item1.Y),
                new Point(c.Item1.X + 1, c.Item1.Y),
                new Point(c.Item1.X, c.Item1.Y + 1)
            }).Where(point => _terrain[point.X, point.Y] && (_map[point.X, point.Y] == null || _map[point.X, point.Y] == cell.Item2)).ToList();

            // Build the distance map from the current creature
            Board<int> fromSource = BuildDistanceMap(cell.Item1);

            // Get the best target
            var bestPoints = targetPoints.Where(point => fromSource[point.X, point.Y] > 0).OrderBy(point => fromSource[point.X, point.Y]).ThenBy(point => point.Y).ThenBy(point => point.X).Take(1).ToList();
            if (bestPoints.Count == 0)
            {
                // We have some targets but they are unreachable, don't move
                return cell.Item1;
            }

            // return the closest target
            return bestPoints[0];
        }

        private Point MoveToTarget(Point from, Point to)
        {
            // No need to move ?
            if (from.X == to.X && from.Y == to .Y)
            {
                return from;
            }

            // Build distance map from target
            var distances = BuildDistanceMap(to);
            int minDistance = int.MaxValue;
            Point best = from;
            int currentDistance = distances[from.X, from.Y];
            if (distances[from.X, from.Y - 1] > 0 && distances[from.X, from.Y - 1] < minDistance)
            {
                minDistance = distances[from.X, from.Y - 1];
                best = new Point(from.X, from.Y - 1);                
            }
            if (distances[from.X - 1, from.Y] > 0 && distances[from.X - 1, from.Y] < minDistance)
            {
                minDistance = distances[from.X - 1, from.Y];
                best = new Point(from.X - 1, from.Y);
            }
            if (distances[from.X + 1, from.Y] > 0 && distances[from.X + 1, from.Y] < minDistance)
            {
                minDistance = distances[from.X + 1, from.Y];
                best = new Point(from.X + 1, from.Y);
            }
            if (distances[from.X, from.Y + 1] > 0 && distances[from.X, from.Y + 1] < minDistance)
            {
                minDistance = distances[from.X, from.Y + 1];
                best = new Point(from.X, from.Y + 1);
            }
            return best;
        }

        private (Point, Creature) GetBestOpponent(CreatureType me, Point where)
        {            
            int lowestHealth = int.MaxValue;
            Point? best = null;
            Point target = new Point(where.X, where.Y - 1);
            if (_map[target.X, target.Y] != null && _map[target.X, target.Y].Type != me)
            {
                if (_map[target.X, target.Y].Health < lowestHealth)
                {
                    lowestHealth = _map[target.X, target.Y].Health;
                    best = target;
                }
            }

            target = new Point(where.X - 1, where.Y);
            if (_map[target.X, target.Y] != null && _map[target.X, target.Y].Type != me)
            {
                if (_map[target.X, target.Y].Health < lowestHealth)
                {
                    lowestHealth = _map[target.X, target.Y].Health;
                    best = target;
                }
            }

            target = new Point(where.X + 1, where.Y);
            if (_map[target.X, target.Y] != null && _map[target.X, target.Y].Type != me)
            {
                if (_map[target.X, target.Y].Health < lowestHealth)
                {
                    lowestHealth = _map[target.X, target.Y].Health;
                    best = target;
                }
            }

            target = new Point(where.X, where.Y + 1);
            if (_map[target.X, target.Y] != null && _map[target.X, target.Y].Type != me)
            {
                if (_map[target.X, target.Y].Health < lowestHealth)
                {
                    lowestHealth = _map[target.X, target.Y].Health;
                    best = target;
                }
            }

            return best.HasValue ? (best.Value, _map[best.Value.X, best.Value.Y]) : (where, null);
        }
    }   
}
