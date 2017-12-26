using System;
using System.Text;
using System.Collections.Generic;

namespace Aoc.Framework
{
    public interface Day
    {
        string Codename { get; }

        string Name { get; }

        void Init();
        
        string Run(Part part);
    }
}
