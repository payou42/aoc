using System;
using System.Text;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201722 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private Board<Cell> _board;

        private Direction _direction;

        private Point _position;

        private int _infections;

        public Day201722()
        {
            Codename = "2017-22";
            Name = "Sporifica Virus";
        }

        public void Init()
        {
        }

        public string Run(Aoc.Framework.Part part)
        {
            BuildBoard(Aoc.Framework.Input.GetStringVector(this));
            _position.X = 0;
            _position.Y = 0;
            _direction = Direction.Up;
            _infections = 0;
            int steps = (part == Aoc.Framework.Part.Part1) ? 10000 : 10000000;            
            for (int i = 0; i < steps; ++i)
            {
                Burst(part);
            }
            return _infections.ToString();;
        }

        private void BuildBoard(string[] lines)
        {
            _board = new Board<Cell>();
            int start = - ((lines.Length - 1) / 2);
            for (int j = 0; j < lines.Length; ++j)
            {
                for (int i = 0; i < lines[j].Length; ++i)
                {
                    _board[start + i, start + j] = (lines[lines.Length - 1 - j][i] == '#') ? Cell.Infected : Cell.Clean;
                }         
            }
        }

        private void Burst(Aoc.Framework.Part part)
        {
            Direction turnDirection = GetTurnDirection();
            _direction = Board<Cell>.Turn(_direction, turnDirection);
            Infect(part);
            _position = Board<Int64>.MoveForward(_position, _direction);
        }

        private Direction GetTurnDirection()
        {
            switch (_board[_position.X, _position.Y])
            {
                case Cell.Clean: return Direction.Left;
                case Cell.Weakened: return Direction.Up;
                case Cell.Infected: return Direction.Right;
                case Cell.Flagged: return Direction.Down;
            }
            return Direction.Up;
        }

        private void Infect(Aoc.Framework.Part part)
        {
            // Part 1
            if (part == Aoc.Framework.Part.Part1)
            {
                _infections += (_board[_position.X, _position.Y] == Cell.Clean) ? 1 : 0;
                _board[_position.X, _position.Y] = (Cell)(((int)_board[_position.X, _position.Y] + 2) % ((int)Cell.Count) );
                return;
            }

            // Part 2
            if (part == Aoc.Framework.Part.Part2)
            {
                _infections += (_board[_position.X, _position.Y] == Cell.Weakened) ? 1 : 0;
                _board[_position.X, _position.Y] = (Cell)(((int)_board[_position.X, _position.Y] + 1) % ((int)Cell.Count) );
                return;
            }
        }
    }

    public enum Cell
    {
        Clean = 0,
        
        Weakened = 1,
        
        Infected = 2,
        
        Flagged = 3,
        
        Count = 4
    }
}