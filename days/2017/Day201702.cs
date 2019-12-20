using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Aoc
{
    public class Day201702 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private Int32[][] _content = null;

        public Day201702()
        {
            Codename = "2017-02";
            Name = "Corruption Checksum";
        }

        public void Init()
        {
            _content = Aoc.Framework.Input.GetIntMatrix(this);
        }

        public string Run(Aoc.Framework.Part part)
        {
            return EvaluateChecksum(part).ToString();
        }

        private int EvaluateRowChecksum(int index)
        {
            int min = Int32.MaxValue;
            int max = Int32.MinValue;
            for (int i = 0; i < _content[index].Length; ++i)
            {
                min = Math.Min(min, _content[index][i]);
                max = Math.Max(max, _content[index][i]);
            }
            return max - min;
        }

        private int EvaluateRowEven(int index)
        {
            for (int i = 0; i < _content[index].Length; ++i)
            {
                for (int j = 0; j < _content[index].Length; ++j)
                {
                    if ((i != j) && (_content[index][i] % _content[index][j] == 0 && (_content[index][i] >= _content[index][j])))
                    {
                        return _content[index][i] / _content[index][j];
                    }
                }
            }
            return 0;
        }

        private int EvaluateChecksum(Aoc.Framework.Part part)
        {
            int checksum = 0;
            for (int i = 0; i < _content.Length; ++i)
            {
                checksum += (part == Aoc.Framework.Part.Part1) ? EvaluateRowChecksum(i) : EvaluateRowEven(i);
            }
            return checksum;
        }

    }
}