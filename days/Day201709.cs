using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace Aoc
{
    public class Day201709 : Day
    {
        public string Codename { get; private set; }

        public string Name { get; private set; }

        private Tree _root;

        private int _garbage;
        
        public Day201709()
        {            
            Codename = "2017-09";
            Name = "Stream Processing";
            _garbage = 0;
            _root = BuildTree();            
        }

        public string Run(Part part)
        {
            if (part == Part.Part1)
            {
                return _root.TotalWeight.ToString();
            }

            if (part == Part.Part2)
            {
                return _garbage.ToString();
            }

            return "";
        }

        private Tree BuildTree()
        {
            string content = Input.GetString(this);
            Tree current = new Tree("root", 0);
            Tree root = current;
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
                            current = current.AddChild("", current.LocalWeight + 1);
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
            root.EvaluateWeight();
            return root;
        }
    }   
}