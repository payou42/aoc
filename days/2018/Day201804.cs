using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201804 : Aoc.Framework.IDay
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
                int day = int.Parse(timestamp[2]) + (timestamp[3] == "23" ? 1 : 0);
                int minute = timestamp[3] == "23" ? 0 : int.Parse(timestamp[4]);

                string[] content = items[1].Trim().Split(" ");
                if (content[0] == "Guard")
                {
                    awake = true;
                    guard = int.Parse(content[1].Substring(1));
                    minute = 0;
                }
                if (content[0] == "wakes")
                {
                    awake = true;
                }                

                _events[i] = new ShiftEvent
                {
                    Year = int.Parse(timestamp[0]),
                    Month = int.Parse(timestamp[1]),
                    Day = day,
                    Minute = minute,
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
                int? bestGuard = null;
                int bestSleepTime = -1;

                foreach (var guardEvents in _events.GroupBy(e => e.Guard))
                {
                    int guardId = guardEvents.First().Guard.Value;
                    int guardSleepTime = 0;
                    foreach (var dayEvents in guardEvents.GroupBy(ge => ge.Day))
                    {
                        // Last time the guard fell asleep
                        int? startTime = null;

                        // Count the time the guard was asleep
                        foreach (var e in dayEvents)
                        {
                            if (e.Awake)
                            {
                                if (startTime.HasValue)
                                {
                                    guardSleepTime += e.Minute - startTime.Value;
                                }
                                startTime = null;
                            }
                            else
                            {
                                startTime = e.Minute;
                            }
                        }

                        // Guard was asleep at the end of its turn
                        if (startTime.HasValue)
                        {
                            guardSleepTime += 60 - startTime.Value;
                        }                        
                    }

                    if (guardSleepTime > bestSleepTime)
                    {
                        bestSleepTime = guardSleepTime;
                        bestGuard = guardId;
                    }
                }

                // Count the days asleep per minutes
                int[] counter = new int[60];
                var bestGuardEvents = _events.Where(e => e.Guard == bestGuard);
                foreach (var bestDayEvents in bestGuardEvents.GroupBy(ge => ge.Day))
                {
                    // Last time the guard fell asleep
                    int? startTime = null;

                    // Count the time the guard was asleep
                    foreach (var e in bestDayEvents)
                    {
                        if (e.Awake)
                        {
                            if (startTime.HasValue)
                            {
                                for (int i = startTime.Value; i < e.Minute; ++i)
                                {
                                    counter[i]++;
                                }
                            }
                            startTime = null;
                        }
                        else
                        {
                            startTime = e.Minute;
                        }
                    }

                    // Guard was asleep at the end of its turn
                    if (startTime.HasValue)
                    {
                        for (int i = startTime.Value; i < 60; ++i)
                        {
                            counter[i]++;
                        }
                    }                        
                }

                // Find the best minute
                int bestMinute = 0;
                for (int i = 0; i < 60; ++i)
                {
                    if (counter[i] > counter[bestMinute])
                    {
                        bestMinute = i;
                    }
                }
                    
                return (bestGuard * bestMinute).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                (int, int)[] minuteStats = new (int, int)[60];
                for (int i = 0; i < 60; ++i)
                {
                    minuteStats[i] = (0, 0);
                }

                foreach (var guardEvents in _events.GroupBy(e => e.Guard))
                {
                    int guardId = guardEvents.First().Guard.Value;

                    // Count the days asleep per minutes
                    int[] counter = new int[60];
                    foreach (var dayEvents in guardEvents.GroupBy(ge => ge.Day))
                    {
                        // Last time the guard fell asleep
                        int? startTime = null;

                        // Count the time the guard was asleep
                        foreach (var e in dayEvents)
                        {
                            if (e.Awake)
                            {
                                if (startTime.HasValue)
                                {
                                    for (int i = startTime.Value; i < e.Minute; ++i)
                                    {
                                        counter[i]++;
                                    }
                                }
                                startTime = null;
                            }
                            else
                            {
                                startTime = e.Minute;
                            }
                        }

                        // Guard was asleep at the end of its turn
                        if (startTime.HasValue)
                        {
                            for (int i = startTime.Value; i < 60; ++i)
                            {
                                counter[i]++;
                            }
                        }                        
                    }

                    // Update minuteStats
                    for (int i = 0; i < 60; ++i)
                    {
                        if (minuteStats[i].Item1 < counter[i])
                        {
                            minuteStats[i] = (counter[i], guardId);
                        }
                    }
                }

                // Get the stats
                int bestMinute = 0;
                for (int i = 0; i < 60; ++i)
                {
                    if (minuteStats[i].Item1 > minuteStats[bestMinute].Item1)
                    {
                        bestMinute = i;
                    }
                }

                return (bestMinute * minuteStats[bestMinute].Item2).ToString();
            }

            return "";
        }
    }   
}