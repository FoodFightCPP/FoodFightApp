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
        //Task<T> Create(T entity);
        //Task<T> Update(Guid id, T entity);
        //Task<bool> Delete(Guid id);
    }
}
