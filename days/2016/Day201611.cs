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

        public static class Building
        {
            public static uint MICROCHIP = 0;

            public static uint GENERATOR = 1;

            public static uint ELEVATOR = 2;

            public static uint GetIndex(uint id, uint kind)
            {
                if (kind == ELEVATOR)
                    return 0;

                return 2 + id * 4 + kind * 2;
            }

            public static uint GetIndex(uint id)
            {
                return 2 + (id * 2);
            }

            public static uint Set(uint initial, uint index, uint floor)
            {
                uint state = initial;
                state &= ~((uint)0x3 << (int)index);
                state |= (floor & (uint)0x03) << (int)index;
                return state;
            }

            public static uint Get(uint state, uint index)
            {
                return (state >> (int)index) & 0x03;
            }
            
            public static uint Move(uint initial, uint index, uint floor)
            {
                uint state = initial;
                state = Building.Set(state, index, floor);
                state = Building.Set(state, 0, floor);
                return state;
            }

            public static uint Move(uint initial, uint index1, uint index2, uint floor)
            {
                uint state = Building.Move(initial, index1, floor);
                state = Building.Set(state, index2, floor);
                return state;
            }

            public static bool IsFinal(uint state, uint count)
            {
                for (int i = 0; i < 1 + 2 * count; ++i)
                {
                    if (((state >> (2 * i)) & 0x03) != 0x03)
                    {
                        return false;
                    }
                }

                return true;
            }

            public static bool IsValid(uint state, uint count)
            {
                for (uint i = 0; i < count; ++i)
                {
                    // Get the floor of the item
                    uint itemFloor = Building.Get(state, Building.GetIndex(i, Building.MICROCHIP));

                    // If the generator is on the same floor, we're good
                    if (Building.Get(state, Building.GetIndex(i, Building.GENERATOR)) == itemFloor)
                    {
                        continue;
                    }

                    // if there's another generator on the same floor, we're not good
                    for (uint j = 0; j < count; ++j)
                    {
                        if (i == j)
                        {
                            continue;
                        }

                        if (Building.Get(state, Building.GetIndex(j, Building.GENERATOR)) == itemFloor)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }

            public static void Dump(uint state, uint count)
            {
                for (int floor = 3; floor >= 0; floor--)
                {
                    // Draw floor
                    Console.Write($"F{floor} | ");

                    // Draw elevator
                    Console.Write(Building.Get(state, 0) == floor ? "E " : "  ");

                    // Draw each element
                    for (uint e = 0; e < count; ++e)
                    {
                        bool present = (Building.Get(state, Building.GetIndex(e, Building.MICROCHIP))) == floor;
                        Console.Write(present ? $"{e}M " : "-- ");
                        present = (Building.Get(state, Building.GetIndex(e, Building.GENERATOR))) == floor;
                        Console.Write(present ? $"{e}G " : "-- ");
                    }

                    // Next floor
                    Console.WriteLine();
                }

                Console.WriteLine();
            }
        }
    
        private uint _state;

        private uint _count;

        private Dictionary<uint, (long, bool)> _history;

        public Day201611()
        {
            Codename = "2016-11";
            Name = "Radioisotope Thermoelectric Generators";
        }

        public void Init()
        {
            _history = new Dictionary<uint, (long, bool)>();
        }

        public void Reset(bool extended = false)
        {
            // Initial set up
            // 0. Thulium
            // 1. Plutonium
            // 2. Strontium
            // 3. Promethium
            // 4. Ruthenium
            // 5. Elerium
            // 6. Dilithium
            _count = 5;
            _state = 0;
            _state = Building.Set(_state, Building.GetIndex(0, Building.GENERATOR), 0);
            _state = Building.Set(_state, Building.GetIndex(0, Building.MICROCHIP), 0);
            _state = Building.Set(_state, Building.GetIndex(1, Building.GENERATOR), 0);
            _state = Building.Set(_state, Building.GetIndex(2, Building.GENERATOR), 0);
            _state = Building.Set(_state, Building.GetIndex(1, Building.MICROCHIP), 1);
            _state = Building.Set(_state, Building.GetIndex(2, Building.MICROCHIP), 1);
            _state = Building.Set(_state, Building.GetIndex(3, Building.GENERATOR), 2);
            _state = Building.Set(_state, Building.GetIndex(3, Building.MICROCHIP), 2);
            _state = Building.Set(_state, Building.GetIndex(4, Building.GENERATOR), 2);
            _state = Building.Set(_state, Building.GetIndex(4, Building.MICROCHIP), 2);

            if (extended)
            {
                _count = 7;
                _state = Building.Set(_state, Building.GetIndex(5, Building.GENERATOR), 0);
                _state = Building.Set(_state, Building.GetIndex(5, Building.MICROCHIP), 0);
                _state = Building.Set(_state, Building.GetIndex(6, Building.GENERATOR), 0);
                _state = Building.Set(_state, Building.GetIndex(6, Building.MICROCHIP), 0);
            }
           
            _history.Clear();
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                Reset(false);
                DeepScan(_state);
                return (1 + _history.Values.Where(item => item.Item2).Select(item => item.Item1).Min()).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                Reset(true);
                DeepScan(_state);
                return (1 + _history.Values.Where(item => item.Item2).Select(item => item.Item1).Min()).ToString();
            }

            return "";
        }

        private void DeepScan(uint initial)
        {
            // Build the moves stack
            uint state = initial;
            Queue<(uint, long)> queue = new Queue<(uint, long)>();

            // Add the current building in the history
            _history[state] = (0, Building.IsFinal(state, _count));

            // Add all possible moves in the stack
            AddMoves(queue, state, 0);

            // Process stack
            while (queue.TryDequeue(out var item))
            {
                // Get the building
                state = item.Item1;
                long moves = item.Item2;

                // Check the validity of the move
                if (Building.IsValid(state, _count))
                {
                    // Check the current state in the history
                    if (!_history.ContainsKey(state))
                    {
                        bool complete = Building.IsFinal(state, _count);
                        _history[state] = (moves, complete);
                        if (complete)
                        {
                            // Since we are doing a width-first scan, we have the minimum
                            return;
                        }
        
                        AddMoves(queue, state, moves + 1);
                    }
                }
            }
        }

        private void AddMoves(Queue<(uint, long)> queue, uint state, long moves)
        {
            // Get the current floor
            uint floor = Building.Get(state, 0);

            // Try combinaison of one item
            for (uint i = 0; i < 2 * _count; ++i)
            {
                if (Building.Get(state, Building.GetIndex(i)) != floor)
                    continue;
                
                if (floor < 3) 
                    queue.Enqueue((Building.Move(state, Building.GetIndex(i), floor + 1), moves));

                if (floor > 0)
                    queue.Enqueue((Building.Move(state, Building.GetIndex(i), floor - 1), moves));
            }

            // Try combinaison of 2 items
            for (uint i = 0; i < 2 * _count - 1; ++i)
            {
                if (Building.Get(state, Building.GetIndex(i)) != floor)
                    continue;

                for (uint j = i + 1; j < 2 * _count; ++j)
                {
                    if (Building.Get(state, Building.GetIndex(j)) != floor)
                        continue;

                    if (floor < 3) 
                        queue.Enqueue((Building.Move(state, Building.GetIndex(i), Building.GetIndex(j), floor + 1), moves));

                    if (floor > 0)
                        queue.Enqueue((Building.Move(state, Building.GetIndex(i), Building.GetIndex(j), floor - 1), moves));
                }
            }
        }
    }
}