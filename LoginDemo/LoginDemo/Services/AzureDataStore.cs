using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LoginDemo.DTOs.Partials;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace LoginDemo.Services
{
    public class AzureDataStore : IDataStore
    {
        HttpClient client;


        public AzureDataStore()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri($"{App.AzureBackendUrl}/");
        }

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        public async Task<IEnumerable<Tipo>> GetItemsAsync<Tipo>(bool forceRefresh = false) where Tipo : class, new()
        {
            IEnumerable<Tipo> items = null;
            if (forceRefresh && IsConnected)
            {
                var json = await client.GetStringAsync($"api/usuario/ListaElementos");
                items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Tipo>>(json));
            }

            return items;
        }

        public async Task<Tipo> GetItemAsync<Tipo>(Guid id) where Tipo : class, new()
        {
            if (id != null && IsConnected)
            {
                var json = await client.GetStringAsync($"api/usuario/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<Tipo>(json));
            }

            return null;
        }

        public async Task<bool> AddItemAsync<Tipo>(Tipo item) where Tipo : class, new()
        {
            if (item == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(item);

            var response = await client.PostAsync($"api/usuario/InsertarElemento", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateItemAsync<Tipo>(Tipo item) where Tipo : class, new()
        {
            try
            {
                if (item == null || !IsConnected)
                    return false;

                var serializedItem = JsonConvert.SerializeObject(item);

                var response = await client.PutAsync("api/usuario/ActualizarElemento", new StringContent(serializedItem, Encoding.UTF8, "application/json")).ConfigureAwait(false);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteItemAsync<Tipo>(Tipo item) where Tipo : class, new()
        {
            try
            {
                if (item == null || !IsConnected)
                    return false;

                var serializedItem = JsonConvert.SerializeObject(item);

                var response = await client.PutAsync("api/usuario/EliminarElemento", new StringContent(serializedItem, Encoding.UTF8, "application/json")).ConfigureAwait(false);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> LoginItemAsync(USUARIOVALIDAR item)
        {
            try
            {
                if (item == null || !IsConnected)
                    return false;

                var serializedItem = JsonConvert.SerializeObject(item);

                var response = await client.PutAsync("api/usuario/ValidarLogin", new StringContent(serializedItem, Encoding.UTF8, "application/json")).ConfigureAwait(false);

                return response.IsSuccessStatusCode;
            }
            catch (Exception exception)
            {
                return false;
            }
        }
    }
}