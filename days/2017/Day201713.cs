using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201713 : Aoc.Framework.IDay
    {
        public class Scanner
        {
            public int Range { get; }

            public int Depth { get; }

            public int Severity
            {
                get
                {
                    return Depth * Range;
                }
            }

            public Scanner(int depth, int range)
            {
                Depth = depth;
                Range = range;
            }

            public bool IsScanned(int start)
            {
                return (start + Depth) % (2 * Range - 2) == 0;
            }
        }
        
        public class Firewall
        {
            private readonly List<Scanner> _firewall;

            public Firewall()
            {
                _firewall = new List<Scanner>();
            }

            public void AddScanner(int depth, int range)
            {
                _firewall.Add(new Scanner(depth, range));
            }

            public int Scan(int start)
            {
                int severity = 0;
                bool caught = false;
                foreach (Scanner scanner in _firewall)
                {
                    if (scanner.IsScanned(start))
                    {
                        caught = true;
                        severity += scanner.Severity;
                    }
                }

                if (!caught)
                {
                    severity = -1;
                }
                return severity;
            }
        }
        
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private Firewall _firewall;

        public Day201713()
        {
            Codename = "2017-13";
            Name = "Packet Scanners";
        }

        public void Init()
        {
            BuildFirewall();
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return _firewall.Scan(0).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                int delay = -1;
                while (_firewall.Scan(++delay) >= 0) {}
                return delay.ToString();
            }

            return "";
        }

        private void BuildFirewall()
        {
            _firewall = new Firewall();
            foreach (string line in Aoc.Framework.Input.GetStringVector(this))
            {
                string[] items = line.Split(": ");
                int depth = Int32.Parse(items[0]);
                int range = Int32.Parse(items[1]);
                _firewall.AddScanner(depth, range);
            }
        }
    }   
}