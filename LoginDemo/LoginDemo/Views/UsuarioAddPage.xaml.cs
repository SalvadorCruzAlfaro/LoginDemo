using LoginDemo.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoginDemo.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoginDemo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsuarioAddPage : ContentPage
    {
        public ModelUsuario Item { get; set; }

        public List<EnumSexo> ListaSexo { get; set; }

        public UsuarioAddPage()
        {
            InitializeComponent();
            ListaSexo = Enum.GetValues(typeof(EnumSexo)).Cast<EnumSexo>().ToList();
            Item = new ModelUsuario
            {
                Usuario = new USUARIO()
            };
            BindingContext = this;
            
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            if(Item.Usuario.Contrasena.Equals(Item.Repetir))
            {
                MessagingCenter.Send(this, "AddUsuario", Item.Usuario);
                MessagingCenter.Send(this, "AddUsuarioLogin", Item.Usuario);

                await Navigation.PopModalAsync();
            }
            else
            {
                DisplayAlert("Contraseña no coincide", "Los passwords insertados deben conicidir", "OK");
            }
            
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}