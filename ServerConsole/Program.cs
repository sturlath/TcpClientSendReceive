using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace TcpServerProgram
{
    internal class Program
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

        private static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();


            var ipAddress = "127.0.0.1";
            var port = 14000;

            var ip = IPAddress.Parse(ipAddress);
            var ServerSocket = new TcpListener(ip, port);
            ServerSocket.Start();

            Log.Debug("Started Server. Listening on IP:{ip}:{port}", ip, port);

            while (true)
            {
                TcpClient clientSocket = ServerSocket.AcceptTcpClient();
                Log.Debug("Client connected:{remote}", clientSocket.Client.RemoteEndPoint);

                lock (_lock) clients.Add(clientSocket);
                var client = new HandleClient();
                client.StartClient(clientSocket);
                Log.Debug("{count} clients connected", GetClientCount());
            }
        }
    }

    public class HandleClient
    {
        private TcpClient clientSocket;
        private static int bufferLength = 1024;
        private byte[] readBuffer = new byte[bufferLength];

        public void StartClient(TcpClient inClientSocket)
        {
            this.clientSocket = inClientSocket;
            var ctThread = new Thread(Chat);
            ctThread.Start();
        }

        private void Chat()
        {
            var reader = clientSocket.GetStream();

            try
            {
                while (true)
                {
                    var bytesRead = reader.Read(readBuffer, 0, bufferLength);

                    using (var memoryStream = new MemoryStream())
                    {
                        memoryStream.Write(readBuffer, 0, bytesRead);
                        var message = System.Text.Encoding.ASCII.GetString(memoryStream.ToArray());

                        Log.Debug("Server got this message from client: {message}", message);

                        foreach (TcpClient client in Program.GetClients())
                        {
                            var writer = new BinaryWriter(client.GetStream());
                            writer.Write($"Server got your message '{message}'");
                        }
                    }
                }
            }
            catch (EndOfStreamException)
            {
                Log.Error($"client disconnecting: {clientSocket.Client.RemoteEndPoint}");
                clientSocket.Client.Shutdown(SocketShutdown.Both);
            }
            catch (IOException e)
            {
                Log.Error($"IOException reading from {clientSocket.Client.RemoteEndPoint}: {e.Message}");
                Console.WriteLine();
            }
            catch (ObjectDisposedException e)
            {
                Log.Error(e, "Error (ObjectDisposedException) receiving from server");
            }
            catch (Exception e)
            {
                Log.Error(e, "Error receiving from server");
            }

            clientSocket.Close();
            Program.RemoveClient(clientSocket);
            Log.Debug("{count} clients connected", Program.GetClientCount());
        }
    }
}