using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day202019 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private Dictionary<int, string> _rules;

        private Dictionary<int, string> _cache;

        private string[] _input;

        public Day202019()
        {
            Codename = "2020-19";
            Name = "Monster Messages";
        }

        public void Init()
        {
            _rules = new Dictionary<int, string>();
            _cache = new Dictionary<int, string>();
            var input = Aoc.Framework.Input.GetStringVector(this, "\r\n\r\n");
            var strRules = input[0].Split("\r\n");
            foreach (string r in strRules)
            {
                var items = r.Split(":");
                _rules[int.Parse(items[0])] = items[1];
            }

            _input = input[1].Split("\r\n");
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return CountMatches().ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                _cache[8] = $"{_cache[42]}({_cache[42]})*";
                _cache[11] = $"{_cache[42]}({_cache[42]}({_cache[42]}({_cache[42]}{_cache[31]})?{_cache[31]})?{_cache[31]})?{_cache[31]}";
                _cache.Remove(0);
                return CountMatches().ToString();
            }

            return "";
        }

        private int CountMatches()
        {
            string regex = $"^{this.BuildRegex(0)}$";
            Regex r = new Regex(regex);
            int count = 0;
            foreach (string s in _input)
            {
                if (r.IsMatch(s))
                {
                    count++;
                }
            }

            return count;
        }

        private string BuildRegex(int rule)
        {
            if (_cache.ContainsKey(rule))
            {
                return _cache[rule];
            }

            string s = _rules[rule].Trim();
            if (s.StartsWith("\""))
            {
                _cache[rule] = s[1..2];
            }
            else
            {
                var ors = s.Trim().Split("|");
                if (ors.Length == 2)
                {
                    _cache[rule] = $"({this.BuildChain(ors[0])}|{this.BuildChain(ors[1])})";
                    
                }
                else
                {
                    _cache[rule] = this.BuildChain(ors[0]);
                }
            }

            return _cache[rule];
        }

        private string BuildChain(string input)
        {
            string r = "";
            var chains = input.Trim().Split(" ").Select(int.Parse);
            foreach (int n in chains)
            {
                r += this.BuildRegex(n);
            }

            return r;
        }
    }   
}