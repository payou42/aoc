using System;
using System.Text;
using System.Net;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;

namespace Aoc.Framework
{
    public class Generator
    {
        public static string GetTitle(string year, string day)
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
            string title = s.Split("---")[1].Trim();
            return title;
        }
    }
}