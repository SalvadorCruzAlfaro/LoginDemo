using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LoginDemo.Services;
using LoginDemo.Views;

namespace LoginDemo
{
    public partial class App : Application
    {
        //TODO: Replace with *.azurewebsites.net url after deploying backend to Azure
        public static string AzureBackendUrl = "http://logindemo.azurewebsites.net";
        public static bool UseMockDataStore = true;

        public App()
        {
            InitializeComponent();
            DependencyService.Register<AzureDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
