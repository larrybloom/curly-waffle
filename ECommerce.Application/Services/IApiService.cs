using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Services
{
    public interface IApiService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(string endpoint);
        Task<T> GetByIdAsync(string endpoint, int id);
        Task<T> CreateAsync(string endpoint, T entity);
        Task<T> UpdateAsync(string endpoint, int id, T entity);
        Task<bool> DeleteAsync(string endpoint, int id);
    }
}
