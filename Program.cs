using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace udp
{
    class Program
    {
        private static int listenerPort = 11000;
        private static int senderPort = 11001;
        static void Main(string[] args)
        {
            string identificator = Environment.GetEnvironmentVariable("id");  // 1 - Receiver, 2 - Sender

            if (identificator == "1") { // Receiver server
                UdpClient udpClient = new UdpClient(listenerPort);  // Create UDP client
                Console.WriteLine("Application is running, ready to receive messages.\n");  // Write initialisation message

                while(true) {
                    IPEndPoint listeningEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), senderPort);    // Make listener endpoint (localhost and sender port)
                    
                    Byte[] messageBytes = udpClient.Receive(ref listeningEndPoint);     // Receive incoming byte array from endpoint
                    string receivedMessage = Encoding.ASCII.GetString(messageBytes);    // Convert to string
                    
                    Console.Write("> ");                    // Write "> " before each received message
                    Console.WriteLine(receivedMessage);     // Display received message in terminal
                }

            } else {    // Sender application
                UdpClient udpClient = new UdpClient(senderPort);    // Create UDP client
                Console.WriteLine("Application is running, now you can enter messages.\n"); // Write initialisation message

                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), listenerPort);   // Create new ip endpoint (receiver, with listener port)
                udpClient.Connect(endPoint);    // Connect to reciever server

                while(true) {                               // Enter in infinite loop
                    Console.Write("Message: ");             // Write "Message: " before each input
                    string message = Console.ReadLine();    // Read input from console

                    Byte[] messageBytes = Encoding.ASCII.GetBytes(message); // Convert string to byte array
                    udpClient.Send(messageBytes, messageBytes.Length);      // Send byte array over UDP to receiver port ("1217.0.0.1", listenerPort);
                    Console.WriteLine("Message sent.\n");

                }
            }

        }
    }
}
