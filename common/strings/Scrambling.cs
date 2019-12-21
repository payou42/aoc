using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Aoc.Common.Strings
{
    public static class Scrambling
    {
        public static string Reverse(string input)
        {
            char[] charArray = input.ToCharArray();
            Array.Reverse( charArray );
            return new string( charArray );
        }

        public static string ReplaceLetter(string input, char x, char y)
        {
            return input.Replace(x, '.').Replace(y, x).Replace('.', y);
        }

        public static string ReplacePosition(string input, int x, int y)
        {
            char cx = input[x];
            char cy = input[y];
            return ReplaceLetter(input, cx, cy);
        }

        public static string ReversePart(string input, int x, int y)
        {
            return (x > 0 ? input.Substring(0, x) : "") + Reverse(input[x..(y + 1)]) + (y < input.Length - 1 ? input.Substring(y + 1) : "");
        }

        public static string Splice(string input, int start, int length, string replacement = "")
        {
            int x = start;
            int y = x + length - 1;
            return (x > 0 ? input.Substring(0, x) : "") + replacement + (y < input.Length - 1 ? input.Substring(y + 1) : "");
        }

        public static string MovePart(string input, int from, int to)
        {
            string s = input.Substring(from, 1);
            return input.Remove(from, 1).Insert(to, s);            
        }

        public static string RotateLeft(string input, int amount)
        {
            int n = amount % input.Length;
            if (n == 0)
            {
                return input;
            }
            return input.Substring(n) + input.Substring(0, n);
        }

        public static string RotateRight(string input, int amount)
        {
            int n = amount % input.Length;
            if (n == 0)
            {
                return input;
            }
            return input.Substring(input.Length - n) + input.Substring(0, input.Length - n);            
        }
        public static string RotateLetter(string input, char letter)
        {
            int n = input.IndexOf(letter);
            n = ((n >= 4 ? 2 : 1) + n) % input.Length;
            if (n == 0)
            {
                return input;
            }
            return input.Substring(input.Length - n) + input.Substring(0, input.Length - n);
        }
    }
}