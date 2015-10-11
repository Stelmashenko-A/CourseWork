using System;
using System.Linq;

namespace Repository
{
    public interface IRepository<T> : IDisposable
    {
        IQueryable<T> GetAll(long userId);
        T Get(long userId, long id);
        void Delete(long userId, long id);
        void Save(long userId);
        void Add(long userId, T obj);
    }
}
