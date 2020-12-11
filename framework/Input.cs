using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using Aoc.Common.Grid;

namespace Aoc.Framework
{
    public class Input
    {
        public static string GetString(IDay day)
        {
            try
            {
                // Get the path elements
                string[] items = day.Codename.Split("-");

                // Open the text file using a stream reader.
                using StreamReader sr = new StreamReader($"./inputs/{items[0]}/{day.Codename}.txt");
                
                // Read the stream to a string, and write the string to the console.
                return sr.ReadToEnd();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string[] GetStringVector(IDay day, string separator = "\r\n")
        {
            string raw = GetString(day);
            if (raw == null)
            {
                return null;
            }            
            
            return raw.Split(separator);
        }

        public static string[][] GetStringMatrix(IDay day, string column = "\t", string row = "\r\n")
        {
            string raw = GetString(day);
            if (raw == null)
            {
                return null;
            }
            
            String[] lines = raw.Split(row);
            string[][] matrix = new string[lines.Length][];
            for (int i = 0; i < lines.Length; ++i)
            {                
                String[] cells = lines[i].Split(column);
                matrix[i] = new string[cells.Length];
                for (int j = 0; j < cells.Length; ++j)
                {
                    matrix[i][j] = cells[j];
                }
            }

            return matrix;
        }

        public static Int32 GetInt(IDay day)
        {
            string raw = GetString(day);
            if (raw == null)
            {
                return 0;
            }            
            
            return Int32.Parse(raw);
        }
        public static Int32[] GetIntVector(IDay day, string separator = "\r\n")
        {
            string raw = GetString(day);
            if (raw == null)
            {
                return null;
            }            
            
            String[] lines = raw.Split(separator);
            Int32[] vector = new Int32[lines.Length];
            for (int i = 0; i < lines.Length; ++i)
            {
                vector[i] = Int32.Parse(lines[i]);
            }
            return vector;
        }

        public static Int64[] GetLongVector(IDay day, string separator = "\r\n")
        {
            string raw = GetString(day);
            if (raw == null)
            {
                return null;
            }            
            
            String[] lines = raw.Split(separator);
            Int64[] vector = new Int64[lines.Length];
            for (int i = 0; i < lines.Length; ++i)
            {
                vector[i] = Int64.Parse(lines[i]);
            }
            return vector;
        }

        public static Int32[][] GetIntMatrix(IDay day, string column = "\t", string row = "\r\n")
        {
            string raw = GetString(day);
            if (raw == null)
            {
                return null;
            }
            
            String[] lines = raw.Split(row);
            Int32[][] matrix = new Int32[lines.Length][];
            for (int i = 0; i < lines.Length; ++i)
            {                
                String[] cells = lines[i].Split(column);
                matrix[i] = new Int32[cells.Length];
                for (int j = 0; j < cells.Length; ++j)
                {
                    matrix[i][j] = Int32.Parse(cells[j]);
                }
            }

            return matrix;
        }

        public static Board<T> GetBoard<T>(IDay day, Func<char, T> parser, string row = "\r\n")
        {
            string raw = GetString(day);
            if (raw == null)
            {
                return null;
            }
            
            String[] lines = raw.Split(row);
            Board<T> board = new Board<T>();
            for (int y = 0; y < lines.Length; ++y)
            {
                for (int x = 0; x < lines[y].Length; ++x)
                {
                    board[x, y] = parser(lines[y][x]);
                }
            }

            return board;
        }
    }
}
