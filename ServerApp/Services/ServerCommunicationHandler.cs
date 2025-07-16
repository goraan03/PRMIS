using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.Services
{
    public static class ServerCommunicationHandler
    {
        public static void HandleTcp(Socket tcpSocket)
        {
            Socket acceptedSocket = tcpSocket.Accept();
            IPEndPoint clientEP = acceptedSocket.RemoteEndPoint as IPEndPoint;
            Console.WriteLine($"TCP klijent povezan: {clientEP}");
        }

        public static void HandleUdp(Socket udpSocket)
        {
            EndPoint clientEP = new IPEndPoint(IPAddress.Any, 0);
            byte[] buffer = new byte[4096];
            int received = udpSocket.ReceiveFrom(buffer, ref clientEP);
            byte[] validData = buffer.Take(received).ToArray();
        }
    }
}