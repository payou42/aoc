using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common.Simulators;

namespace Aoc
{
    public class Day201606 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string[] _messages;

        public Day201606()
        {
            Codename = "2016-06";
            Name = "Signals and Noise";
        }

        public void Init()
        {
            _messages = Aoc.Framework.Input.GetStringVector(this, "\r\n");
        }

        public string Run(Aoc.Framework.Part part)
        {
            string decoded = "";
            for (int j = 0; j < _messages[0].Length; ++j)
            {
                Registers counter = new Registers();
                foreach (string s in _messages)
                {
                    counter[s[j].ToString()]++;
                }
                Int64 check =  (part == Aoc.Framework.Part.Part1) ? counter.Storage.Values.Max() : counter.Storage.Values.Min();
                decoded += counter.Storage.Where(pair => check.Equals(pair.Value)).Select(pair => pair.Key).First();
            }
            return decoded;
        }
    }   
}