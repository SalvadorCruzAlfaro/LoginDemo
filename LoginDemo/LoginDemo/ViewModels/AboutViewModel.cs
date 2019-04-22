using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace LoginDemo.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "Acerca de...";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://github.com/SalvadorCruzAlfaro/LoginDemo")));
        }

        public ICommand OpenWebCommand { get; }
    }
}