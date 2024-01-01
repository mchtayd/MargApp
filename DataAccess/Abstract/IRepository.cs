using Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IRepository<T> where T : IEntity
    {
        List<T> GetList();
        T Get(string id);
        string Add(T entity);
        string Update(T entity, string oldName);
        string Delete(string sicilNo);
    }
}
