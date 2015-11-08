using System.Collections.Generic;
using System.Linq;
using LinqToTwitter;
using Account = Repository.Model.Account;


namespace Server
{
    public class Loader
    {
        protected TwitterContext BuildTwitterContext(Account account)
        {
            var singleUserAuthorizer = new SingleUserAuthorizer
            {
                CredentialStore = new InMemoryCredentialStore
                {
                    ConsumerKey = ConsumerToken.ConsumerKey,
                    ConsumerSecret = ConsumerToken.ConsumerSecret,
                    OAuthToken = account.TwitterCredentials.Tokens.Token,
                    OAuthTokenSecret = account.TwitterCredentials.Tokens.TokenSecret,
                    ScreenName = account.TwitterCredentials.ScreenName,
                    UserID = account.TwitterCredentials.UserId
                }
            };

            return new TwitterContext(singleUserAuthorizer);
        }

        public List<Status> Load(Account account, int count = 20)
        {
            var twitterCtx = BuildTwitterContext(account);
            var tweetTask =
                (from tweet in twitterCtx.Status
                    where tweet.Type == StatusType.User &&
                          tweet.ScreenName == account.TwitterCredentials.ScreenName &&
                          tweet.SinceID == account.TimeLine.MaxId() &&
                          tweet.Count == count
                    select tweet).ToListAsync();
            tweetTask.Wait();
            return tweetTask.Result;
        }
    }
}