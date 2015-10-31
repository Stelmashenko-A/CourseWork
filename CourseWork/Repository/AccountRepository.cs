using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using Raven.Client.Document;
using Repository.Model;

namespace Repository
{
    public class AccountRepository : IRepository<AccountInfo>
    {
        private readonly IDocumentStore _store;

        public AccountRepository()
        {
            _store = new DocumentStore
            {
                Url = "http://localhost:8081/",
                DefaultDatabase = "Twitty"
            };
            _store.Initialize();
        }

        public void Dispose()
        {
            //todo
        }

        public IDictionary<Id, AccountInfo> GetAll()
        {
            using (var session = _store.OpenSession())
            {

                return session.Query<AccountInfoStorage>().First().Data;

            }
        }

        public AccountInfo Get(Id userId)
        {
            using (var session = _store.OpenSession())
            {
                return session.Query<AccountInfoStorage>().First().Data[userId];
            }
        }

        public void Delete(Id userId)
        {
            using (var session = _store.OpenSession())
            {
                session.Query<AccountInfoStorage>().First().Data.Remove(userId);
                session.SaveChanges();
            }
        }

        public void Save(Id userId)
        {
            using (var session = _store.OpenSession())
            {
                session.SaveChanges();
            }
        }

        public void Add(Id userId, AccountInfo obj)
        {
            using (var session = _store.OpenSession())
            {
                if (session.Query<AccountInfoStorage>().First().Data.ContainsKey(userId))
                {
                    throw new Exception(" add ");
                }
                session.Query<AccountInfoStorage>().First().Data.Add(userId, obj);
                session.SaveChanges();
            }
        }
    }

    
}
