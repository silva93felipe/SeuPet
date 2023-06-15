using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IBaseService<T>
    {
        void Create(T entity);
        void Update(T entity, int id);
        Task<bool> Delete(int id);
        IEnumerable<T> GetAll();
        Task<T> GetById(int id);
    }
}