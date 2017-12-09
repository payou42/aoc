using System;
using System.Text;
using System.Collections.Generic;

namespace _9._Streams
{
    enum Command
    {
        NOPE,
        SKIP_NEXT,
        OPEN_GARBAGE,
        CLOSE_GARBAGE,
        OPEN_STREAM,
        CLOSE_STREAM
    }
}