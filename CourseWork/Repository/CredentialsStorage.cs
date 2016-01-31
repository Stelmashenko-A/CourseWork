using System.Collections.Generic;
using System.Linq;
using Raven.Client.Document;
using Repository.Model;

namespace Repository
{
    public class CredentialsStorage
    {
        protected DocumentStore Store;
        protected SafetyProvider SafetyProvider;
        public CredentialsStorage()
        {
            Store = new DocumentStore
            {
                Url = "http://localhost:8081/",
                DefaultDatabase = "Twitty"
            };
            Store.Initialize();
            SafetyProvider = new SafetyProvider();
        }

        public bool Validate(string email, string password)
        {
            using (var session = Store.OpenSession())
            {
                var credentials = session.Query<InternalCredentials>().Where(item => item.Email == email);
                if (!credentials.Any()||credentials.Count()>1)
                {
                    return false;
                } 
                var cred = credentials.First();
                return SafetyProvider.CheckPassword(password, cred.PasswordHash, cred.Salt, cred.Iterations);
            }
        }

        public IList<long> GetClaims(string email)
        {
            using (var session = Store.OpenSession())
            {
                var credentials = session.Query<InternalCredentials>().Where(item => item.Email == email);
                if (!credentials.Any() || credentials.Count() > 1)
                {
                    return null;
                }
                return credentials.First().TwitterIds;
            }

        }

        public void CreateUser(string email, string password)
        {
            using (var session = Store.OpenSession())
            {
                string hash, salt;
                int iterations;
                SafetyProvider.GetHash(password, out hash,out salt, out iterations);
                session.Store(new InternalCredentials(email,hash,salt,iterations));
                session.SaveChanges();
            }
        }

        public void AddAccount(string email, long id)
        {
            using (var session = Store.OpenSession())
            {
                session.Advanced.LuceneQuery<InternalCredentials>().First(item=>item.Email==email).TwitterIds.Add(id);
                session.SaveChanges();
            }
        }
    }
}
