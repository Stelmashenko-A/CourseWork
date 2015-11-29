using System.Linq;
using Nancy.Security;
using Repository;

namespace SelfHostedRestAPI
{
    public class UserDatabase
    {
        public static IUserIdentity ValidateUser(CredentialsStorege storage, string userName, string password)
        {
            return !storage.Validate(userName, password)
                ? null
                : new UserIdentity(userName, storage.GetClaims(userName).Select(item => item.ToString()));
        }
    }
}