using System;
using System.Text;
using System.Collections.Generic;

namespace _16._Promenade
{
    class Dance
    {
        private char[] _programs = null;

        private int _start = 0;

        public Dance(int size)
        {
            // Init the "dancers"
            _programs = new char[size];
            for (int i = 0; i < size; ++i)
            {
                _programs[i] = (char)('a' + i);
            }

            // Instead of moving a lot of stuff in the array, let's keep a "start" offest
            _start = 0;
        }

        public void Spin(int offset)
        {
            // Just move the _start pointer
            _start = (_start + _programs.Length - offset) % _programs.Length;
        }

        public void Exchange(int i, int j)
        {
            char temp = _programs[(i + _start) % _programs.Length];
            _programs[(i + _start) % _programs.Length] = _programs[(j + _start) % _programs.Length];
            _programs[(j + _start) % _programs.Length] = temp;
        }

        public void Partner(char a, char b)
        {
            int x = 0;
            int y = 0;
            for (int i = 0; i < _programs.Length; ++i)
            {
                if (_programs[i] == a)
                {
                    x = i;
                }
                if (_programs[i] == b)
                {
                    y = i;
                }                
            }
            char temp = _programs[x];
            _programs[x] = _programs[y];
            _programs[y] = temp;
        }

        public override String ToString()
        {
            string output = "";
            for (int i = 0; i < _programs.Length; ++i)
            {
                output += _programs[(i + _start) % _programs.Length];
            }
            return output;
        }
    }
}