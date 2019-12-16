using System;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Grid;
using Aoc.Common.Simulators;

namespace Aoc
{
    public class Day201913 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        public static int EMPTY = 0;

        public static int WALL = 1;

        public static int BLOCK = 2;

        public static int PADDLE = 3;

        public static int BALL = 4;

        private Board<int> _board;

        private IntCpu _cpu;

        private long _score = 0;

        public Day201913()
        {
            Codename = "2019-13";
            Name = "Care Package";
        }

        public void Init()
        {
            _cpu = new IntCpu();

            _score = 0;
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                // Run the simulation
                _board = new Board<int>();
                _cpu.Reset(Aoc.Framework.Input.GetLongVector(this, ","));
                while (_cpu.State != IntCpu.RunningState.Halted)
                    _cpu.Run();

                // Fill the board
                while (_cpu.Output.Count > 0)
                {
                    int x = (int)_cpu.Output.Dequeue();
                    int y = (int)_cpu.Output.Dequeue();
                    int a = (int)_cpu.Output.Dequeue();
                    _board[x, y] = a;
                }

                // Count the non empty cells
                return _board.Cells.Count(c => c.Item2 == BLOCK).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // Run the simulation
                _board = new Board<int>();
                _cpu.Reset(Aoc.Framework.Input.GetLongVector(this, ","));
                _cpu.Code[0] = 2;

                while (_cpu.State != IntCpu.RunningState.Halted)
                {
                    Simulate();
                    Play();
                }

                return _score.ToString();
            }

            return "";
        }

        private void Simulate()
        {
            _cpu.State = IntCpu.RunningState.Running;
            while (_cpu.State != IntCpu.RunningState.Halted && _cpu.State != IntCpu.RunningState.WaitingInput)
            {
                _cpu.Run(true);

                if (_cpu.Output.Count >= 3)
                {
                    long x = _cpu.Output.Dequeue();
                    long y = _cpu.Output.Dequeue();
                    long a = _cpu.Output.Dequeue();
                    if ((x == -1) && (y == 0))
                        _score = a;
                    else
                        _board[(int)x, (int)y] = (int)a;
                }
            }       
        }

        private void Draw()
        {
            string[] characters = new string[5] { " ", "#", "B", "_", "*" };
            Console.WriteLine($"Score: {_score}, Blocks = {_board.Cells.Count(c => c.Item2 == BLOCK)}");
            for (int j = 0; j < 24; ++j)
            {
                for (int i = 0; i < 43; ++i)
                {
                    Console.Write(characters[_board[i, j]]);
                }

                Console.WriteLine();
            }
            
            Console.WriteLine();
        }

        private void Play()
        {
            // Implement an IA in order to play for me
            var ball = _board.Cells.Where(c => c.Item2 == BALL).First();
            var paddle = _board.Cells.Where(c => c.Item2 == PADDLE).First();

            if (paddle.Item1.X < ball.Item1.X)
            {
                _cpu.Input.Enqueue(1);
                return;
            }

            if (paddle.Item1.X > ball.Item1.X)
            {
                _cpu.Input.Enqueue(-1);
                return;
            }

            _cpu.Input.Enqueue(0);
        }
    }   
}