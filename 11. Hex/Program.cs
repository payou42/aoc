using System;
using System.Text;
using System.Collections.Generic;

namespace _11._Hex
{
    class Program
    {
        static int ProcessRaw(HexCoordinate position)
        {
            int furthest = 0;
            String[] input = Input.RAW.Split(",");        
            for (int i = 0; i <input.Length; ++i)
            {
                position.Move(input[i]);
                furthest = Math.Max(furthest, position.GetDistance());
            }
            return furthest;
        }

       
        static void Main(string[] args)
        {
            // Prepare the grig
            HexCoordinate position = new HexCoordinate();

            // Read the data
            int furthest = ProcessRaw(position);

            // Calculate the distance
            Console.WriteLine("Position: ({0}, {1})", position.X, position.Y);
            Console.WriteLine("Distance: {0}", position.GetDistance());
            Console.WriteLine("Furthest: {0}", furthest);
        }
    }
}
