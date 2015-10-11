using Nancy;
using Nancy.Authentication.Token;
using Nancy.Security;

namespace RestAPI
{
    /*public class AuthModule : NancyModule
    {
        public AuthModule(ITokenizer tokenizer)
            : base("/auth")
        {
            Post["/"] = x =>
            {
                var userName = (string)Request.Form.UserName;
                var password = (string)Request.Form.Password;

                IUserIdentity userIdentity = null;// = UserDatabase.ValidateUser(userName, password);

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
    }*/
}