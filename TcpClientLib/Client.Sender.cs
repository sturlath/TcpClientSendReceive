using Serilog;
using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using TcpClientLib.Helpers;

namespace TcpClientLib
{
    public sealed partial class Client
    {
        private sealed class Sender
        {
            internal async Task<GenericResult<bool>> SendData(byte[] data)
            {
                var response = new GenericResult<bool>(true);

                try
                {
                    if (_stream.CanWrite)
                    {
                        // transition the data to the thread and send it...
                        await WriteWithTimeout(_stream, data, timeoutMs: 5000);

                        // Same code as above without timeout
                        // await _stream.WriteAsync(data, 0, data.Length).ConfigureAwait(false);
                    }
                    else
                    {
                        response.Succeeded = false;
                        response.ErrorMessage = "Couldn't write to NetworkStream (_stream)";
                    }

                    return response;
                }
                catch (TaskCanceledException ex)
                {
                    var re = new GenericResult<bool>(ex);
                    re.ErrorMessage = "Timed out sending to the server. " + re.ErrorMessage;
                    return re;
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error sending data to server in Client.Sender.{SendData}()", nameof(SendData));
                    return new GenericResult<bool>(ex);
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

            private async Task WriteWithTimeout(Stream os, byte[] buf, int timeoutMs)
            {
                var tokenSource = new CancellationTokenSource(timeoutMs); // cancel after waitMs milliseconds.
                Task task = os.WriteAsync(buf, 0, buf.Length, tokenSource.Token);
                Task waitedTask = await Task.WhenAny(task, Task.Delay(-1, tokenSource.Token));
                await waitedTask; //Wait on the returned task to observe any exceptions.
            }

            private NetworkStream _stream;
            private Thread _thread;
        }
    }
}