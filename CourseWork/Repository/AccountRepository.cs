using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using Raven.Client.Document;
using Repository.Model;

namespace Repository
{
    public class AccountRepository : IRepository<Account>
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

        public IDictionary<ulong, Account> GetAll()
        {
            using (var session = _store.OpenSession())
            {
                return session.Query<Storage>().First().Data;
            }
        }

        public Account Get(ulong userId)
        {
            using (var session = _store.OpenSession())
            {
                return session.Query<Storage>().First().Data[userId];
            }
        }

        public TwitterCredentials GetTwitterCredentials(ulong userId)
        {
            using (var session = _store.OpenSession())
            {
                return session.Query<Storage>().First().Data[userId].TwitterCredentials;
            }
        }

        public IList<TwitterCredentials> GetAllTwitterCredentialses()
        {
            using (var session = _store.OpenSession())
            {
                return
                    session.Query<Storage>()
                        .First()
                        .Data.Values.Select(variable => variable.TwitterCredentials)
                        .ToList();
            }
        }
        public void Delete(ulong userId)
        {
            using (var session = _store.OpenSession())
            {
                session.Query<Storage>().First().Data.Remove(userId);
                session.SaveChanges();
            }
        }

        public void Save(ulong userId)
        {
            using (var session = _store.OpenSession())
            {
                session.SaveChanges();
            }
        }

        public void Add(ulong userId, Account obj)
        {
            using (var session = _store.OpenSession())
            {
                var s = session.Query<Storage>().First();
                s.Data.Add(userId,obj);
                session.SaveChanges();
            }
        }

        
    }

    
}
