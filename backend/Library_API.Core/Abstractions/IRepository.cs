using Library_API.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_API.Core.Abstractions
{
     public interface IRepository<T> where T : class, IEntity  {
        Task<T> GetById(Guid id);
        Task<List<T>> GetAll();
        Task<Guid> Create(T entity);
        Task Update(T entity);
        Task Delete(Guid id);
        Task<bool> IsExist(Guid id);
      }
}
