using System;
using System.Collections.Generic;
using Repository.Model;

namespace Repository
{
    public interface IRepository<T> : IDisposable
    {
        IDictionary<ulong, Account> GetAll();
        T Get(ulong userId);
        void Delete(ulong userId);
        void Save(ulong userId);
        void Add(ulong userId, T obj);
    }
}
