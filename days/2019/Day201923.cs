using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Simulators;

namespace Aoc
{
    public class Day201923 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private IntCpu[] _network;

        private readonly int _size = 50;

        public Day201923()
        {
            Codename = "2019-23";
            Name = "Category Six";
        }

        public void Init()
        {
            _network = new IntCpu[_size];
            for (int i = 0; i < _size; ++i)
            {
                _network[i] = new IntCpu();
            }
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                // Reset and run all cpus
                for (int i = 0; i < _size; ++i)
                {
                    _network[i].Reset(Aoc.Framework.Input.GetLongVector(this, ","));
                    _network[i].Input.Enqueue(i);
                    _network[i].Run();
                }

                while (true)
                {
                    // Process Outputs
                    for (int i = 0; i < _size; ++i)
                    {
                        if (_network[i].Output.Count >= 3)
                        {
                            long addr = _network[i].Output.Dequeue();
                            long x = _network[i].Output.Dequeue();
                            long y = _network[i].Output.Dequeue();

                            if (addr == 255)
                            {
                                return y.ToString();
                            }

                            _network[addr].Input.Enqueue(x);
                            _network[addr].Input.Enqueue(y);
                        }
                    }

                    // Process Inputs
                    for (int i = 0; i < _size; ++i)
                    {
                        if (_network[i].State == IntCpu.RunningState.WaitingInput)
                        {
                            if (_network[i].Input.Count == 0)
                            {
                                _network[i].Input.Enqueue(-1);
                            }

                            _network[i].Run();
                        }
                    }
                }
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // Reset and run all cpus
                long natx = -1;
                long naty = -1;
                long lastSent = -2;

                for (int i = 0; i < _size; ++i)
                {
                    _network[i].Reset(Aoc.Framework.Input.GetLongVector(this, ","));
                    _network[i].Input.Enqueue(i);
                    _network[i].Run();
                }

                while (true)
                {
                    // Process Outputs
                    for (int i = 0; i < _size; ++i)
                    {
                        if (_network[i].Output.Count >= 3)
                        {
                            long addr = _network[i].Output.Dequeue();
                            long x = _network[i].Output.Dequeue();
                            long y = _network[i].Output.Dequeue();

                            if (addr == 255)
                            {
                                natx = x;
                                naty = y;
                            }
                            else
                            {
                                _network[addr].Input.Enqueue(x);
                                _network[addr].Input.Enqueue(y);
                            }
                        }
                    }

                    // NAT watchdog
                    if (_network.All(c => (c.State == IntCpu.RunningState.WaitingInput) && (c.Input.Count == 0)))
                    {
                        if (lastSent == naty)
                        {
                            return lastSent.ToString();
                        }
                        
                        _network[0].Input.Enqueue(natx);
                        _network[0].Input.Enqueue(naty);
                        lastSent = naty;
                    }

                    // Process Inputs
                    for (int i = 0; i < _size; ++i)
                    {
                        if (_network[i].State == IntCpu.RunningState.WaitingInput)
                        {
                            if (_network[i].Input.Count == 0)
                            {
                                _network[i].Input.Enqueue(-1);
                            }

                            _network[i].Run();
                        }
                    }
                }
            }

            return "";
        }
    }   
}