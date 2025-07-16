using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Services
{
    public static class ClientCommunicationHandler
    {
        public static void HandleTcp(string algoritam)
        {
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint serverEP = new IPEndPoint(IPAddress.Loopback, 50001);
            clientSocket.Connect(serverEP);

            Console.WriteLine("TCP klijent povezan sa serverom.");
        }

        public static void HandleUdp(string algoritam)
        {
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint serverEP = new IPEndPoint(IPAddress.Loopback, 50002);

            Console.WriteLine("UDP klijent spreman za slanje.");
        }
    }
}