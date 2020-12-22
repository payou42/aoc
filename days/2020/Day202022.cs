using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day202022 : Aoc.Framework.IDay
    {
        public enum Winner
        {
            None,
            Player1,
            Player2,
        };

        public string Codename { get; private set; }

        public string Name { get; private set; }

        private long[][] _decks;

        public Day202022()
        {
            Codename = "2020-22";
            Name = "Crab Combat";
        }

        public void Init()
        {
            var input = Aoc.Framework.Input.GetStringVector(this, "\r\n\r\n");
            _decks = input.Select(deck => deck.Split("\r\n").Skip(1).Select(long.Parse).ToArray()).ToArray();
        }

        public string Run(Aoc.Framework.Part part)
        {
            Queue<long> player1 = new Queue<long>(_decks[0]);
            Queue<long> player2 = new Queue<long>(_decks[1]);
            Combat(player1, player2, part);
            return CalculateScore(player1.Count > 0 ? player1 : player2).ToString();
        }

        private Winner Combat(Queue<long> player1, Queue<long> player2, Aoc.Framework.Part part)
        {
            HashSet<(long, long)> history = new HashSet<(long, long)>();
            Winner winner = Winner.None;
            while (winner == Winner.None)
            {
                winner = Round(player1, player2, history, part);
            }

            return winner;
        }

        private Winner Round(Queue<long> player1, Queue<long> player2, HashSet<(long, long)> history, Aoc.Framework.Part part)
        {
            // Check round history, only for part2
            if (part == Aoc.Framework.Part.Part2)
            {
                var h = CalculateHistory(player1, player2);
                if (history.Contains(h))
                {
                    return Winner.Player1;
                }

                history.Add(h);
            }

            // Draw top cards
            long card1 = player1.Dequeue();
            long card2 = player2.Dequeue();

            // Check remaining cards
            Winner roundWinner = Winner.None;
            if (part == Aoc.Framework.Part.Part2 && player1.Count >= card1 && player2.Count >= card2)
            {
                // Recursive game, only for part 2
                roundWinner = Combat(new Queue<long>(player1.Take((int)card1)), new Queue<long>(player2.Take((int)card2)), Aoc.Framework.Part.Part2);
            }
            else
            {
                roundWinner = (card1 > card2) ? Winner.Player1 : Winner.Player2;
            }

            // Check the round
            if (roundWinner == Winner.Player1)
            {
                player1.Enqueue(card1);
                player1.Enqueue(card2);
                if (player2.Count == 0)
                {
                    return Winner.Player1;
                }
            }
            else
            {
                player2.Enqueue(card2);
                player2.Enqueue(card1);
                if (player1.Count == 0)
                {
                    return Winner.Player2;
                }
            }

            return Winner.None;
        }

        private long CalculateScore(Queue<long> deck)
        {
            long c = deck.Count;
            if (c == 0)
                return 0;

            long v = deck.Dequeue();
            return (c * v) + CalculateScore(deck);
        }

        private (long, long) CalculateHistory(Queue<long> player1, Queue<long> player2)
        {
            var h1 = HashQueue(player1);
            var h2 = HashQueue(player2);
            return (h1, h2);
        }

        private long HashQueue(Queue<long> q)
        {
            long hash = 0;
            int index = 0;
            long temp = 0;
            foreach (long l in q)
            {
                temp = (temp << 6) + (l & 63);
                index++;
                if (index >= 10)
                {
                    hash ^= temp;
                    temp = 0;
                    index = 0;
                }
            }

            if (index > 0)
            {
                hash ^= temp;
            }

            return hash;
        }
    }
}