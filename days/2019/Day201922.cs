using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;
using System.Numerics;

namespace Aoc
{
    public class Day201922 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string[] _input;

        public Day201922()
        {
            Codename = "2019-22";
            Name = "Slam Shuffle";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetStringVector(this);

        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                IEnumerable<int> deck = Enumerable.Range(0, 10007);
                foreach (string s in _input)
                {
                    if (s[0..3] == "cut")
                    {
                        string amount = s[4..^0];
                        deck = Cut(deck, int.Parse(amount));
                        continue;
                    }

                    if (s[5..9] == "with")
                    {
                        string amount = s[20..^0];
                        deck = Increment(deck, int.Parse(amount));
                        continue;
                    }

                    deck = Deal(deck);
                }

                return deck.Select((a, i) => (a.Equals(2019)) ? i : -1).Max().ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // Find the original index of card 2020
                BigInteger target = 2020;
                BigInteger length = 119315717514047;
                BigInteger repeats = 101741582076661;

                // increment_mul = 1 = the difference between two adjacent numbers
                // Doing the process will multiply increment by increment_mul.
                BigInteger increment_mul = 1;

                // offset_diff = 0 = the first number in the sequence.
                // doing the process will increment this by offset_diff * (the increment before the process started).
                BigInteger offset_diff = 0;
                
                for (int i = 0; i < _input.Length; ++i)
                {
                    string s = _input[i];
                    if (s[0..3] == "cut")
                    {
                        // Get the amount
                        BigInteger amount = long.Parse(s[4..^0]);

                        // Shift q left
                        offset_diff += amount * increment_mul;
                        offset_diff %= length;
                        continue;
                    }

                    if (s[5..9] == "with")
                    {
                        // Get the amount
                        BigInteger amount = long.Parse(s[20..^0]);
                        
                        // Difference between two adjacent numbers is multiplied by the inverse of the increment.
                        increment_mul *= BigInteger.ModPow(amount, length - 2, length);
                        increment_mul %= length;
                        continue;
                    }

                    // Reverse sequence.
                    // Instead of going up, go down.
                    increment_mul *= -1;
                    increment_mul %= length;
                                        
                    // Tthen shift 1 left
                    offset_diff += increment_mul;
                    offset_diff %= length;
                }

                
                // Calculate (increment, offset) for the number of iterations of the process
                // Increment = increment_mul^iterations
                BigInteger increment = BigInteger.ModPow(increment_mul, repeats, length);
        
                // offset = 0 + offset_diff * (1 + increment_mul + increment_mul^2 + ... + increment_mul^iterations)
                // use geometric series.
                BigInteger offset = offset_diff * (1 - increment) * BigInteger.ModPow((1 - increment_mul) % length, length - 2, length);
                offset %= length;

                return ((offset + target * increment) % length).ToString();
            }

            return "";
        }

        private IEnumerable<int> Deal(IEnumerable<int> cards)
        {
            return cards.Reverse();
        }

        private IEnumerable<int> Cut(IEnumerable<int> cards, int n)
        {
            int amount = (n < 0) ? (cards.Count() + n) : n;
            IEnumerable<int> top = cards.Take(amount);
            IEnumerable<int> bottom = cards.Skip(amount);
            return bottom.Concat(top);
        }

        private IEnumerable<int> Increment(IEnumerable<int> cards, int n)
        {
            int[] result = new int[cards.Count()];
            int index = 0;
            foreach (int card in cards)
            {
                result[index] = card;
                index = (index + n) % result.Length;
            }

            return result.AsEnumerable();
        }
    }   
}