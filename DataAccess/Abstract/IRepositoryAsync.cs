using Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    internal interface IRepositoryAsync<T> where T : IEntity
    {
        Task<List<T>> GetListAsync();
        Task<T> GetAsync(string id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity, string oldName);
        Task<bool> DeleteAsync(string sicilNo);
    }
}
