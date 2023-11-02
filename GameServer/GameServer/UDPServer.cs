using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GameServer
{

    public class UDPServer
    {
        private UdpClient _udpListener;

        public static void Main(string[] args)
        {
            int port = 5000;
            UDPServer server = new UDPServer(port);
            server.Start();
        }

        public UDPServer(int port)
        {
            _udpListener = new UdpClient(port);
            Console.WriteLine($"UDP Server is running on port {port}");
        }

        public void Start()
        {
            while (true)
            {
                try
                {
                    IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    byte[] receivedBytes = _udpListener.Receive(ref remoteEndPoint);
                    string receivedMessage = Encoding.UTF8.GetString(receivedBytes);
                    Console.WriteLine($"Received message: {receivedMessage}");

                    string responseMessage = "Server received: " + receivedMessage;
                    byte[] responseBytes = Encoding.UTF8.GetBytes(responseMessage);
                    _udpListener.Send(responseBytes, responseBytes.Length, remoteEndPoint);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex}");
                }
            }
        }

    }
}
