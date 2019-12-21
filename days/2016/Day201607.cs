using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201607 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string[] _ips;

        public Day201607()
        {
            Codename = "2016-07";
            Name = "Internet Protocol Version 7";
        }

        public void Init()
        {
            _ips = Aoc.Framework.Input.GetStringVector(this);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return _ips.Where(ip => HasTLS(ip)).Count().ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                return _ips.Where(ip => HasSSL(ip)).Count().ToString();
            }

            return "";
        }

        private bool HasTLS(string ip)
        {
            bool inside = false;
            bool hasAbba = false;
            for (int i = 0; i < ip.Length - 3; ++i)
            {
                if (ip[i] == '[') inside = true;
                if (ip[i] == ']') inside = false;
                if (ip[i + 1] != '[' && ip[i + 1] != ']' && ip[i + 2] == ip[i + 1] && ip[i + 3] == ip[i] && ip[i] != ip [i + 1])
                {
                    if (inside) return false;
                    hasAbba = true;
                }
            }
            return hasAbba;
        }

        private bool HasSSL(string ip)
        {
            bool inside = false;
            List<string> abas = new List<string>();
            List<string> babs = new List<string>();

            for (int i = 0; i < ip.Length - 2; ++i)
            {
                if (ip[i] == '[') inside = true;
                if (ip[i] == ']') inside = false;
                if (ip[i + 1] != '[' && ip[i + 1] != ']' && ip[i + 1] != ip[i] && ip[i] == ip[i + 2])
                {
                    if (inside)
                    {
                        babs.Add(ip.Substring(i, 3));
                    }
                    else
                    {
                        abas.Add(ip.Substring(i, 3));
                    }
                }
            }

            foreach (string aba in abas)
            {
                string bab = new String(new Char[3] { aba[1], aba[0], aba[1] });
                if (babs.Contains(bab))
                {
                    return true;
                }
            }
            return false;
        }
    }   
}