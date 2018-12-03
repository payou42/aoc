using System;
using System.Text;
using System.Collections.Generic;

namespace Aoc.Common.Containers
{
    public class SparseArray<T>
    {
        (long, long, T)[] _array;

        public long Length { get; private set; }

        public long RealLength { get; private set; }

        public SparseArray(long length)
        {
            Length = length;
            RealLength = length;
            _array = new (long, long, T)[length];
            for (long i = 0; i < length; ++i)
            {
                _array[i] = ((i - 1 + length) % length, (i + 1) % length, default(T));
            }
        }

        public T this[long index]
        {
            get
            {
                return _array[index].Item3;
            }

            set
            {
                _array[index].Item3 = value;
            }
        }

        public void RemoveAt(long index)
        {
            long previous = _array[index].Item1;
            long next =_array[index].Item2;
            _array[previous].Item2 = next;
            _array[next].Item1 = previous;
            RealLength--;
        }

        public long Previous(long index, long offset = 1)
        {
            long previous = index;
            while (offset > 0)
            {
                previous = _array[previous].Item1;
            }
            return previous;
        }

        public long Next(long index, long offset = 1)
        {
            long next = index;
            while (offset > 0)
            {
                next = _array[next].Item2;
                offset--;
            }
            return next;
        }
    }
}