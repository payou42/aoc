using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201809 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private int _players;

        private int _last;

        public Day201809()
        {
            Codename = "2018-09";
            Name = "Marble Mania";
        }

        public void Init()
        {
            string[] items = Aoc.Framework.Input.GetStringVector(this, " ");
            _players = int.Parse(items[0]);
            _last = int.Parse(items[6]);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return Play(_players, _last).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                return Play(_players, _last * 100).ToString();
            }

            return "";
        }

        private long Play(int players, int last)
        {
            int currentMarble = 0;
            int currentPlayer = 0;
            LinkedListNode<int> currentPosition = null;
            LinkedList<int> board = new LinkedList<int>();

            long[] scores = new long[players];

            while (currentMarble <= last)
            {
                if (board.Count == 0)
                {
                    currentPosition = board.AddLast(currentMarble);
                }
                if (currentMarble % 23 == 0)
                {
                    for (int i = 0; i < 7; ++i)
                    {
                        if (currentPosition == board.First)
                        {
                            currentPosition = board.Last;
                        }
                        else
                        {
                            currentPosition = currentPosition.Previous;
                        }
                    }
                    scores[currentPlayer] += currentMarble + currentPosition.Value;
                    LinkedListNode<int> next;
                    if (currentPosition == board.Last)
                    {
                        next = board.First;
                    }
                    else
                    {
                        next = currentPosition.Next;
                    }
                    board.Remove(currentPosition);
                    currentPosition = next;
                }
                else
                {
                    if (currentPosition == board.Last)
                    {
                        currentPosition = board.First;
                    }
                    else
                    {
                        currentPosition = currentPosition.Next;
                    }
                    currentPosition = board.AddAfter(currentPosition, currentMarble);
                }
                currentMarble++;
                currentPlayer = (currentPlayer + 1) % players;
            }

            return scores.Max();
        }
    }   
}