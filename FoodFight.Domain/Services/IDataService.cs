using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodFight.Domain.Services
{
    public interface IDataService<T>
    {
        Task<IEnumerable<T>> GetAll(string type);
        Task<T> Get(Guid id, string type);
        Task<T> GetByEmail(string Email, string type);
        Task<T> Create(T entity, string type);
        Task<T> Update(Guid id, T entity, string type);
        Task<bool> Delete(Guid id, string type);
    }
}
