using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201804 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        public class ShiftEvent : IComparable
        {
            public int? Guard { get; set; }

            public int Year { get; set; }

            public int Month { get; set; }

            public int Day { get; set; }

            public int Minute { get; set; }

            public bool Awake { get; set; }

            public int CompareTo(object obj)
            {
                ShiftEvent e = (ShiftEvent)obj;
                int year = Year.CompareTo(e.Year);
                if (year != 0)
                    return year;

                int month = Month.CompareTo(e.Month);
                if (month != 0)
                    return month;

                int day = Day.CompareTo(e.Day);
                if (day != 0)
                    return day;

                return Minute.CompareTo(e.Minute);
            }
        }

        private string[] _input;
        
        private ShiftEvent[] _events;

        public Day201804()
        {
            Codename = "2018-04";
            Name = "Repose Record";
        }

        public void Init()
        {
            // Parse the event
            _input = Aoc.Framework.Input.GetStringVector(this);
            _events = new ShiftEvent[_input.Length];
            for (int i = 0; i < _input.Length; ++i)
            {
                string[] items = _input[i].Split("]");
                string[] timestamp = items[0].Substring(1).Split('-', ' ', ':');
                int? guard = null;
                bool awake = false;

                string[] content = items[1].Trim().Split(" ");
                if (content[0] == "Guard")
                {
                    awake = true;
                    guard = int.Parse(content[1].Substring(1));
                }
                if (content[0] == "wakes")
                {
                    awake = true;
                }                

                _events[i] = new ShiftEvent
                {
                    Year = int.Parse(timestamp[0]),
                    Month = int.Parse(timestamp[1]),
                    Day = int.Parse(timestamp[2]),
                    Minute = timestamp[3] == "23" ? 0 : int.Parse(timestamp[4]),
                    Guard = guard,
                    Awake = awake
                };
            }

            // Sort them
            Array.Sort(_events);

            // Resolve the guards id
            
            int? currentGuard = null;
            for (int i = 0; i < _events.Length; ++i)
            {
                if (_events[i].Guard.HasValue)
                {
                    currentGuard = _events[i].Guard.Value;
                }
                else
                {
                    _events[i].Guard = currentGuard;
                }
            }
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                // Find the most sleepy guard
                var test = _events.GroupBy(e => e.Guard);
                return "a";
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                return "part2";
            }

            return "";
        }
    }   
}