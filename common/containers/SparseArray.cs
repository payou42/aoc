using System;
using System.Text;
using System.Collections.Generic;

namespace Aoc.Common.Containers
{
    public class SparseArray<T>
    {
        public long Length { get; private set; }

        public long RealLength { get; private set; }

        public long First => _first;

        private readonly (long Previous, long Next, T Content)[] _array;

        private long _first;

        public SparseArray(long length)
        {
            Length = length;
            RealLength = length;
            _array = new (long, long, T)[length];
            _first = 0;
            for (long i = 0; i < length; ++i)
            {
                _array[i] = ((i - 1 + length) % length, (i + 1) % length, default(T));
            }
        }

        public T this[long index]
        {
            get
            {
                return _array[index].Content;
            }

            set
            {
                _array[index].Content = value;
            }
        }

        public void RemoveAt(long index)
        {
            long previous = _array[index].Previous;
            long next =_array[index].Next;

            if (index == _first)
            {
                _first = next;
            }

            _array[previous].Next = next;
            _array[next].Previous = previous;
            RealLength--;
        }

        public long Previous(long index, long offset = 1)
        {
            long previous = index;
            while (offset > 0)
            {
                previous = _array[previous].Previous;
            }

            return previous;
        }

        public long Next(long index, long offset = 1)
        {
            long next = index;
            while (offset > 0)
            {
                next = _array[next].Next;
                offset--;
            }

            return next;
        }
    }
}