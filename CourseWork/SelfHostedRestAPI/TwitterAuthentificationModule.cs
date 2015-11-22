using System;
using Nancy;
using Nancy.Json;
using Nancy.Responses;
using Repository;
using Repository.Model;
using Server;
using Account = Repository.Model.Account;

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
            Post["/twitter/authentification/authorizationUri"] = parameters =>
            {
                return TwitterOauth.GetAuthorizationUri();
            };
            Get["/twitter/authentification/authorizationUri"] = parameters =>
            {
                return TwitterOauth.GetAuthorizationUri();
            };
            Get["/twitter/authentification/pin"] = parameters =>
            {
                return new JavaScriptSerializer().Serialize("pin");
            };
            Post["/auth"] = parameters =>
            {
                string token, tokenSecret, userName;
                ulong id;

                TwitterOauth.GetTokens(Request.Query["oauth_token"], Request.Query["oauth_verifier"], out token,
                    out tokenSecret,
                    out userName, out id);
                var accountRepository = new Storage();
                try
                {
                    var acc = accountRepository.GetAccountById(id);
                    if (acc == null)
                    {
                        accountRepository.AddAccount(
                            new Account(new TwitterCredentials(new TwitterToken(token, tokenSecret), userName, id)));
                        return new RedirectResponse("https://mail.ru", RedirectResponse.RedirectType.Temporary);
                    }
                    accountRepository.ResetTokens(userName, new TwitterToken(token, tokenSecret));
                    return new RedirectResponse("https://mail.ru", RedirectResponse.RedirectType.Temporary);
                }
                catch (Exception)
                {
                    
                    accountRepository.AddAccount(new Account(new TwitterCredentials(new TwitterToken(token, tokenSecret), userName, id)));
                    return Response.AsRedirect("https://mail.ru");
                }
                
                
                    
            };
            Get["/auth"] = parameters =>
            {
                string token, tokenSecret, userName;
                ulong id;

                TwitterOauth.GetTokens(Request.Query["oauth_token"], Request.Query["oauth_verifier"], out token,
                    out tokenSecret,
                    out userName, out id);
                var accountRepository = new Storage();
                try
                {
                    var acc = accountRepository.GetAccountById(id);
                    if (acc == null)
                    {
                        accountRepository.AddAccount(
                            new Account(new TwitterCredentials(new TwitterToken(token, tokenSecret), userName, id)));
                        return new RedirectResponse("https://mail.ru", RedirectResponse.RedirectType.Temporary);
                    }
                    accountRepository.ResetTokens(userName, new TwitterToken(token, tokenSecret));
                    return new RedirectResponse("https://mail.ru", RedirectResponse.RedirectType.Temporary);
                }
                catch (Exception)
                {

                    accountRepository.AddAccount(new Account(new TwitterCredentials(new TwitterToken(token, tokenSecret), userName, id)));
                    return Response.AsRedirect("https://mail.ru");
                }



            };
        }
    }

}