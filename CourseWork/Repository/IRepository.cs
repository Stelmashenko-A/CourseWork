using System;
using System.Collections.Generic;
using Repository.Model;

namespace Repository
{
    public interface IRepository<T> : IDisposable
    {
        IDictionary<Id, AccountInfo> GetAll();
        T Get(Id userId);
        void Delete(Id userId);
        void Save(Id userId);
        void Add(Id userId, T obj);
    }
}
