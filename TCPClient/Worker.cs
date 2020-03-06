using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace TCPClient
{
    class Worker
    {
        public void Start()
        {
            while (true)
            {
                using (TcpClient clientSocket = new TcpClient("localhost", 7777))
                using (StreamReader reader = new StreamReader(clientSocket.GetStream()))
                using (StreamWriter writer = new StreamWriter(clientSocket.GetStream()))
                {
                    while (true)
                    {
                        Console.WriteLine("Client started");

                        string str = Console.ReadLine();
                        writer.WriteLine(str);
                        writer.Flush();
                        string stringReader = reader.ReadLine();
                        Console.WriteLine(stringReader);


                    }
                }
            }
        }
    }
}
