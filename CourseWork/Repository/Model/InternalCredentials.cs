using System.Collections.Generic;

namespace Repository.Model
{
    internal class InternalCredentials
    {
        public string Email { get; protected set; }

        public string PasswordHash { get; protected set; }

        public string Salt { get; protected set; }

        public int Iterations { get; protected set; }

        public IList<ulong> TwitterIds { get; protected set; }

        public InternalCredentials(string email, string passwordHash, string salt, int iterations)
        {
            Email = email;
            PasswordHash = passwordHash;
            Salt = salt;
            TwitterIds = new List<ulong>();
            Iterations = iterations;
        }

        

        public void AddId(ulong id)
        {
            //todo add exception
            TwitterIds.Add(id);
        }

        public void RemoveId(ulong id)
        {
            TwitterIds.Remove(id);
        }
    }
}
