using System.Collections.Generic;
using Nancy;
using Nancy.Authentication.Token;
using Nancy.Responses;
using Nancy.Security;
using Repository;

namespace SelfHostedRestAPI
{

    public class AuthModule : NancyModule
    {
        public AuthModule(ITokenizer tokenizer, IStorage storage)
            : base("/auth2")
        {
            Post["/"] = x =>
            {
                var userName = (string)Request.Form.UserName;
                var password = (string)Request.Form.Password;

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

    public class UserDatabase
    {
        public static IUserIdentity ValidateUser(IStorage storage, string userName, string password)
        {
            return new UserIdentity();
        }
    }
    public class UserIdentity : IUserIdentity
    {
        public string UserName { get; }
        public IEnumerable<string> Claims { get; }

        public UserIdentity()
        {
            UserName = "user";
            var s = new List<string>();
            s.Add("admin");
            Claims = s;

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