using System;
using System.Linq;
using System.Collections.Generic;

namespace _22._Virus
{
    class Virus
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public Direction Direction { get; private set; }
        public int Infections { get; private set; }

        public Virus()
        {
            X = 0;
            Y = 0;
            Direction = Direction.UP;
            Infections = 0;
        }

        public void Burst(Board board)
        {
            Turn(board[X, Y]);
            board[X, Y] = Infect(board[X, Y]);
            Move();
        }

        private void Turn(Cell cell)
        {
            int increment = GetIncrement(cell);
            Direction = (Direction)(((int)Direction + (int)Direction.COUNT + increment) % (int)Direction.COUNT);
        }

        private void Move()
        {
            switch (Direction)
            {
                case Direction.UP:
                {
                    Y++;
                    return;
                }
                case Direction.LEFT:
                {
                    X--;
                    return;
                }
                case Direction.DOWN:
                {
                    Y--;
                    return;
                }
                case Direction.RIGHT:
                {
                    X++;
                    return;
                }
            }
        }

        private int GetIncrement(Cell cell)
        {
            switch (cell)
            {
                case Cell.CLEAN: return -1;
                case Cell.WEAKENED: return 0;
                case Cell.INFECTED: return 1;
                case Cell.FLAGGED: return 2;
            }
            return 0;
        }

        private Cell Infect(Cell cell)
        {
            // Part 1
            /*
            switch (cell)
            {
                case Cell.CLEAN:
                {
                    Infections++;
                    return Cell.INFECTED;
                }

                case Cell.INFECTED: return Cell.CLEAN;
            }
            */

            // Part 2
            switch (cell)
            {
                case Cell.WEAKENED:
                {
                    Infections++;
                    return Cell.INFECTED;
                }

                case Cell.CLEAN: return Cell.WEAKENED;
                case Cell.INFECTED: return Cell.FLAGGED;
                case Cell.FLAGGED: return Cell.CLEAN;

            }
            
            return cell;
        }
    }
}