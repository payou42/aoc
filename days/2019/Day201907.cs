using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using Aoc.Common.Simulators;

namespace Aoc
{
    public class Day201907 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private IntCpu _cpu;

        private IntCpu[] _amplifiers;

        private long[] _code;

        public Day201907()
        {
            Codename = "2019-07";
            Name = "Amplification Circuit";
        }

        public void Init()
        {
            _code = Aoc.Framework.Input.GetLongVector(this, ",");
            _cpu = new IntCpu();
            _amplifiers = new IntCpu[5];
            for (int i = 0; i < 5; ++i)
            {
                _amplifiers[i] = new IntCpu();
            }

            _amplifiers[0].OnOutput += () => Pipe(0);
            _amplifiers[1].OnOutput += () => Pipe(1);
            _amplifiers[2].OnOutput += () => Pipe(2);
            _amplifiers[3].OnOutput += () => Pipe(3);
            _amplifiers[4].OnOutput += () => Pipe(4);
        }

        private void Pipe(int i)
        {
            _amplifiers[(i + 1) % 5].Input.Enqueue(_amplifiers[i].Output.Dequeue());
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                long max = TestAllSettings(DirectRun, 0, new List<int>(), new List<int> {0, 1, 2, 3, 4});
                return max.ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                long max = TestAllSettings(FeedbackRun, 0, new List<int>(), new List<int> {5, 6, 7, 8, 9});
                return max.ToString();
            }

            return "";
        }

        private long TestAllSettings(Func<List<int>, long> evaluator, long max, List<int> used, List<int> available)
        {
            if (available.Count == 0)
            {
                // Test this one
                return Math.Max(max, evaluator(used));
            }

            long output = max;
            for (int i = 0; i < available.Count; ++i)
            {
                int phase = available[i];
                used.Add(phase);
                available.RemoveAt(i);
                output = TestAllSettings(evaluator, output, used, available);
                available.Insert(i, phase);
                used.RemoveAt(used.Count - 1);
            }

            return output;
        }

        private long DirectRun(List<int> settings)
        {
            long output = 0;
            for (int i = 0; i < settings.Count; ++i)
            {
                _cpu.Reset(_code);
                _cpu.Input.Enqueue(settings[i]);
                _cpu.Input.Enqueue(output);
                _cpu.Run(true);
                output = _cpu.Output.Dequeue();
            }

            return output;
        }

        private long FeedbackRun(List<int> settings)
        {
            
            for (int i = 0; i < settings.Count; ++i)
            {
                _amplifiers[i].Reset(_code);
                _amplifiers[i].Input.Enqueue(settings[i]);
            }

            _amplifiers[0].Input.Enqueue(0);
            while (_amplifiers[0].State != IntCpu.RunningState.Halted)
            {
                for (int i = 0; i < settings.Count; ++i)
                {
                    _amplifiers[i].Run(true);
                }
            }

            return _amplifiers[0].Input.Dequeue();
        }
    }   
}