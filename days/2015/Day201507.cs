using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Logic;

namespace Aoc
{
    public class Day201507 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string[] _commands;

        private Circuit _circuit;

        public Day201507()
        {
            Codename = "2015-07";
            Name = "Some Assembly Required";
        }

        public void Init()
        {
            _commands = Aoc.Framework.Input.GetStringVector(this);
            _circuit = new Circuit();
            ParseCommands();
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                // Naively activate each gates
                _circuit.Run();

                // Get the output
                return _circuit.Registers["a"].ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                return "part2";
            }

            return "";
        }

        private void ParseCommands()
        {
            foreach (string command in _commands)
            {
                // Get the items
                string[] items = command.Split(" ");

                // 44430 -> b
                if (items.Length == 3)
                {
                    if (UInt16.TryParse(items[0], out var a))
                    {
                        _circuit.Add( new GateConst { A = a, Output = items[2] } );
                    }
                    else
                    {
                        _circuit.Add( new GateWire { A = items[0], Output = items[2] } );
                    }
                    
                    continue;
                }

                // Process all kind of commands
                switch (items[items.Length - 4])
                {
                    // NOT dq -> dr
                    case "NOT":
                    {
                        _circuit.Add( new GateNot { A = items[1], Output = items[3] } );
                        break;
                    }

                    // kg OR kf -> kh
                    case "OR":
                    {
                        _circuit.Add( new GateOr { A = items[0], B = items[2], Output = items[4] } );
                        break;
                    }

                    // kg AND kf -> kh
                    case "AND":
                    {
                        _circuit.Add( new GateAnd { A = items[0], B = items[2], Output = items[4] } );
                        break;
                    }
                    
                    // kg XOR kf -> kh                    
                    case "XOR":
                    {
                        _circuit.Add( new GateXor { A = items[0], B = items[2], Output = items[4] } );
                        break;
                    }

                    // lf RSHIFT 2 -> lg
                    case "RSHIFT":
                    {
                        _circuit.Add( new GateRShift { A = items[0], B = UInt16.Parse(items[2]), Output = items[4] } );
                        break;
                    }

                    // lf LSHIFT 2 -> lg
                    case "LSHIFT":
                    {
                        _circuit.Add( new GateLShift { A = items[0], B = UInt16.Parse(items[2]), Output = items[4] } );
                        break;
                    }
                }
            }
        }
    }   
}