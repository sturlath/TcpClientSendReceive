using Serilog;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

[assembly: IgnoresAccessChecksTo("System.Diagnostics")]
namespace TcpClientProgram
{
    internal sealed partial class Client
    {
        private sealed class Receiver
        {
            public const int receiveBufferSize = 1024;
            private readonly byte[] receivebuffer = new byte[receiveBufferSize];
            private static ManualResetEvent ShutdownEvent = new ManualResetEvent(false);

            internal event EventHandler<DataReceivedEventArgs> DataReceived;
            internal event EventHandler<EventArgs> DataReceivedThatWorks;

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

                                Log.Debug("Got a response from the server: " + responseData);

                                // Raise the DataReceived event w/ data...
                                // HERE I HAVE TRIED ALL KINDS OF WAYS WITHOUT LUCK.
                                //OnDataReceived(this, new DataReceivedEventArgs(""));
                                //DataReceived?.Invoke(this, e);

                                //DataReceivedThatWorks?.Invoke(this, EventArgs.Empty);

                                OnDataReceivedThatWorks(this, EventArgs.Empty);
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
                            // Handle the exception...
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception...
                }
                finally
                {
                    _stream.Close();
                }
            }


            internal void OnDataReceived(object sender, DataReceivedEventArgs e)
            {
                //Invoke?
            }

            internal void OnDataReceivedThatWorks(object sender, EventArgs e)
            {
                DataReceivedThatWorks?.Invoke(this, EventArgs.Empty);
            }

            private NetworkStream _stream;
            private Thread _thread;
        }
    }
}

//Tried to beet the internal constructor with this  https://www.strathweb.com/2018/10/no-internalvisibleto-no-problem-bypassing-c-visibility-rules-with-roslyn/
namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class IgnoresAccessChecksToAttribute : Attribute
    {
        public IgnoresAccessChecksToAttribute(string assemblyName)
        {
            AssemblyName = assemblyName;
        }

        public string AssemblyName { get; }
    }
}