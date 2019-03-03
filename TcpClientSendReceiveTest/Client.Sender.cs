using Serilog;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TcpClientProgram
{
    internal sealed partial class Client
    {
        private sealed class Sender
        {
            internal void SendData(byte[] data)
            {
                try
                {
                    Log.Debug("Sending data from Client: '{data}'", Encoding.ASCII.GetString(data));

                    // transition the data to the thread and send it...
                    _stream.Write(data, 0, data.Length);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, $"Failed while executing Client.Sender.{nameof(SendData)}");
                    throw;
                }
            }

            internal Sender(NetworkStream stream)
            {
                _stream = stream;
                _thread = new Thread(Run);
                _thread.Start();
            }

            private void Run()
            {
                // main thread loop for sending data...
            }

            private NetworkStream _stream;
            private Thread _thread;
        }
    }
}
