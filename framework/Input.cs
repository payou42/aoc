using System;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace Aoc.Framework
{
    public class Input
    {
        public static string GetString(Day day)
        {
            try
            {
                // Get the path elements
                string[] items = day.Codename.Split("-");

                // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader($"./inputs/{items[0]}/{day.Codename}.txt"))
                {
                    // Read the stream to a string, and write the string to the console.
                    return sr.ReadToEnd();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string[] GetStringVector(Day day, string separator = "\r\n")
        {
            string raw = GetString(day);
            if (raw == null)
            {
                return null;
            }            
            
            return raw.Split(separator);
        }

        public static string[][] GetStringMatrix(Day day, string row = "\t", string column = "\r\n")
        {
            string raw = GetString(day);
            if (raw == null)
            {
                return null;
            }
            
            String[] lines = raw.Split(column);
            string[][] matrix = new string[lines.Length][];
            for (int i = 0; i < lines.Length; ++i)
            {                
                String[] cells = lines[i].Split(row);
                matrix[i] = new string[cells.Length];
                for (int j = 0; j < cells.Length; ++j)
                {
                    matrix[i][j] = cells[j];
                }
            }

            return matrix;
        }

        public static Int32 GetInt(Day day)
        {
            string raw = GetString(day);
            if (raw == null)
            {
                return 0;
            }            
            
            return Int32.Parse(raw);
        }
        public static Int32[] GetIntVector(Day day, string separator = "\r\n")
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

        public static Int32[][] GetIntMatrix(Day day, string row = "\t", string column = "\r\n")
        {
            string raw = GetString(day);
            if (raw == null)
            {
                return null;
            }
            
            String[] lines = raw.Split(column);
            Int32[][] matrix = new Int32[lines.Length][];
            for (int i = 0; i < lines.Length; ++i)
            {                
                String[] cells = lines[i].Split(row);
                matrix[i] = new Int32[cells.Length];
                for (int j = 0; j < cells.Length; ++j)
                {
                    matrix[i][j] = Int32.Parse(cells[j]);
                }
            }

            return matrix;
        }
    }
}
