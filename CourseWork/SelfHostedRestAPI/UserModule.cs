using Nancy;
using Nancy.Security;
using Repository;

namespace SelfHostedRestAPI
{
    public class UserModule : NancyModule
    {
        protected Storage Storage;
        public UserModule(Storage storage)
        {
            InitializeUser();
            Storage = storage;
        }

        protected void InitializeUser()
        {

            Post["/user/{id}"] = parametes =>
            {
                var claims = (string) parametes.id;
                this.RequiresClaims(new[] { claims });
                return Storage.GetAccountById(parametes.id);
            };
        }
    }
}
