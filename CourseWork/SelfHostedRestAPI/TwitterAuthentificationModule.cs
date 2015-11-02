using Nancy;
using Nancy.Json;
using Repository;
using Repository.Model;
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
                string token, tokenSecret, userName;
                ulong id;

                TwitterOauth.GetTokens(Request.Query["oauth_token"], Request.Query["oauth_verifier"], out token,
                    out tokenSecret,
                    out userName, out id);
                var accountRepository = new AccountRepository();

                accountRepository.Add(id, new AccountInfo(new TwitterToken(token, tokenSecret), userName, id));
                return new JavaScriptSerializer().Serialize("pin");
            };
        }
    }
}