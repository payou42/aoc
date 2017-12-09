using System;
using System.Text;
using System.Collections.Generic;

namespace _9._Streams
{
    class Program
    {
        static Command ProcessCharacter(string content, int index) {
            switch (content[index])
            {
                case '{': return Command.OPEN_STREAM;
                case '}': return Command.CLOSE_STREAM;
                case '<': return Command.OPEN_GARBAGE;
                case '>': return Command.CLOSE_GARBAGE;
                case '!': return Command.SKIP_NEXT;
                default: return Command.NOPE;
            }
        }

        static Streams ProcessRaw()
        {
            string content = Input.RAW;
            Streams current = new Streams();
            Streams root = current;
            int index = 0;
            bool garbage = false;
            int garbageCount = 0;
            while (index < content.Length) 
            {
                // Process characters one by one.
                // Not the more efficient way but I don't like regex
                Command command = ProcessCharacter(content, index);                
                switch (command)
                {
                    case Command.NOPE:
                    {
                        if (garbage)
                        {
                            garbageCount++;
                        }
                        break;
                    }

                    case Command.SKIP_NEXT:
                    {
                        index++;
                        break;
                    }

                    case Command.OPEN_STREAM:
                    {
                        if (!garbage)
                        {
                            Streams child = new Streams(current);
                            current.Children.Add(child);
                            current = child;
                        }
                        else
                        {
                            garbageCount++;
                        }
                        break;
                    }

                    case Command.CLOSE_STREAM:
                    {
                        if (!garbage)
                        {
                            current = current.Parent;
                        }
                        else
                        {
                            garbageCount++;
                        }                        
                        break;
                    }

                    case Command.OPEN_GARBAGE:
                    {
                        if (garbage)
                        {
                            // Already in garbage
                            garbageCount++;
                        }
                        garbage = true;
                        break;
                    }

                    case Command.CLOSE_GARBAGE:
                    {
                        garbage = false;
                        break;                        
                    }
                }
                index++;
            }
            Console.WriteLine("Garbage count: {0}", garbageCount);
            return root;
        }

        static void Main(string[] args)
        {
            // Process the data
            Streams root = ProcessRaw();

            // Dump score
            Console.WriteLine("Total score: {0}", root.GetTotalScore());

        }
    }
}
