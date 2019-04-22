using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoginDemo.DTOs;
using LoginDemo.Models;
using LoginDemo.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoginDemo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsuarioDetailPage : ContentPage
    {
        public ModelUsuario Item { get; set; }
        public List<EnumSexo> ListaSexo { get; set; }

        public UsuarioDetailPage(USUARIO item)
        {
            InitializeComponent();
            ListaSexo = Enum.GetValues(typeof(EnumSexo)).Cast<EnumSexo>().ToList();
            Item = new ModelUsuario
            {
                Usuario = item
            };

            BindingContext = this;
        }

        async void Update_OnClicked(object sender, EventArgs e)
        {
            if (Item.Usuario.Contrasena.Equals(Item.Repetir))
            {
                MessagingCenter.Send(this, "UpdateUsuario", Item.Usuario);
                await Navigation.PopAsync();
            }
            else
            {
                DisplayAlert("Contraseña no coincide", "Los passwords insertados deben conicidir", "OK");
            }
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async void Delete_OnClicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "DeleteUsuario", Item.Usuario);
            await Navigation.PopAsync();
        }
    }
}