using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common;

namespace Aoc
{
    public class Day201516 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        public Day201516()
        {
            Codename = "2015-16";
            Name = "Aunt Sue";
        }

        public void Init()
        {
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                Dictionary<string, int> target = new Dictionary<string, int>
                {
                    { "children", 3 },
                    { "cats", 7 },
                    { "samoyeds", 2 },
                    { "pomeranians", 3 }, 
                    { "akitas", 0 },
                    { "vizslas", 0 },
                    { "goldfish", 5 },
                    { "trees", 3 },
                    { "cars", 2 },
                    { "perfumes", 1 }
                };

                string[][] lines = Aoc.Framework.Input.GetStringMatrix(this, " ");
                
                for (int i = 0; i < lines.Length; ++i)                
                {
                    string[] line = lines[i];
                    bool valid = true;
                    for (int index = 2; index < line.Length; index += 2)
                    {
                        string property = line[index][0..^1];
                        string svalue = line[index + 1];
                        int value = int.Parse(svalue.EndsWith(',') ? svalue[0..^1] : svalue);
                        if (target[property] != value)
                        {
                            valid = false;
                            break;
                        }
                    }

                    if (valid)
                    {
                        return (i + 1).ToString();
                    }
                }
                return "";
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                Dictionary<string, int> target = new Dictionary<string, int>
                {
                    { "children", 3 },
                    { "cats", 7 },
                    { "samoyeds", 2 },
                    { "pomeranians", 3 }, 
                    { "akitas", 0 },
                    { "vizslas", 0 },
                    { "goldfish", 5 },
                    { "trees", 3 },
                    { "cars", 2 },
                    { "perfumes", 1 }
                };

                string[][] lines = Aoc.Framework.Input.GetStringMatrix(this, " ");
                
                for (int i = 0; i < lines.Length; ++i)                
                {
                    string[] line = lines[i];
                    bool valid = true;
                    for (int index = 2; index < line.Length; index += 2)
                    {
                        string property = line[index][0..^1];
                        string svalue = line[index + 1];
                        int value = int.Parse(svalue.EndsWith(',') ? svalue[0..^1] : svalue);
                        if (property == "cats" || property == "trees")
                        {
                            if (target[property] >= value)
                            {
                                valid = false;
                                break;
                            }
                        }
                        else if (property == "pomeranians" || property == "goldfish")
                        {
                            if (target[property] <= value)
                            {
                                valid = false;
                                break;
                            }                            
                        }
                        else if (target[property] != value)
                        {
                            valid = false;
                            break;
                        }
                    }

                    if (valid)
                    {
                        return (i + 1).ToString();
                    }
                }
                return "";
            }

            return "";
        }
    }   
}