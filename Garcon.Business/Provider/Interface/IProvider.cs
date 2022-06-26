using System.Collections.Generic;

namespace Garcon.Business.Provider.Interface
{
    public interface IProvider<T> where T : class
    {
        List<T> GetEntityFromSource();
        void SaveChanges(List<T> entity);
    }
}
