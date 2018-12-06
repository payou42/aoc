using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Aoc.Common;

namespace Aoc
{
    public class Day201512 : Aoc.Framework.Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private string _input;

        public Day201512()
        {
            Codename = "2015-12";
            Name = "JSAbacusFramework.io";
        }

        public void Init()
        {
            _input = Aoc.Framework.Input.GetString(this);
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                // Load the content
                dynamic content = JsonConvert.DeserializeObject(_input);

                // Sum all of the JToken
                return GetJsonSum(content.Root, false).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                // Load the content
                dynamic content = JsonConvert.DeserializeObject(_input);

                // Sum all of the JToken
                return GetJsonSum(content.Root, true).ToString();
            }

            return "";
        }

        private long GetJsonSum(JToken node, bool excludeRed)
        {
            if (node.Type == JTokenType.Integer)
            {
                return (long)node;
            }
            else
            {
                long sum = 0;

                if (excludeRed && node.Type == JTokenType.Object)
                {
                    foreach (JToken child in node.Children())
                    {
                        if (child.Type == JTokenType.Property)
                        {
                            foreach (JToken v in child.Children())
                            {
                                if (v.Type == JTokenType.String)
                                {
                                    if ((string)v == "red")
                                    {
                                        return 0;
                                    }
                                }
                            }
                        }
                    }
                }
                foreach (JToken child in node.Children())
                {
                    sum += GetJsonSum(child, excludeRed);
                }
                return sum;
            }
        }
    }   
}