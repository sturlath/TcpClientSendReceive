using System;
using TcpClientLib;
using TcpClientLib.Helpers;
using TcpClientMobileApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TcpClientMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : ContentPage
    {
        public Client Client;
        private ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();

            Client = new Client();
            Client.MainDataReceived += OnClient_MainDataReceived;
        }

        private async void Connect_Clicked(object sender, EventArgs e)
        {
            GenericResult<bool> connectResponse = await Client.ConnectAsync(entryIPAddress.Text, Convert.ToInt32(entryPort.Text));

            if (connectResponse.HasError)
            {
                await DisplayAlert("Error", connectResponse.ErrorMessage, ":-(");
            }
        }

        private async void SendData_Clicked(object sender, EventArgs e)
        {
            if (Client.IsConnected)
            {
                GenericResult<bool> sendResponse = await Client.SendData("TEST DATA");

                if (sendResponse.HasError)
                {
                    await DisplayAlert("Error", sendResponse.ErrorMessage, ":-(");
                }
            }
        }

        private void OnClient_MainDataReceived(object sender, DataReceivedArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                // This gets called but it never fires the MainDataReceived event because _stream.DataAvailable (in Client.Receiver) is never true!
                // Except once just to show that this code works.
                await DisplayAlert("Success", $"Received this '{e.Data}' from the server!", ":-)");
            });
        }
    }
}