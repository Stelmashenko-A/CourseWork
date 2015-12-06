using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;
using Nancy.Authentication.Token;
using Nancy.Json;
using Nancy.Responses;
using Nancy.Security;
using Repository;
using Repository.Model;
using Server;
using Account = Repository.Model.Account;

namespace SelfHostedRestAPI
{
    public class TwitterAuthentificationModule : NancyModule
    {
        private readonly IStorage _storage;

        public TwitterAuthentificationModule(CredentialsStorage credentialsStorage, ITokenizer tokenizer, IStorage storage)
        {
            _storage = storage;
            InitializeTweet(credentialsStorage, tokenizer);
        }

        protected void InitializeTweet(CredentialsStorage credentialsStorage, ITokenizer tokenizer)
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
            Post["/authTwitterAccaunt"] = parameters =>
            {
                this.RequiresClaims(new[] { Request.Headers["Email"].First() });
                string token, tokenSecret, userName;
                ulong id;

                TwitterOauth.GetTokens(Request.Query["oauth_token"], Request.Query["oauth_verifier"], out token,
                    out tokenSecret,
                    out userName, out id);
                var accountRepository = _storage;
                try
                {
                    var acc = accountRepository.GetAccountById(id);
                    var claimsUint = credentialsStorage.GetClaims(Request.Headers["Email"].First()) ?? new List<ulong>();
                    claimsUint.Add(id);
                    string authToken;
                    if (acc == null)
                    {

                        
                        credentialsStorage.AddAccount(Request.Headers["Email"].First(), id);
                        accountRepository.AddAccount(
                            new Account(new TwitterCredentials(new TwitterToken(token, tokenSecret), userName, id)));
                        authToken = tokenizer.Tokenize(
                            new UserIdentity(Request.Headers["Email"].First(), claimsUint.Select(x => x.ToString())),
                            Context);


                        return new JavaScriptSerializer().Serialize(new SetTokenResponse(id, authToken));
                    }
                    accountRepository.ResetTokens(userName, new TwitterToken(token, tokenSecret));
                    var t = claimsUint.Select(x => x.ToString());
                    if (t == null)
                    {
                        t= new List<string>();
                    }
                    authToken = tokenizer.Tokenize(
                        new UserIdentity(Request.Headers["Email"].First(), claimsUint.Select(x => x.ToString())),
                        Context);

                    return new JavaScriptSerializer().Serialize(new SetTokenResponse(id, authToken));
                }
                catch (Exception)
                {

                    accountRepository.AddAccount(
                        new Account(new TwitterCredentials(new TwitterToken(token, tokenSecret), userName, id)));
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
                var accountRepository = _storage;
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

    public class SetTokenResponse
    {
        public SetTokenResponse(ulong currentAccountId, string token)
        {
            CurrentAccountId = currentAccountId;
            Token = token;
        }

        public string Token { get; protected set; }
        public ulong CurrentAccountId { get; protected set; }
    }
}