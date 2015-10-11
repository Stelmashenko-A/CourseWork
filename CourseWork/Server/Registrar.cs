using System;
using Repository;

namespace Server
{
    public class Registrar
    {
        public bool TryRegistrate(string userName, string password)
        {
            using (var repositiry = new AccountRepository())
            {
                try
                {
                    repositiry.Add(new Account(userName, password));
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
