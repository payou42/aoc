using System;

namespace _4._Passphrase
{
    class Program
    {
        static string[][] CONTENT;

        static void ParseRaw()
        {            
            String[] lines = Input.RAW.Split("\r\n");
            CONTENT = new string[lines.Length][];
            for (int i = 0; i < lines.Length; ++i)
            {                
                String[] cells = lines[i].Split(" ");
                CONTENT[i] = new string[cells.Length];
                for (int j = 0; j < cells.Length; ++j)
                {
                    CONTENT[i][j] = cells[j];
                }
            }            
        }

        static bool IsAnagram(string a, string b)
        {
            for (int i = 'a'; i <= 'z'; ++i)
            {
                int ca = 0;
                int cb = 0;
                for (int ia = 0; ia < a.Length; ++ia)
                {
                    if (a[ia] == i)
                    {
                        ca++;
                    }
                }
                for (int ib = 0; ib < b.Length; ++ib)
                {
                    if (b[ib] == i)
                    {
                        cb++;
                    }
                }                
                if (ca != cb)
                {
                    return false;
                }
            }
            return true;
        }

        static bool CheckWords(string a, string b)
        {
            // First half
            // return a == b;

            // Second half
            return IsAnagram(a, b);    
        }

        static bool ValidateRow(int index)
        {
            for (int i = 0; i < CONTENT[index].Length - 1; ++i)
            {
                for (int j = i + 1; j < CONTENT[index].Length; ++j)
                {
                    if (CheckWords(CONTENT[index][i], CONTENT[index][j]))
                    {
                        return false;
                    }
                }
            }            
            return true;
        }

        static int CountValid()
        {
            int valid = 0;
            for (int i = 0; i < CONTENT.Length; ++i)
            {
                if (ValidateRow(i))
                {
                    valid++;
                }                
            }
            return valid;
        }

        static void Main(string[] args)
        {
            ParseRaw();
            Console.WriteLine("Valid passphrase = {0}", CountValid());
        }
    }
}
