using System.Collections.Generic;
using System.Linq;
using LinqToTwitter;
using Account = Repository.Model.Account;


namespace Server
{
    public class Loader
    {
        public List<Status> Load(Account account, int count = 20)
        {
            var contextBuilder = new TwitterContextBuilder();
            var twitterCtx = contextBuilder.Build(account.TwitterCredentials);
            var tweetTask =
                (from tweet in twitterCtx.Status
                    where tweet.Type == StatusType.User &&
                          tweet.ScreenName == account.TwitterCredentials.ScreenName &&
                          tweet.SinceID == account.MaxId &&
                          tweet.Count == count
                    select tweet).ToListAsync();
            tweetTask.Wait();
            return tweetTask.Result;
        }
    }
}