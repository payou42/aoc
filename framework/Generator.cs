using System;
using System.Text;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;

namespace Aoc.Framework
{
    public class Generator
    {
        private static string FetchTitle(string year, string day)
        {
            // Read the web page
            string url = String.Format("https://adventofcode.com/{0}/day/{1}", year, day);
            WebClient client = new WebClient ();
            client.Headers.Add ("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            Stream data = client.OpenRead (url);
            StreamReader reader = new StreamReader (data);
            string s = reader.ReadToEnd();
            data.Close ();
            reader.Close ();

            // Find the title
            string fullTitle = s.Split("---")[1].Trim();
            string title = fullTitle.Split(":")[1].Trim();
            return title;
        }

        private static string FetchInput(string year, string day)
        {
            string s = "";
            try
            {
                // Read the web page
                string url = String.Format("https://adventofcode.com/{0}/day/{1}/input", year, day);
                WebClient client = new WebClient ();
                client.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/65.0.3298.3 Safari/537.36");
                client.Headers.Add("accept-encoding", "gzip, deflate, br");
                client.Headers.Add("accept-language", "fr-FR,fr;q=0.9,en-US;q=0.8,en;q=0.7");
                client.Headers.Add("upgrade-insecure-requests", "1");
                client.Headers.Add("cookie", GetCookie());                
                Stream data = client.OpenRead(url);

                switch (client.ResponseHeaders["content-encoding"])
                {
                    case "gzip":
                    {
                        using GZipStream decompressionStream = new GZipStream(data, CompressionMode.Decompress);
                        using StreamReader reader = new StreamReader(decompressionStream);
                        s = reader.ReadToEnd();
                        break;
                    }

                    case "deflate":
                    {
                        using DeflateStream decompressionStream = new DeflateStream(data, CompressionMode.Decompress);
                        using StreamReader reader = new StreamReader(decompressionStream);
                        s = reader.ReadToEnd();
                        break;
                    }

                    default:
                    {
                        using StreamReader reader = new StreamReader(data);
                        s = reader.ReadToEnd();
                        break;
                    }
                }

                data.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return s;
        }

        private static string GetCookie()
        {
            try
            {
                // Open the text file using a stream reader.
                using StreamReader sr = new StreamReader("./cookie.txt");
                
                // Read the stream to a string, and write the string to the console.
                return sr.ReadToEnd();
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static string GetTemplate()
        {
            try
            {
                // Open the text file using a stream reader.
                using StreamReader sr = new StreamReader("./framework/template.txt");
                
                // Read the stream to a string, and write the string to the console.
                return sr.ReadToEnd();
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static void StoreInput(string path, string codeName, string input)
        {
            string formatted = input.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "\r\n");
            if (formatted.EndsWith("\r\n"))
            {
                formatted = formatted[..^2];
            }

            try
            {
                // Open the text file using a stream writer.
                using StreamWriter sw = new StreamWriter($"./inputs/{path}/{codeName}.txt");
                
                // Write the content of the input in the file.
                sw.Write(formatted);
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static void StoreClass(string classPath, string className, string codeName, string realName)
        {
            string template = GetTemplate();
            template = template.Replace("{{class}}", className);
            template = template.Replace("{{code}}", codeName);
            template = template.Replace("{{name}}", realName);

            try
            {
                // Open the text file using a stream writer.
                using StreamWriter sw = new StreamWriter($"./days/{classPath}/{className}.cs");
                
                // Write the content of the input in the file.
                sw.Write(template);
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }            
        }        

        public static void Generate(string year, string day)
        {            
            // Get the data
            string className = "Day" + year + day.PadLeft(2, '0');
            string codeName  = year + "-" + day.PadLeft(2, '0');
            string realName  = FetchTitle(year, day);
            string input     = FetchInput(year, day);

            // Store the content in a txt file
            StoreInput(year.ToString(), codeName, input);

            // Store the new class
            StoreClass(year.ToString(), className, codeName, realName);
        }
    }
}