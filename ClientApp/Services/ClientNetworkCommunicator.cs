using Common;
using Common.Helpers;
using Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Services
{
    public static class ClientNetworkCommunicator
    {
        public static void SendAndReceiveMessageTCP(Socket clientSocket, byte[] cryptoPayload, string algoritam)
        {
            while (true)
            {
                if (algoritam == "DES")
                {
                    try
                    {
                        Console.WriteLine("\n==================== CLIENT TCP [DES] KOMUNIKACIJA ====================");

                        Console.WriteLine("\n>> Unesi poruku koju želiš da pošalješ serveru:");
                        string poruka = Console.ReadLine();

                        byte[] buffer = new byte[4096];
                        byte[] kljuc = cryptoPayload.Skip(32).Take(8).ToArray();
                        byte[] iv = cryptoPayload.Skip(40).Take(8).ToArray();

                        DesAlgorithm desAlg = new DesAlgorithm(poruka, kljuc, iv);
                        byte[] encryptedMessage = desAlg.Encrypt();
                        string base64Message = Convert.ToBase64String(encryptedMessage);

                        Console.WriteLine("\n>> Enkriptovana poruka (Base64 format):");
                        Console.WriteLine(base64Message);

                        int brBajta = clientSocket.Send(Encoding.UTF8.GetBytes(base64Message));

                        brBajta = clientSocket.Receive(buffer);
                        string echoMessage = Encoding.UTF8.GetString(buffer, 0, brBajta);

                        Console.WriteLine("\n>> Primljena eho poruka od servera (Base64 format):");
                        Console.WriteLine(echoMessage);

                        DesAlgorithm desAlgEcho = new DesAlgorithm(echoMessage, kljuc, iv);
                        string decryptedMessage = desAlgEcho.Decrypt(Convert.FromBase64String(echoMessage));

                        Console.WriteLine("\n>> Dekriptovana eho poruka:");
                        Console.WriteLine(decryptedMessage);

                        Console.WriteLine("\n================================================================\n");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Greska: {ex}");
                    }
                }
                else if (algoritam == "RSA")
                {
                    Console.WriteLine("Nije jos implementirano");
                }
            }
        }

        public static void SendAndReceiveMessageUDP(Socket clientSocket, byte[] cryptoPayload, string algoritam)
        {
            EndPoint serverEP = new IPEndPoint(IPAddress.Loopback, 50002);

            while (true)
            {
                if (algoritam == "DES")
                {
                    try
                    {
                        Console.WriteLine("\n==================== CLIENT UDP [DES] KOMUNIKACIJA ====================");

                        Console.WriteLine("\n>> Unesi poruku koju želiš da pošalješ serveru:");
                        string poruka = Console.ReadLine();

                        byte[] buffer = new byte[4096];
                        byte[] kljuc = cryptoPayload.Skip(32).Take(8).ToArray();
                        byte[] iv = cryptoPayload.Skip(40).Take(8).ToArray();

                        DesAlgorithm desAlg = new DesAlgorithm(poruka, kljuc, iv);
                        byte[] encryptedMessage = desAlg.Encrypt();
                        string base64Message = Convert.ToBase64String(encryptedMessage);

                        Console.WriteLine("\n>> Enkriptovana poruka (Base64 format):");
                        Console.WriteLine(base64Message);

                        clientSocket.SendTo(Encoding.UTF8.GetBytes(base64Message), serverEP);

                        int brBajta = clientSocket.ReceiveFrom(buffer, ref serverEP);
                        string echoMessage = Encoding.UTF8.GetString(buffer, 0, brBajta);

                        Console.WriteLine("\n>> Primljena eho poruka od servera (Base64 format):");
                        Console.WriteLine(echoMessage);

                        DesAlgorithm desAlgEcho = new DesAlgorithm(echoMessage, kljuc, iv);
                        string decryptedMessage = desAlgEcho.Decrypt(Convert.FromBase64String(echoMessage));

                        Console.WriteLine("\n>> Dekriptovana eho poruka:");
                        Console.WriteLine(decryptedMessage);

                        Console.WriteLine("\n================================================================\n");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Greska: {ex}");
                    }
                }
                else if (algoritam == "RSA")
                {
                    Console.WriteLine("Nije jos implementirano");
                }
            }
        }
    }
}
