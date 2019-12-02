using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201902 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private int[] _code;

        private int _ip;

        public Day201902()
        {
            Codename = "2019-02";
            Name = "1202 Program Alarm";
        }

        public void Init()
        {
            _code = Aoc.Framework.Input.GetIntVector(this, ",");
            _ip = 0;
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                _code[1] = 12;
                _code[2] = 2;
                Run();
                return _code[0].ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                for (int noun = 0; noun <= 99; noun++)
                {
                    for (int verb = 0; verb <= 99; verb++)
                    {
                        Init();
                        _code[1] = noun;
                        _code[2] = verb;
                        Run();
                        if (_code[0] == 19690720)
                        {
                            return (100 * noun + verb).ToString();
                        }
                    }
                }
                return "Not found";
            }

            return "";
        }

        private void Run()
        {
            while (_code[_ip] != 99)
            {
                switch (_code[_ip])
                {
                    case 1:
                    {
                        int a = _code[_code[_ip + 1]];
                        int b = _code[_code[_ip + 2]];
                        int x = _code[_ip + 3];
                        _code[x] = a + b;
                        _ip += 4;
                        break;
                    }

                    case 2:
                    {
                        int a = _code[_code[_ip + 1]];
                        int b = _code[_code[_ip + 2]];
                        int x = _code[_ip + 3];
                        _code[x] = a * b;
                        _ip += 4;
                        break;
                    }
                }
            }
        }
    }   
}