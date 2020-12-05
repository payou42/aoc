using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using Aoc.Common;

namespace Aoc
{
    public class Day202004 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string[] _input;

        private Dictionary<string, string>[] _passports;

        private Dictionary<string, string> _rules = new Dictionary<string, string>()
        {
            { "byr", @"\b(19[2-9][0-9]|200[0-2])\b" },
            { "iyr", @"\b(201[0-9]|2020)\b" },
            { "eyr", @"\b(202[0-9]|2030)\b" },
            { "hgt", @"\b(1[5-8][0-9]cm|19[0-3]cm|59in|6[0-9]in|7[0-6]in)\b" },
            { "hcl", @"\s*\#[0-9a-z]{6}" },
            { "ecl", @"\b(amb|blu|brn|gry|grn|hzl|oth)\b" },
            { "pid", @"\b[0-9]{9}\b" },
            { "cid", @".*" }
        };

        public Day202004()
        {
            Codename = "2020-04";
            Name = "Passport Processing";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetStringVector(this, "\r\n\r\n");
            _passports = new Dictionary<string, string>[_input.Length];
            for (int i = 0; i < _input.Length; ++i)
            {
                _passports[i] = new Dictionary<string, string>();
                string[] items = _input[i].Split(new string[] { " ", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in items)
                {
                    string[] kvp = item.Split(":");
                    _passports[i][kvp[0]] = kvp[1];
                }
            }
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return _passports.Count(pp => IsPassportValid(pp)).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                return _passports.Count(pp => IsPassportValid(pp) && AreFieldsValid(pp)).ToString();
            }

            return "";
        }

        private bool IsPassportValid(Dictionary<string, string> passport)
        {
            return ((passport.Count == 8) || (passport.Count == 7 && !passport.ContainsKey("cid")));
        } 

        private bool AreFieldsValid(Dictionary<string, string> passport)
        {
            foreach (var kvp in passport)
            {
                if (!Regex.IsMatch(kvp.Value, _rules[kvp.Key]))
                {
                    return false;
                }
            }

            return true;
        }
    }   
}