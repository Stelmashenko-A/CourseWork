using Nancy;
using Nancy.Json;
using Server;

namespace RestAPI
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
        }
    }
}