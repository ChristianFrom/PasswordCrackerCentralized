using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using model;
using PasswordCrackerCentralized;
using util;

namespace DistributedPWCracking
{
    class Program
    {
        public static List<List<UserInfo>> chunks = new List<List<UserInfo>>();

        static void Main(string[] args)
        {


            SplitToChunks();

            StartTCP();

            //Cracking c = new Cracking();

            //foreach (var user in chunks)
            //{
            //    c.RunCracking(user);
            //}

            Console.WriteLine(chunks.Count);
            Console.ReadLine();
        }




        public static void StartTCP()
        {
            IPAddress IP = IPAddress.Any;
            TcpListener server = new TcpListener(IP, 7777);

            server.Start();

            while (true)
            {
                TcpClient socket = server.AcceptTcpClient();
                Console.WriteLine("Serveren er startet");

                //Starter ny tråd
                Task.Run(() =>
                { //indsætter en metode (delegate)
                    TcpClient tempSocket = socket;
                    Calculate(tempSocket);
                });
            }
        }


        public static void Calculate(TcpClient tempsocket)
        {

            using (StreamReader reader = new StreamReader(tempsocket.GetStream()))
            using (StreamWriter writer = new StreamWriter(tempsocket.GetStream()))
            {
                while (true)
                {
                    var str = reader.ReadLine();
                    if (str == "gib chunk")
                    {
                        writer.WriteLine("værsgo nibba");
                        writer.Flush();

                    }
                    else
                    {
                        writer.WriteLine("skrub af");
                        writer.Flush();

                    }
                }
            }
        }


        public static List<List<UserInfo>> SplitToChunks()
        {

            int ctr = 0;
            List<UserInfo> userInfos = PasswordFileHandler.ReadPasswordFile("passwords.txt");

            List<UserInfo> temp = new List<UserInfo>();

            foreach (var user in userInfos)
            {
                temp.Add(user);

                ctr++;
                if (ctr == 5)
                {
                    chunks.Add(temp);
                    ctr = 0;
                }
            }

            return chunks;
        }
    }
}
