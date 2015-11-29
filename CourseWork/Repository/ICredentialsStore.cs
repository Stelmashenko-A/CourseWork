using System.Collections.Generic;
using System.Linq;
using Raven.Client.Document;
using Repository.Model;

namespace Repository
{
    public class CredentialsStorege
    {
        protected DocumentStore Store;
        protected SafetyProvider SafetyProvider;
        public CredentialsStorege()
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

        public IList<ulong> GetClaims(string email)
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
    }
}
