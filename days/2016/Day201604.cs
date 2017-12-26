using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201604 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private Tuple<string, int, string>[] _rooms;

        public Day201604()
        {
            Codename = "2016-04";
            Name = "Security Through Obscurity";
        }

        public void Init()
        {
            _rooms = Aoc.Framework.Input.GetStringVector(this, "\n").Where(s => s != "").Select(s => ParseRoom(s)).ToArray();
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                Int64 sum = 0;
                foreach (Tuple<string, int, string> room in _rooms)
                {
                    if (ValidateRoom(room.Item1, room.Item3))
                    {
                        sum += room.Item2;
                    }
                }
                return sum.ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                foreach (Tuple<string, int, string> room in _rooms)
                {
                    if (ValidateRoom(room.Item1, room.Item3))
                    {
                        if (DecryptRoom(room.Item1, room.Item2) == "northpole object storage")
                        {
                            return room.Item2.ToString();
                        }
                    }
                }
                return "";
            }

            return "";
        }

        private Tuple<string, int, string> ParseRoom(string room)
        {
            string[] items = room.Split("[");
            string checksum = items[1].Substring(0, items[1].Length - 1);

            string[] dashes = items[0].Split("-");
            int id = Int32.Parse(dashes[dashes.Length - 1]);

            string name = String.Join(" ", dashes, 0, dashes.Length - 1);
            return new Tuple<string, int, string>(name, id, checksum);
        }

        private bool ValidateRoom(string name, string checksum)
        {
            // Initialize array
            Tuple<char, int>[] scores = new Tuple<char, int>[26];
            for (int i = 0; i < 26; ++i)
            {
                scores[i] = new Tuple<char, int>((char)('a' + i), 0);
            }

            // Compute frequency
            foreach (char c in name)
            {
                if (c != ' ')
                {
                    scores[c - 'a'] = new Tuple<char, int>(c, scores[c - 'a'].Item2 + 1);
                }
            }

            // Sort the result (expecting a stable sort)
            scores = scores.OrderByDescending(item => item.Item2).ToArray();
            string hash = "";
            for (int i = 0; i < 5; ++i)
            {
                hash += scores[i].Item1;
            }

            return hash == checksum;
        }

        private string DecryptRoom(string name, int id)
        {
            return new String(name.ToCharArray().Select( c => (c == ' ') ? ' ' : (char)('a' + (((int)(c - 'a') + id) % 26)) ).ToArray());
        }
    }
}