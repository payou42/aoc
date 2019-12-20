using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Aoc
{
    public class Day201701 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string _captcha = null;

        public Day201701()
        {
            Codename = "2017-01";
            Name = "Inverse Captcha";
        }

        public void Init()
        {
            _captcha = Aoc.Framework.Input.GetString(this);
        }

        public string Run(Aoc.Framework.Part part)
        {
            int result = 0;
            for (int i = 0; i < _captcha.Length; ++i)
            {
                if (Match(i, part))
                {
                    int v = _captcha[i] - '0';
                    result += v;
                }
            }

            return result.ToString();
        }

        private Char Next(int index, Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return _captcha[(index + 1) % _captcha.Length];
            }
            
            return _captcha[(index + (_captcha.Length / 2)) % _captcha.Length];
        }

        private Boolean Match(int index, Aoc.Framework.Part part)
        {
            return _captcha[index] == Next(index, part);
        }
    }
}