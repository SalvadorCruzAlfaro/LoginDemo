using LoginDemo.DTOs;
using LoginDemo.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoginDemo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsuarioPage : ContentPage
    {
        UsuarioViewModel viewModel;

        public UsuarioPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new UsuarioViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as USUARIO;
            if (item == null)
                return;

            //await Navigation.PushAsync(new UsuarioDetailPage(new UsuarioDetailViewModel(item)));
            await Navigation.PushAsync(new UsuarioDetailPage(item));

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new UsuarioAddPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Elementos.Count == 0)
                viewModel.ListarCommand.Execute(null);
        }
        
    }
}
