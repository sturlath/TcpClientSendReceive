using Serilog;
using TcpClientLib;
using TcpClientLib.Helpers;

namespace Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
             .MinimumLevel.Debug()
            .Enrich.FromLogContext()
             .WriteTo.Console()
             .CreateLogger();

            Log.Debug("Starting up the Client");

            // Give the Server some time to start up. Just in case...
            System.Threading.Thread.Sleep(1000);

            // Setup the client
            var client = new Client();
            client.MainDataReceived += OnClient_MainDataReceived;

            //The same host/port as the server
            client.Connect("localhost", 14000); 

            if (!client.IsConnected.Value)
            {
                Log.Debug("Not connected... {ErrorMessage}", client.IsConnected.ErrorMessage);
                //Do your re-connect etc..
            }

            //Generate and send some data
            for (var i = 0; i < 100; i++)
            {
                var data = $"Data from CLIENT number: {i.ToString()} ";

                GenericResult<bool> wasDataSent = client.SendData(data);

                if (wasDataSent.Succeeded)
                {
                    //Log.Debug("Data sent to server");
                }
                else
                    Log.Debug("Unsuccessful sending to server: {ErrorMessage}", wasDataSent.ErrorMessage);

                //Sleep and then send some data again
                System.Threading.Thread.Sleep(600);
            }
        }

        private static void OnClient_MainDataReceived(object sender, DataReceivedArgs e)
        {
            Log.Debug("Data from Server: {data}", e.Data);
        }
    }
}