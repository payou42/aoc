using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace _19._Tubes
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create the maze
            Tubes tubes = new Tubes(Input.RAW);
            tubes.Walk();

            // SHow the path taken
            Console.WriteLine("Path taken  : {0}", tubes.Path);
            Console.WriteLine("Steps count : {0}", tubes.Steps);
        }
    }
}
