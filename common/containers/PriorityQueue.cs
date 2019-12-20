using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace Aoc.Common.Containers
{

    public class PriorityQueue<T>
    {
        private readonly Dictionary<T, long> _dataByContent;

        private readonly Dictionary<long, List<T>> _dataByPriority;

        private readonly SortedSet<long> _priorities;

        private long _count;

        public PriorityQueue()
        {
            _dataByContent = new Dictionary<T, long>();
            _dataByPriority = new Dictionary<long, List<T>>();
            _priorities = new SortedSet<long>();
            _count = 0;
        }

        public void AddOrUpdate(T data, long priority)
        {
            if (_dataByContent.ContainsKey(data))
            {
                // Actually update the priority of the given item instead of adding it
                Update(data, priority);
            }
            else
            {
                Enqueue(data, priority);
            }
        }

        public void Enqueue(T data, long priority)
        {
            // Add the data to the new priority
            if (!_dataByPriority.ContainsKey(priority))
            {
                _priorities.Add(priority);
                _dataByPriority[priority] = new List<T>();
            }            
            _dataByPriority[priority].Add(data);
            _dataByContent[data] = priority;
            _count++;
        }

        public bool Update(T data, long priority)
        {
            // Skip missing data
            if (!_dataByContent.ContainsKey(data))
            {
                return false;
            }

            // Skip if priority hasn't changed            
            long previous = _dataByContent[data];
            if (priority == previous)
            {
                return true;
            }

            // Remove the data from the previous priority
            _dataByPriority[previous].Remove(data);
            if (_dataByPriority[previous].Count == 0)
            {
                _dataByPriority.Remove(previous);
                _priorities.Remove(previous);
            }

            // Add the data to the new priority
            if (!_dataByPriority.ContainsKey(priority))
            {
                _priorities.Add(priority);
                _dataByPriority[priority] = new List<T>();
            }            
            _dataByPriority[priority].Add(data);
            _dataByContent[data] = priority;
            return true;
        }

        public long Count => _count;

        public bool TryDequeueMin(out T data)
        {
            // Empty queue ?
            if (_count == 0)
            {
                data = default;
                return false;
            }

            // Get the min item
            DequeueAtPriority(_priorities.Min, out data);
            return true;
        }

        public bool TryDequeueMax(out T data)
        {
            // Empty queue ?
            if (_count == 0)
            {
                data = default;
                return false;
            }

            // Get the max item
            DequeueAtPriority(_priorities.Max, out data);
            return true;
        }

        public void DequeueAtPriority(long priority, out T data)
        {
            // Get the item from the priorities dict
            List<T> matches = _dataByPriority[priority];
            data = matches[0];

            // Remove it from the data dict
            _dataByContent.Remove(data);

            // Remove it from the priority dict
            matches.RemoveAt(0);
            if (matches.Count == 0)
            {
                _dataByPriority.Remove(priority);
                _priorities.Remove(priority);
            }

            // Decrease count
            _count--;
        }
    }
}