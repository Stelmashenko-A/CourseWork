using System.Collections;
using System.Collections.Generic;
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
                var c = Request.Query;
                string str1, str2, userName;
                ulong id;
                TwitterOauth.GetTokens(Request.Query["oauth_token"], Request.Query["oauth_verifier"], out str1, out str2,
                    out userName, out id);
                var accountRepository = new AccountRepository();

               // accountRepository.Add(id, new AccountInfo(new TwitterToken(str1, str2), userName, id));
                Loader loader = new Loader();
                loader.Load(accountRepository.GetAll(), "1WMZ0jYYuv8ZHrYI1L6hWN4m1",
                "XYXajdaRgzMi11pIm5FM4WHc4xRzJPpPIwSMRMbACOEkOHEMDL");
                return new JavaScriptSerializer().Serialize("pin");
            };
        }
    }
}
