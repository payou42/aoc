using System;
using System.Text;
using System.Collections.Generic;

namespace _7._Circus
{
    class Program
    {      
        static Dictionary<String, Element> ELEMENTS;

        static void ParseRaw()
        {
            ELEMENTS = new Dictionary<String, Element>();
            String[] lines = Input.RAW.Split("\r\n");

            // First build the list of elements
            for (int i = 0; i < lines.Length; ++i)
            {
                String[] items = lines[i].Split(" ");
                String name = items[0];
                Int32 weight = Int32.Parse(items[1].Substring(1, items[1].Length - 2));
                ELEMENTS[name] = new Element(name, weight);
            }

            // Then build dependencies
            for (int i = 0; i < lines.Length; ++i)
            {
                String[] items = lines[i].Split(" ");                
                if (items.Length > 3)
                {
                    Element parent = ELEMENTS[items[0]];
                    for (int j = 3; j < items.Length; ++j)
                    {
                        String childName = items[j];
                        if (childName[childName.Length - 1] == ',')
                        {
                            childName = childName.Substring(0, childName.Length - 1);
                        }
                        Element child = ELEMENTS[childName];
                        child.IsRoot = false;
                        parent.Children.Add(child);
                    }
                }
            }            
        }

        static void Main(string[] args)
        {
            // Load the data
            ParseRaw();

            // Find the root
            Element root = null;
            foreach(KeyValuePair<String, Element> element in ELEMENTS)
            {
                if (element.Value.IsRoot)
                {
                    root = element.Value;
                    break;
                }
            }
            Console.WriteLine("Root detected: {0}", root.Name);

            // Evaluate weights
            root.EvaluateWeight();

            // Find unbalanced elements
            root.CheckBalanced();
        }
    }
}
