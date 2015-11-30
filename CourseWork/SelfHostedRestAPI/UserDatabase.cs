using System.Collections.Generic;
using System.Linq;
using Nancy.Security;
using Repository;

namespace SelfHostedRestAPI
{
    public class UserDatabase
    {
        public static IUserIdentity ValidateUser(CredentialsStorage storage, string userName, string password)
        {
            return !storage.Validate(userName, password)
                ? null
                : new UserIdentity(userName, storage.GetClaims(userName).Select(item => item.ToString()));
        }

        public static IUserIdentity CreateUser(CredentialsStorage storage, string userName, string password)
        {
            storage.CreateUser(userName,password);
            return new UserIdentity(userName, new List<string>());
        }
    }
}