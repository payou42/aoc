using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201608 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private int _width;
        
        private int _height;

        private bool[,] _screen;

        private string[] _input;

        public Day201608()
        {
            Codename = "2016-08";
            Name = "Two-Factor Authentication";
        }

        public void Init()
        {
            _width = 50;
            _height = 6;
            _screen = new bool[_width, _height];
            _input = Aoc.Framework.Input.GetStringVector(this);
            foreach (string s in _input)
            {
                string[] command = s.Split(" ");
                switch (command[0])
                {
                    case "rotate":
                    {
                        int amount = Int32.Parse(command[4]);
                        int what = Int32.Parse(command[2].Split("=")[1]);
                        if (command[1] == "row")
                        {
                            ShiftRow(what, amount);
                        }
                        else
                        {
                            ShiftColumn(what, amount);
                        }
                        break;
                    }

                    case "rect":
                    {
                        int[] param = command[1].Split("x").Select(item => Int32.Parse(item)).ToArray();
                        Rect(param[0], param[1]);
                        break;
                    }
                }
            }
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {                
                return CountPixels().ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                return "\n" + DrawScreen() + "\n";
            }

            return "";
        }

        private void Rect(int a, int b)
        {
            for (int i = 0; i < a; ++i)
            {
                for (int j = 0; j < b; ++j)
                {
                    _screen[i,j] = true;
                }
            }
        }

        private void ShiftRow(int row, int n = 1)
        {
            for (int k = 0; k < n; ++k)
            {
                bool saved = _screen[_width - 1, row];
                for (int i = _width - 1; i > 0; --i)
                {
                    _screen[i, row] = _screen[i - 1, row];
                }
                _screen[0, row] = saved;
            }
        }
        private void ShiftColumn(int column, int n = 1)
        {
            for (int k = 0; k < n; ++k)
            {
                bool saved = _screen[column, _height - 1];
                for (int i = _height - 1; i > 0; --i)
                {
                    _screen[column, i] = _screen[column, i - 1];
                }
                _screen[column, 0] = saved;
            }
        }

        private int CountPixels()
        {
            int count = 0;
            for (int i = 0; i < _width; ++i)
            {
                for (int j = 0; j < _height; ++j)
                {
                    count += _screen[i, j] ? 1 : 0;
                }
            }
            return count;
        }

        private string DrawScreen()
        {
            StringBuilder output = new StringBuilder();
            for (int j = 0; j < _height; ++j)
            {
                output.Append("\t");
                for (int i = 0; i < _width; ++i)
                {
                    output.Append(_screen[i, j] ? "#" : " ");
                }
                output.Append("\n");
            }
            return output.ToString();
        }
    }
}