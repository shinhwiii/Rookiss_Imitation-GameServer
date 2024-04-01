﻿using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerCore
{
    class Program
    {
        static Listener _listener = new Listener();

        static void OnAcceptHandler(Socket clientSocket)
        {
            // 받는다
            byte[] recvBuff = new byte[1024];
            int recvBytes = clientSocket.Receive(recvBuff);
            string recvData = Encoding.UTF8.GetString(recvBuff, 0, recvBytes);
            Console.WriteLine($"[From Client] {recvData}");

            // 보낸다
            byte[] sendBuff = Encoding.UTF8.GetBytes("Welcome to MMORPG Server");
            clientSocket.Send(sendBuff);

            // 쫓아낸다
            clientSocket.Shutdown(SocketShutdown.Both);
            clientSocket.Close();
        }

        static void Main(string[] args)
        {
            // DNS (Domain Name System) : www.shinhwi.com => 123.123.123.12
            string host = Dns.GetHostName();
            IPHostEntry ipHost = Dns.GetHostEntry(host);
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777); // 7777은 포트 번호 (임의로)

            _listener.Init(endPoint, OnAcceptHandler);
            Console.WriteLine("Listening...");

            while (true)
            {

            }
        }
    }
}