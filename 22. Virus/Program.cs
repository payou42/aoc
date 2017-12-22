using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace _22._Virus
{
    class Program
    {
        static void Main(string[] args)
        {            
            // Create the grid
            Board board = new Board();
            board.Init(Input.BOARD_RAW);
            
            // Create the virus
            Virus virus = new Virus();

            // Bursts
            for (int i = 0; i < 10000000; ++i)
            {
                virus.Burst(board);
            }

            // Show the state of the board
            // board.Draw(virus);
            Console.WriteLine("Infections: {0}", virus.Infections);
        }
    }
}
