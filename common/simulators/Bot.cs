using System;
using System.Text;
using System.Drawing;
using System.Collections.Generic;
using Aoc.Common.Geometry;

namespace Aoc.Common.Simulators
{
    /// <summary>
    /// An object representing a Turing machine
    /// </summary>
    public class Bot
    {
        public string Name { get; private set; }

        public int Count
        {
            get
            {
                return _content.Count;
            }
        }

        private readonly List<int> _content;

        public Bot(string name)
        {
            Name = name;
            _content = new List<int>();
        }

        public void Add(int value)
        {
            _content.Add(value);
        }
        
        public int RemoveLowest()
        {
            int v = Math.Min(_content[0], _content[1]);
            _content.Remove(v);
            return v;
        }

        public int RemoveFirst()
        {
            int v = _content[0];
            _content.Clear();
            return v;
        }

        public bool IsMatchingPair(int v1, int v2)
        {
            if (_content[0] == v1 && _content[1] == v2)
            {
                return true;
            }

            if (_content[0] == v2 && _content[1] == v1)
            {
                return true;
            }

            return false;
        }
    }
}