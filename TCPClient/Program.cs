using System;

namespace TCPClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Worker w = new Worker();
            w.Start();
        }
    }
}
