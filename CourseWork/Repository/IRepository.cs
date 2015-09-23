using System;
using System.Linq;

namespace Repository
{
    public interface IRepository<T> : IDisposable
    {
        IQueryable<T> GetAll(int userId);
        T Get(int userId, int id);
        void Delete(int userId, int id);
        void Save(int userId);
        void Add(int userId, T obj);
    }
}
