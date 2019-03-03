using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TcpServerProgram
{
    class Program
    {
        private static readonly object _lock = new object();
        private static readonly List<TcpClient> clients = new List<TcpClient>();

        public static TcpClient[] GetClients()
        {
            lock (_lock) return clients.ToArray();
        }

        public static int GetClientCount()
        {
            lock (_lock) return clients.Count;
        }

        public static void RemoveClient(TcpClient client)
        {
            lock (_lock) clients.Remove(client);
        }

        static void Main(string[] args)
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            TcpListener ServerSocket = new TcpListener(ip, 14000);
            ServerSocket.Start();

            Console.WriteLine("Server started.");
            while (true)
            {
                TcpClient clientSocket = ServerSocket.AcceptTcpClient();
                Console.WriteLine($"client connected: {clientSocket.Client.RemoteEndPoint}");
                lock (_lock) clients.Add(clientSocket);
                handleClient client = new handleClient();
                client.startClient(clientSocket);
                Log.Debug("Serilog TEST");
                Console.WriteLine($"{GetClientCount()} clients connected");
            }
        }
    }

    public class handleClient
    {
        TcpClient clientSocket;

        public void startClient(TcpClient inClientSocket)
        {
            this.clientSocket = inClientSocket;
            Thread ctThread = new Thread(Chat);
            ctThread.Start();
        }

        private void Chat()
        {
            BinaryReader reader = new BinaryReader(clientSocket.GetStream());

            try
            {
                while (true)
                {
                    string message = reader.ReadString();
                    foreach (var client in Program.GetClients())
                    {
                        BinaryWriter writer = new BinaryWriter(client.GetStream());
                        writer.Write("Hi from the Server!");
                    }
                }
            }
            catch (EndOfStreamException)
            {
                Console.WriteLine($"client disconnecting: {clientSocket.Client.RemoteEndPoint}");
                clientSocket.Client.Shutdown(SocketShutdown.Both);
            }
            catch (IOException e)
            {
                Console.WriteLine($"IOException reading from {clientSocket.Client.RemoteEndPoint}: {e.Message}");
            }

            clientSocket.Close();
            Program.RemoveClient(clientSocket);
            Console.WriteLine($"{Program.GetClientCount()} clients connected");
        }
    }
}
