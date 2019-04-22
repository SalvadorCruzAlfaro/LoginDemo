using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LoginDemo.DTOs.Partials;

namespace LoginDemo.Services
{
    public interface IDataStore
    {
        Task<bool> AddItemAsync<T>(T item) where T : class, new();
        Task<bool> UpdateItemAsync<T>(T item) where T : class, new();
        Task<bool> DeleteItemAsync<T>(T item) where T : class, new();
        Task<T> GetItemAsync<T>(Guid id) where T : class, new();
        Task<IEnumerable<T>> GetItemsAsync<T>(bool forceRefresh = false) where T : class, new();
        Task<bool> LoginItemAsync(USUARIOVALIDAR item);
    }
}
