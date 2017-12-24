using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Aoc
{
    public class Day201713 : Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private Firewall _firewall;

        public Day201713()
        {            
            Codename = "2017-13";
            Name = "Packet Scanners";
            BuildFirewall();
        }

        public string Run(Part part)
        {
            if (part == Part.Part1)
            {
                return _firewall.Scan(0).ToString();
            }

            if (part == Part.Part2)
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
            foreach (string line in Input.GetStringVector(this))
            {
                string[] items = line.Split(": ");
                int depth = Int32.Parse(items[0]);
                int range = Int32.Parse(items[1]);
                _firewall.AddScanner(depth, range);
            }
        }
    }   
}