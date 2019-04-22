using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LoginDemo.DTOs;
using LoginDemo.DTOs.Partials;
using LoginDemo.Services;
using LoginDemo.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LoginDemo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public IDataStore DataStore => DependencyService.Get<IDataStore>();

        private USUARIO item;

        public USUARIO Item
        {
            get { return item; }
            set { SetProperty(ref item, value); }
        }


        private string message;
        public string Message
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }

        

        public string Usuario { get; set; }
        public string Contrasena { get; set; }

        public bool Habilitar { get; set; }

        public LoginPage()
        {
            InitializeComponent();
            Item = new USUARIO();
            Message = "";
            Habilitar = true;
            MessagingCenter.Subscribe<LoginViewModel, bool>(this, "Logueado", async (obj, item) =>
            {
                if (item)
                {
                    DisplayAlert("Login Correcto", "Usuario logueado correctamente. Puede acceder al listado", "OK");
                    Habilitar = false;
                    Shell.CurrentShell.GoToAsync($"app:///LoginDemo/Tabs/Listado/Usuarios",true);
                    Shell.CurrentShell.FlyoutIsPresented = false;
                }
                else
                {
                    DisplayAlert("Login InCorrecto", "Usuario y/o contraseña no validos", "OK");
                }

                Usuario = "";
                Contrasena = "";

            });

            MessagingCenter.Subscribe<UsuarioAddPage, USUARIO>(this, "AddUsuarioLogin", async (obj, item) =>
            {
                var newItem = item as USUARIO;
                //Items.Add(newItem);
                newItem.Id = new Guid();
                newItem.FechaCreacion = DateTime.Now;
                bool respuesta = await DataStore.AddItemAsync(newItem);
                if (respuesta)
                {
                    DisplayAlert("Alta Correcta", "Usuario registrado correctamente. Puede acceder usando sus datos registrados", "OK");
                    BindingContext = this;
                }
                    
            });

            BindingContext = this;
        }

        private void Login_OnClicked(object sender, EventArgs e)
        {
            Item = new USUARIO
            {
                Usuario = Usuario,
                Contrasena = Contrasena
            };
            MessagingCenter.Send(this, "LoginUsuario", Item);
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new UsuarioAddPage()));
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}