using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day202016 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        public class Rule
        {
            public long[] Range1;

            public long[] Range2;

            public Rule(string raw)
            {
                string[] items = raw.Trim().Split('-', ' ');
                Range1 = new long[2];
                Range2 = new long[2];
                Range1[0] = long.Parse(items[0]);
                Range1[1] = long.Parse(items[1]);
                Range2[0] = long.Parse(items[3]);
                Range2[1] = long.Parse(items[4]);
            }

            public bool IsMatch(long value)
            {
                if (value >= Range1[0] && value <= Range1[1])
                    return true;

                if (value >= Range2[0] && value <= Range2[1])
                    return true;

                return false;
            }
        }

        public class Ticket
        {
            public long[] Fields;

            public Ticket(string raw)
            {
                Fields = raw.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(s => long.Parse(s)).ToArray();
            }
        }

        private Dictionary<string, Rule> _rules;

        private Ticket _myTicket;

        private Ticket[] _otherTickets;

        public Day202016()
        {
            Codename = "2020-16";
            Name = "Ticket Translation";
        }

        public void Init()
        {
            // Read the input as an array of "paragraph"
            var input = Aoc.Framework.Input.GetStringVector(this, "\r\n\r\n");
            
            // Parse the rules
            _rules = new Dictionary<string, Rule>();
            foreach (string rule in input[0].Split("\r\n", StringSplitOptions.RemoveEmptyEntries))
            {
                var items = rule.Split(": ");
                _rules[items[0]] = new Rule(items[1]);
            }

            // Parse our ticket
            _myTicket = new Ticket(input[1].Split("\r\n")[1]);

            // Parse others tickets
            _otherTickets = input[2].Split("\r\n").Skip(1).Select(s => new Ticket(s)).ToArray();
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                long tser = 0;
                foreach (var t in _otherTickets)
                {
                    foreach (var l in t.Fields)
                    {
                        if (CountMatchingRules(l) == 0)
                        {
                            tser += l;
                        }
                    }
                }

                return tser.ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // Discard invalid tickets
                var tickets = _otherTickets.Where(t => IsTicketValid(t)).Append(_myTicket).ToArray();

                // Build the list of possible fields for each index
                Dictionary<int, HashSet<string>> possibleFields = new Dictionary<int, HashSet<string>>();

                // Then for each rules, check if it can apply on each field index
                for (int i = 0; i < _myTicket.Fields.Length; ++i)
                {
                    possibleFields[i] = new HashSet<string>();

                    foreach (var r in _rules)                
                    {
                        bool ruleMatch = true;
                        foreach (Ticket t in tickets)
                        {
                            if (!r.Value.IsMatch(t.Fields[i]))
                            {
                                ruleMatch = false;
                                break;
                            }
                        }

                        if (ruleMatch)
                        {
                            possibleFields[i].Add(r.Key);
                        }
                    }
                }

                // Build the result
                Dictionary<int, string> resolved = new Dictionary<int, string>();
                while (resolved.Count < _myTicket.Fields.Length)
                {
                    var resolvedFields = possibleFields.Where(kvp => kvp.Value.Count == 1).ToList();

                    
                    foreach (var rf in resolvedFields)
                    {
                        resolved[rf.Key] = rf.Value.First();
                        foreach (var pf in possibleFields)
                        {
                            pf.Value.Remove(resolved[rf.Key]);
                        }
                    }
                }

                // Calculate the result
                long result = 1;
                for (int i = 0; i < _myTicket.Fields.Length; ++i)
                {
                    if (resolved[i].StartsWith("departure"))
                    {
                        result *= _myTicket.Fields[i];
                    }
                }

                return result.ToString();
            }

            return "";
        }

        private bool IsTicketValid(Ticket ticket)
        {
            foreach (var l in ticket.Fields)
            {
                if (CountMatchingRules(l) == 0)
                {
                    return false;
                }
            }

            return true;
        }

        private int CountMatchingRules(long value)
        {
            int count = 0;
            foreach (var rule in _rules)
            {
                if (rule.Value.IsMatch(value))
                {
                    count++;
                }
            }

            return count;
        }
    }   
}