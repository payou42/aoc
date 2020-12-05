using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Crypto;

namespace Aoc
{
    public class Day201504 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string _input;

        public Day201504()
        {
            Codename = "2015-04";
            Name = "The Ideal Stocking Stuffer";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetString(this);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                long i = 1;
                while (true)
                {
                    byte[] md5 = Md5.Compute(_input, i);
                    if ((md5[0] == 0) && (md5[1] == 0) && ((md5[2] & 0xF0) == 0))
                    {
                        return i.ToString();
                    }

                    i++;
                }
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                long i = 1;
                while (true)
                {
                    byte[] md5 = Md5.Compute(_input, i);
                    if ((md5[0] == 0) && (md5[1] == 0) && (md5[2] == 0))
                    {
                        return i.ToString();
                    }

                    i++;
                }
            }

            return "";
        }
    }   
}