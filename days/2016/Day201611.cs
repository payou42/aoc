using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201611 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        public enum ItemType
        {
            Chip = 0,
            Generator = 1
        }

        public class Item
        {
            public string Element { get; set; }

            public ItemType Type { get; set; }
        }

        public class Floor
        {
            public List<Item> Items { get; private set; }

            public Floor()
            {
                Items = new List<Item>();
            }

            public Floor(Floor other)
            {
                Items = other.Items.Select(i => i).ToList();
            }

            public long Hash
            {
                get
                {
                    long hash = 0;
                    foreach (Item item in Items)
                    {
                        hash ^= item.GetHashCode();
                    }
                    return hash;
                }
            }

            public bool IsValid
            {
                get
                {
                    var generators = Items.Where(item => item.Type == ItemType.Generator);
                    var chips = Items.Where(item => item.Type == ItemType.Chip);
                    foreach (Item generator in generators)
                    {
                        foreach (Item chip in chips.Where(chip => chip.Element != generator.Element))
                        {
                            if (!generators.Any(g => g.Element == chip.Element))
                            {
                                return false;
                            }
                        }
                    }
                    return true;
                }
            }

            public bool HasChips
            {
                get
                {
                    return Items.Any(item => item.Type == ItemType.Chip);
                }
            }

            public bool HasItems
            {
                get
                {
                    return Items.Any();
                }
            }
        }

        public class Building
        {
            private Floor[] _floors;

            private int _elevator;

            public string Hash
            {
                get
                {
                    return _elevator.ToString() + '>' + string.Join('.', _floors.Select(floor => floor.Hash.ToString()));
                }
            }

            public bool IsValid
            {
                get
                {
                    return _floors.All(floor => floor.IsValid);
                }
            }

            public bool IsComplete
            {
                get
                {
                    for (int i = 0; i < _floors.Length - 1; ++i)
                    {
                        if (_floors[i].HasItems)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }

            public Building(string[] input)
            {
                _elevator = 0;
                _floors = new Floor[input.Length];
                for (int i = 0; i < input.Length; ++i)
                {
                    // Create the floor
                    _floors[i] = new Floor();

                    // Create the items on the floor
                    string[] items = input[i].Split(" ");
                    for (int j = 1; j < items.Length; ++j)
                    {
                        if (items[j].StartsWith("generator"))
                        {
                            _floors[i].Items.Add(new Item { Element = items[j - 1], Type = ItemType.Generator });
                        }

                        if (items[j].StartsWith("microchip"))
                        {
                            string element = items[j - 1].Split("-")[0];
                            _floors[i].Items.Add(new Item { Element = element, Type = ItemType.Chip });
                        }
                    }
                }
            }

            public Building(Building other)
            {
                _elevator = other._elevator;
                _floors = new Floor[other._floors.Count()];
                for (int i = 0; i < _floors.Length; ++i)
                {
                    _floors[i] = new Floor(other._floors[i]);
                }
            }

            public Floor GetCurrentFloor()
            {
                return _floors[_elevator];
            }

            public bool CanMoveUp()
            {
                return _elevator < _floors.Length - 1;
            }

            public bool CanMoveDown()
            {
                return _elevator > 0;
            }

            public Building MoveItem(int direction, Item item1, Item item2 = null)
            {
                Building building = new Building(this);
                building.ApplyMove(direction, item1, item2);
                return building;
            }

            private void ApplyMove(int direction, Item item1, Item item2 = null)
            {
                _floors[_elevator].Items.Remove(item1);
                _floors[_elevator + direction].Items.Add(item1);
                if (item2 != null)
                {
                    _floors[_elevator].Items.Remove(item2);
                    _floors[_elevator + direction].Items.Add(item2);
                }
                _elevator += direction;
            }
        }
    
        private string[] _input;

        private Building _building;

        private Dictionary<string, (long, bool)> _history;

        public Day201611()
        {
            Codename = "2016-11";
            Name = "Radioisotope Thermoelectric Generators";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetStringVector(this, "\n");
            _building = new Building(_input);
            _history = new Dictionary<string, (long, bool)>();
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                // Recursively search for a valid building
                DeepScan(_building);

                // Look for the minimum valid solution
                return (1 + _history.Values.Where(item => item.Item2).Select(item => item.Item1).Min()).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // Prepare new items
                Floor ground = _building.GetCurrentFloor();
                ground.Items.AddRange( new[]
                {
                    new Item { Element = "elerium", Type = ItemType.Chip },
                    new Item { Element = "elerium", Type = ItemType.Generator },
                    new Item { Element = "dilithium", Type = ItemType.Chip },
                    new Item { Element = "dilithium", Type = ItemType.Generator }
                });

                // Clean history
                _history.Clear();

                // Recursively search for a valid building
                DeepScan(_building);

                // Look for the minimum valid solution
                return (1 + _history.Values.Where(item => item.Item2).Select(item => item.Item1).Min()).ToString();
            }

            return "";
        }

        private void DeepScan(Building initial)
        {
            // Build the oves stack
            Building building = initial;
            Queue<(Building, long)> queue = new Queue<(Building, long)>();

            // Add the current building in the history
            _history[building.Hash] = (0, building.IsComplete);

            // Add all possible moves in the stack
            AddMoves(queue, building, 0);

            // Process stack
            while (queue.TryDequeue(out var item))
            {
                // Get the building
                building = item.Item1;
                long moves = item.Item2;

                // Check the validity of the move
                if (building.IsValid)
                {
                    // Check the current state in the history
                    string hash = building.Hash;
                    if (!_history.ContainsKey(hash))
                    {
                        bool complete = building.IsComplete;
                        _history[hash] = (moves, complete);
                        if (complete)
                        {
                            // Since we are doing a width-first scan, we have the minimum
                            return;
                        }
                        AddMoves(queue, building, moves + 1);
                    }
                }
            }
        }

        private void AddMoves(Queue<(Building, long)> queue, Building building, long moves)
        {
            // Try all possible moves from the current situation
            var items = building.GetCurrentFloor().Items;

            // Move up
            if (building.CanMoveUp())
            {
                // Try combinaison of one item
                for (int i = 0; i < items.Count; ++i)
                {
                    queue.Enqueue((building.MoveItem(1, items[i], null), moves));
                }

                // Try combinaison of 2 items
                for (int i = 0; i < items.Count - 1; ++i)
                {
                    for (int j = i + 1; j < items.Count; ++j)
                    {
                        queue.Enqueue((building.MoveItem(1, items[i], items[j]), moves));
                    }
                }
            }

            // Move down
            if (building.CanMoveDown())
            {
                // Try combinaison of one item
                for (int i = 0; i < items.Count; ++i)
                {
                    queue.Enqueue((building.MoveItem(-1, items[i], null), moves));
                }

                // Try combinaison of 2 items
                for (int i = 0; i < items.Count - 1; ++i)
                {
                    for (int j = i + 1; j < items.Count; ++j)
                    {
                        queue.Enqueue((building.MoveItem(-1, items[i], items[j]), moves));
                    }
                }
            }
        }
    }
}