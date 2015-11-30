using System.Linq;
using Nancy;
using Nancy.Authentication.Token;
using Repository;

namespace SelfHostedRestAPI
{
    public class RegistrationModule : NancyModule
    {
        public RegistrationModule(ITokenizer tokenizer, CredentialsStorage storage)
            : base("/registration")
        {
            Post["/"] = x =>
            {
                var userName = Request.Headers["UserName"].First();
                var password = Request.Headers["Password"].First();

                var userIdentity = UserDatabase.CreateUser(storage, userName, password);

                if (userIdentity == null)
                {
                    return HttpStatusCode.Unauthorized;
                }

                var token = tokenizer.Tokenize(userIdentity, Context);

                return new
                {
                    Token = token,
                };
            };
        }
    }
}