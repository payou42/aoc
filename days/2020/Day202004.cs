using System;
using System.Text;
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
            // Check years
            if (!CheckYear(passport, "byr", 1920, 2002))
                return false;

            if (!CheckYear(passport, "iyr", 2010, 2020))
                return false;

            if (!CheckYear(passport, "eyr", 2020, 2030))
                return false;

            if (!CheckHeight(passport, "hgt"))
                return false;

            if (!CheckColor(passport, "hcl"))
                return false;

            if (!(new string[] { "amb", "blu", "brn", "gry" ,"grn", "hzl", "oth"}.Contains(passport["ecl"])))
                return false;
            
            if (passport["pid"].Length == 9 && int.TryParse(passport["pid"], out var _))
                return true;

            return false;
        }

        private bool CheckYear(Dictionary<string, string> passport, string field, int min, int max)
        {
            if (!passport.ContainsKey(field))
                return false;
            
            if (passport[field].Length != 4)
                return false;
            
            int v = int.Parse(passport[field]);
            if (v < min || v > max)
                return false;

            return true;
        } 

        private bool CheckHeight(Dictionary<string, string> passport, string field)
        {
            string unit = passport[field][^2..^0];           
            if (!int.TryParse(passport[field][0..^2], out int height))
                return false;

            if (unit == "cm" && height >= 150 && height <= 193)
                return true;

            if (unit == "in" && height >= 59 && height <= 76)
                return true;

            return false;
        }

        private bool CheckColor(Dictionary<string, string> passport, string field)
        {
            string color = passport[field];
            if (color[0] != '#')
                return false;

            if (color.Length != 7)
                return false;

            if (!int.TryParse(color[1..^1], NumberStyles.HexNumber, null, out var _))
                return false;

            return true;
        }
    }   
}