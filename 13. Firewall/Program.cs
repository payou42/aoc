using System;
using System.Text;
using System.Collections.Generic;

namespace _13._Firewall
{
    class Program
    {
        static Firewall FIREWALL;        

        static void ParseRaw()
        {
            FIREWALL = new Firewall();
            string[] lines = Input.RAW.Split("\r\n");
            foreach (string line in lines)
            {
                string[] items = line.Split(": ");
                int depth = Int32.Parse(items[0]);
                int range = Int32.Parse(items[1]);
                FIREWALL.AddScanner(depth, range);
            }
        }
       
        static void Main(string[] args)
        {
            // Prepare the grig
            ParseRaw();

            // Check the severity of the packet starting at 0            
            Console.WriteLine("Packet severity: {0}", FIREWALL.Scan(0));

            // Look for the smallest delay in order to not get caught
            bool caught = true;
            int delay = -1;
            while (caught)
            {
                caught = (FIREWALL.Scan(++delay) > 0);
            }
            Console.WriteLine("Smallest delay to not get caught: {0}", delay);
        }
    }
}
