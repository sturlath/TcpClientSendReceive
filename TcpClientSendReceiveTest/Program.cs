using Serilog;
using System.Diagnostics;
using System.Text;

namespace TcpClientProgram
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
             .MinimumLevel.Debug()
             .Enrich.FromLogContext()
             .CreateLogger();


            var client = new Client("localhost", 14000);
            client.MainDataReceived += Client_MainDataReceived;
            client.MainDataReceivedThatWorks += Client_MainDataReceivedThatWorks;

            var data = Encoding.ASCII.GetBytes("Data from CLIENT!");

            do
            {
                client.SendData(data);

                //Sleep and then send some data again
                System.Threading.Thread.Sleep(3000);

            } while (true);

        }

        private static void Client_MainDataReceivedThatWorks(object sender, System.EventArgs e)
        {
            Log.Debug("Got some data back in Main().Client_MainDataReceivedThatWorks()");
        }

        private static void Client_MainDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            Log.Debug("Got some data back in Main().Client_MainDataReceived()");
        }
    }
}
