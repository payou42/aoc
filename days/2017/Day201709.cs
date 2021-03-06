using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Aoc.Common.Containers;

namespace Aoc
{
    public class Day201709 : Aoc.Framework.IDay
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private Tree<string> _tree;

        private int _garbage;
        
        public Day201709()
        {
            Codename = "2017-09";
            Name = "Stream Processing";
        }

        public void Init()
        {
            _garbage = 0;
            _tree = BuildTree(); 
        }

        public string Run(Aoc.Framework.Part part)
        {
            if (part == Aoc.Framework.Part.Part1)
            {
                return _tree.GetWeight(_tree.Root).ToString();
            }

            if (part == Aoc.Framework.Part.Part2)
            {
                return _garbage.ToString();
            }

            return "";
        }

        private Tree<string> BuildTree()
        {
            string content = Aoc.Framework.Input.GetString(this);
            Tree<string> tree = new Tree<string>("root");
            
            Tree<string>.Node current = tree.Root;
            int index = 0;
            bool garbage = false;
            
            while (index < content.Length)
            {
                // Process characters one by one.
                // Not the more efficient way but I don't like regex
                switch (content[index])
                {                    
                    case '!':
                    {
                        index++;
                        break;
                    }

                    case '{':
                    {
                        if (!garbage)
                        {
                            current = current.AddChild("", current.Weight + 1);
                        }
                        else
                        {
                            _garbage++;
                        }
                        break;
                    }

                    case '}':
                    {
                        if (!garbage)
                        {
                            current = current.Parent;
                        }
                        else
                        {
                            _garbage++;
                        }                        
                        break;
                    }

                    case '<':
                    {
                        if (garbage)
                        {
                            // Already in garbage
                            _garbage++;
                        }

                        garbage = true;
                        break;
                    }

                    case '>':
                    {
                        garbage = false;
                        break;                        
                    }

                    default:
                    {
                        if (garbage)
                        {
                            _garbage++;
                        }
                        break;
                    }
                }
                index++;
            }
            
            return tree;
        }
    }   
}