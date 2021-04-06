using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodFight.Domain.Services
{
    public interface IDataService<T>
    {
        Task<IEnumerable<T>> GetAll(string type);
        Task<T> Get(int id, string type);
        Task<T> GetByEmail(string Email, string type);
        Task<IEnumerable<T>> GetConnectedUserById(int id, string type);
        Task<T> Create(T entity, string type);
        Task<T> Update(int id, T entity, string type);
        Task<bool> Delete(int id, string type);
    }
}
