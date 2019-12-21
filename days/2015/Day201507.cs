using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Logic;

namespace Aoc
{
    public class Day201507 : Aoc.Framework.IDay
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
                _circuit.Tick();

                // Get the output
                return _circuit.Registers["a"].ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // Naively activate each gates
                _circuit.Tick();

                // Reset the circuit
                UInt16 result = _circuit.Registers["a"];
                _circuit.Reset();
                
                // Hardwire the output b
                _circuit.Gates.Where(g => g.Output == "b").ToList();
                _circuit.Gates.Add(new GateWire { Inputs = new [] { result.ToString() }, Output = "b" });

                // Naively activate each gates
                _circuit.Tick();

                // Get the output
                return _circuit.Registers["a"].ToString();
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
                    _circuit.Gates.Add( new GateWire { Inputs = new [] { items[0] }, Output = items[2] } );
                    continue;
                }

                // Process all kind of commands
                switch (items[^4])
                {
                    // NOT dq -> dr
                    case "NOT":
                    {
                        _circuit.Gates.Add( new GateNot { Inputs = new [] { items[1] }, Output = items[3] } );
                        break;
                    }

                    // kg OR kf -> kh
                    case "OR":
                    {
                        _circuit.Gates.Add( new GateOr { Inputs = new [] { items[0], items[2] }, Output = items[4] } );
                        break;
                    }

                    // kg AND kf -> kh
                    case "AND":
                    {
                        _circuit.Gates.Add( new GateAnd { Inputs = new [] { items[0], items[2] }, Output = items[4] } );
                        break;
                    }
                    
                    // kg XOR kf -> kh                    
                    case "XOR":
                    {
                        _circuit.Gates.Add( new GateXor { Inputs = new [] { items[0], items[2] }, Output = items[4] } );
                        break;
                    }

                    // lf RSHIFT 2 -> lg
                    case "RSHIFT":
                    {
                        _circuit.Gates.Add( new GateRShift { Inputs = new [] { items[0], items[2] }, Output = items[4] } );
                        break;
                    }

                    // lf LSHIFT 2 -> lg
                    case "LSHIFT":
                    {
                        _circuit.Gates.Add( new GateLShift { Inputs = new [] { items[0], items[2] }, Output = items[4] } );
                        break;
                    }
                }
            }
        }
    }   
}