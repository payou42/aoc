using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace _18._Duet
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create the computer
            Cpu A = new Cpu(0);
            Cpu B = new Cpu(1);

            // Run the program
            string[] instructions = Input.RAW.Split("\r\n");
            bool deadlock = false;
            Int64 sendsCount = 0;
            while (!deadlock)
            {
                while (A.Outbox.Count > 0)
                {
                    Int64 v = A.Outbox.Dequeue();
                    B.Inbox.Enqueue(v);
                }
                while (B.Outbox.Count > 0)
                {
                    Int64 v = B.Outbox.Dequeue();
                    A.Inbox.Enqueue(v);
                    sendsCount++;
                }
                deadlock = A.Execute(instructions) && B.Execute(instructions);                
            }
            Console.WriteLine("Sends count: {0}", sendsCount);
        }
    }
}
