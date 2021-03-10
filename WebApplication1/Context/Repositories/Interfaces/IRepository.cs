using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Context.Repositories.Interfaces
{
    public interface IRepository<T> where T:class
    {
        Task InsertOneAsync(T obj);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task RemoveAsync(string id);
    }
}
