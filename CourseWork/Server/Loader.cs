using System.Collections.Generic;
using System.Linq;
using LinqToTwitter;
using Repository.Model;
using Account = Repository.Model.Account;

namespace Server
{
    public class Loader
    {
        public async void Load(IDictionary<ulong, Account> list, string consumerKey, string consumerSecret)
        {
            foreach (var info in list.Values)
            {
                var singleUserAuthorizer = new SingleUserAuthorizer
                {
                    CredentialStore = new InMemoryCredentialStore
                    {
                        ConsumerKey = consumerKey,
                        ConsumerSecret = consumerSecret,
                        OAuthToken = info.Tokens.Token,
                        OAuthTokenSecret = info.Tokens.TokenSecret,
                        ScreenName = info.ScreenName,
                        UserID = info.UserId
                    }
                };

                var twitterCtx = new TwitterContext(singleUserAuthorizer);
                
                var tweets =
                    await
                        (from tweet in twitterCtx.Status
                            where tweet.Type == StatusType.User &&
                                  tweet.ScreenName == info.ScreenName &&
                                  tweet.Count == 200
                            select tweet)
                            .ToListAsync();
            }

        }
    }
}
