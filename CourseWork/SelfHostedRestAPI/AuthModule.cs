using System.Linq;
using Nancy;
using Nancy.Authentication.Token;
using Nancy.Responses;
using Nancy.Security;
using Repository;

namespace SelfHostedRestAPI
{
    public class AuthModule : NancyModule
    {
        public AuthModule(ITokenizer tokenizer, CredentialsStorage storage)
            : base("/auth")
        {
            Post["/"] = x =>
            {
                var userName = Request.Headers["UserName"].First();
                var password = Request.Headers["Password"].First();
               // var userName = (string)Request.Form.UserName;
                //var password = (string)Request.Form.Password;

                var userIdentity = UserDatabase.ValidateUser(storage, userName, password);

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

            Get["/validation"] = _ =>
            {
                this.RequiresAuthentication();
                return "Yay! You are authenticated!";
            };

            Get["/admin"] = _ =>
            {
                
                this.RequiresClaims(new[] { "admin" });
                return "Yay! You are authorized!";
            };
        }
    }

    public class SecureModule : NancyModule
    {
        public SecureModule()
        {
            Before += ctx => {
                return (Context.CurrentUser == null) ? new HtmlResponse(HttpStatusCode.Unauthorized) : null;
            };
            Get["/validation"] = _ =>
            {
                this.RequiresAuthentication();
                return "Yay! You are authenticated!";
            };
        }
    }
}