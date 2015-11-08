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
                    OAuthToken = account.Tokens.Token,
                    OAuthTokenSecret = account.Tokens.TokenSecret,
                    ScreenName = account.ScreenName,
                    UserID = account.UserId
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
                          tweet.ScreenName == account.ScreenName &&
                          tweet.SinceID == account.TimeLine.MaxId() &&
                          tweet.Count == count
                    select tweet).ToListAsync();
            tweetTask.Wait();
            return tweetTask.Result;
        }
    }
}