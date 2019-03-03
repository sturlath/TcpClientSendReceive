using Serilog;
using System;
using System.Windows.Forms;
using TcpClientLib;
using TcpClientLib.Helpers;

namespace WindowsForms
{
    public partial class Form1 : Form
    {
        public static Client client;

        public Form1()
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

            InitializeComponent();

            client = new Client();
            client.MainDataReceived += OnClient_MainDataReceived;

            //WindowsFormsSynchronizationContext.SetSynchronizationContext(new WindowsFormsSynchronizationContext());
            //WindowsFormsSynchronizationContext.AutoInstall = false;
        }
        private void btnConnectToService_Click(object sender, EventArgs e)
        {
            client.ConnectAsync(txtIpAddress.Text, Convert.ToInt32(txtPort.Text));

            // Check if the client failed to connect to the server
            if (!client.IsConnected.Value)
            {
                txbResponseFromServer.AppendText("\r\n" + client.IsConnected.ErrorMessage);
                Log.Debug("Not connected... {ErrorMessage}", client.IsConnected.ErrorMessage);
                //Do your re-connect etc..
            }
        }

        private void OnClient_MainDataReceived(object sender, DataReceivedArgs e)
        {
            // This never gets called after client.SendData(). 
            // Probably because of something like https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.control.invoke?view=netframework-4.7.2
            txbResponseFromServer.AppendText(e.Data);
        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            // This call does not fire the MainDataReceived event in WinForms but does in the console app!
            GenericResult<bool> test = client.SendData(txtDataToSend.Text);
        }
    }
}
