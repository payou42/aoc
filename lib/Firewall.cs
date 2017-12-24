using System;
using System.Text;
using System.Collections.Generic;

namespace Aoc
{
    public class Firewall
    {
        private List<Scanner> _firewall;

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
}
