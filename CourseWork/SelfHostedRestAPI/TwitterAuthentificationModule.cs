using Nancy;
using Nancy.Json;
using Server;

namespace SelfHostedRestAPI
{
    public class TwitterAuthentificationModule : NancyModule
    {
        public TwitterAuthentificationModule()
        {
            InitializeTweet();
        }

        protected void InitializeTweet()
        {
            Get["/twitter/authentification/authorizationUri"] = parameters =>
            {
                return TwitterOauth.GetAuthorizationUri();
            };

            Get["/twitter/authentification/pin"] = parameters =>
            {
                return new JavaScriptSerializer().Serialize("pin");
            };
            Get["/auth"] = parameters =>
            {
                var c = Request.Query;
                string str1, str2;
                long id;
                TwitterOauth.GetTokens(Request.Query["oauth_token"], Request.Query["oauth_verifier"], out str1, out str2, out id);
                return new JavaScriptSerializer().Serialize("pin");
            };
        }
    }
}
