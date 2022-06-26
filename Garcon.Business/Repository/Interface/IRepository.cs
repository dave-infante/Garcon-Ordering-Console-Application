using System.Collections.Generic;
using System;

namespace Garcon.Business.Repository.Interface
{
    public interface IRepository<T> where T : class
    {
        int GetEntityCount();
        void AddEntity(T entity);
        void UpdateEntity(T entity);
        ICollection<T> GetEntityList();
        void SaveEntity();
    }
}
