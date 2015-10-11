using System;
using System.Linq;
using Raven.Abstractions.Commands;
using Raven.Client;
using Raven.Client.Document;

namespace Repository
{
    public class AccountRepository:IDisposable
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
            _store.Dispose();
        }

        public IQueryable<Account> GetAll()
        {
            using (var session = _store.OpenSession())
            {
                var result =
                    session.Query<Account>();
                return result;
            }    
        }

        public Account Get(long id)
        {
            using (var session = _store.OpenSession())
            {
                var result =
                    session.Load<Account>(id);
                return result;
            }
        }

        public void Delete(long id)
        {
            using (var session = _store.OpenSession())
            {
                session.Advanced.Defer(new DeleteCommandData { Key = id.ToString() });
            }
        }

        public void Save()
        {
            using (var session = _store.OpenSession())
            {
                session.SaveChanges();
            }
        }

        public void Add(Account item)
        {
            using (var session = _store.OpenSession())
            {
                session.Store(item);
                session.SaveChanges();
            }
        }

       /* public long MaxId
        {
            get
            {
                using (var session = _store.OpenSession())
                {
                    try
                    {
                        return session.
                    }
                    catch (Exception)
                    {

                        return 0;
                    }
                }
            }
        }*/
    }
}
