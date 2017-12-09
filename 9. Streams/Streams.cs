using System;
using System.Text;
using System.Collections.Generic;

namespace _9._Streams
{
    class Streams
    {
        public Int32 Score { get; set; }
        public Streams Parent { get; }
        public List<Streams> Children { get; }

        public Streams() 
        {
            Score = 0;
            Children = new List<Streams>();
            Parent = null;
        }

        public Streams(Streams parent) : this()
        {
            Score = 1 + parent.Score;
            Parent = parent;
        }

        public int GetTotalScore()
        {
            int score = Score;
            foreach (Streams child in Children)
            {
                score += child.GetTotalScore();
            }
            return score;
        }
    }
}