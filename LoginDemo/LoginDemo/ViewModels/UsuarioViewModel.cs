using LoginDemo.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Text;
using System.Diagnostics;
using LoginDemo.Views;

namespace LoginDemo.ViewModels
{
    public class UsuarioViewModel : BaseViewModel
    {
        public ObservableCollection<USUARIO> Elementos { get; set; }
        public Command ListarCommand { get; set; }
        public UsuarioViewModel()
        {
            Title = "Listado de usuarios";
            Elementos = new ObservableCollection<USUARIO>();
            ListarCommand=new Command(async()=>
            {
                if (IsBusy)
                    return;

                IsBusy = true;

                try
                {
                    Elementos.Clear();
                    var items = await DataStore.GetItemsAsync<USUARIO>(true);
                    foreach (var item in items)
                    {
                        Elementos.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    IsBusy = false;
                }
            });

            MessagingCenter.Subscribe<UsuarioAddPage, USUARIO>(this, "AddUsuario", async (obj, item) =>
            {
                var newItem = item as USUARIO;
                newItem.Id = new Guid();
                newItem.FechaCreacion = DateTime.Now;
                bool respuesta = await DataStore.AddItemAsync(newItem);
                if (respuesta)
                    ListarCommand.Execute(null);
            });

            MessagingCenter.Subscribe<UsuarioDetailPage, USUARIO>(this, "UpdateUsuario", async (obj, item) =>
            {
                var newItem = item as USUARIO;
                bool respuesta = await DataStore.UpdateItemAsync(newItem);
                if (respuesta)
                    ListarCommand.Execute(null);
            });

            MessagingCenter.Subscribe<UsuarioDetailPage, USUARIO>(this, "DeleteUsuario", async (obj, item) =>
            {
                var newItem = item as USUARIO;
                bool respuesta = await DataStore.DeleteItemAsync(newItem);
                if (respuesta)
                    ListarCommand.Execute(null);
            });
        }
    }
}
