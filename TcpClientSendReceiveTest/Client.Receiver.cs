using Serilog;
using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TcpClientSendReceiveTest.Helpers;

namespace TcpClientProgram
{
    internal sealed partial class Client
    {
        private sealed class Receiver
        {
            public const int receiveBufferSize = 1024;
            private readonly byte[] receivebuffer = new byte[receiveBufferSize];
            private static ManualResetEvent ShutdownEvent = new ManualResetEvent(false);

            internal event EventHandler<DataReceivedArgs> DataReceived;

            internal Receiver(NetworkStream stream)
            {
                _stream = stream;
                _thread = new Thread(Run);
                _thread.Start();
            }

            private void Run()
            {
                try
                {
                    // ShutdownEvent is a ManualResetEvent signaled by
                    // Client when its time to close the socket.
                    while (!ShutdownEvent.WaitOne(0))
                    {
                        try
                        {
                            // We could use the ReadTimeout property and let Read()
                            // block.  However, if no data is received prior to the
                            // timeout period expiring, an IOException occurs.
                            // While this can be handled, it leads to problems when
                            // debugging if we are wanting to break when exceptions
                            // are thrown (unless we explicitly ignore IOException,
                            // which I always forget to do).
                            if (!_stream.DataAvailable)
                            {
                                // Give up the remaining time slice.
                                Thread.Sleep(1);
                            }
                            else if (_stream.DataAvailable)
                            {
                                var b = new byte[1024];
                                var bytes = _stream.Read(b, 0, b.Length);
                                var responseData = Encoding.UTF8.GetString(b, 0, bytes);

                                //Log.Debug("Got a response from the server: " + responseData);

                                // Raise the DataReceived event w/ data...

                                OnDataReceived(this, new DataReceivedArgs(responseData));
                                //or just  DataReceivedThatWorks?.Invoke(this,  new DataReceivedArgs(responseData));
                            }
                            else
                            {
                                // The connection has closed gracefully, so stop the
                                // thread.
                                ShutdownEvent.Set();
                            }
                        }
                        catch (IOException ex)
                        {
                            Log.Error(ex, "Error (IOException) receiving from server");
                            //TOOD: Do some more handling
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error receiving from server");
                    //TOOD: Do some more handling
                }
                finally
                {
                    _stream.Close();
                }
            }

            internal void OnDataReceived(object sender, DataReceivedArgs e)
            {
                DataReceived?.Invoke(this, e);
            }

            private NetworkStream _stream;
            private Thread _thread;
        }
    }
}