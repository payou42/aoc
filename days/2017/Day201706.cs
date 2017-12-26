using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Aoc
{
    public class Day201706 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private int[] _banks;

        private Dictionary<string, int> _history;

        public Day201706()
        {
            Codename = "2017-06";
            Name = "Memory Reallocation";
        }

        public void Init()
        {            
        }

        public string Run(Aoc.Framework.Part part)
        {
            // Prepare data
            _banks = Aoc.Framework.Input.GetIntVector(this, "\t");
            _history = new Dictionary<string, int>();
            PushHistory(0);

            // Initialize the counters
            Int32 steps = 0;
            for (;;)
            {
                Reallocate();
                steps++;
                if (CheckHistory())
                {
                    break;
                }
                PushHistory(steps);
            }
            
            // Finished !
            return (part == Aoc.Framework.Part.Part1) ? steps.ToString() : (steps - _history[GenerateHash()]).ToString();
        }

        private string GenerateHash()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < _banks.Length; ++i)
            {
                sb.Append(_banks[i].ToString());
                sb.Append(",");
            }
            return sb.ToString();
        }

        private void PushHistory(int index)
        {
            _history.Add(GenerateHash(), index);
        }

        private bool CheckHistory()
        {
            return _history.ContainsKey(GenerateHash());
        }

        private int FindMax()
        {
            int max = 0;
            for (int i = 0; i < _banks.Length; ++i)
            {
                if (_banks[i] > _banks[max])
                {
                    max = i;
                }
            }
            return max;
        }

        private void Reallocate()
        {
            int selected = FindMax();
            int blocks = _banks[selected];
            _banks[selected] = 0;
            while (blocks > 0)
            {
                selected = (selected + 1) % _banks.Length;
                _banks[selected] += 1;
                blocks--;
            }
        }
    }
}