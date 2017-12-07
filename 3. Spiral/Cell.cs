using System;
using System.Collections.Generic;

namespace _3._Spiral
{
    class Cell
    {
        public Int32 X { get; set; }
        
        public Int32 Y { get; set; }

        public Int32 Content { get; set; }
    
        public Cell(int x, int y, int content)
        {
            X = x;
            Y = y;
            Content = content;
        }
    }
}
