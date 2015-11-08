using System.Collections.Generic;
using System.Linq;
using LinqToTwitter;
using Repository.Model;

namespace Server
{
    public class Initializer
    {
        public IList<Status> LoadStatuses(TwitterCredentials credentials)
        {
            var contextBuilder = new TwitterContextBuilder();
            var twitterContext = contextBuilder.Build(credentials);
            var statuses = new List<Status>();
            var tweetTask =
                (from tweet in twitterContext.Status
                    where tweet.Type == StatusType.User &&
                          tweet.ScreenName == credentials.ScreenName &&
                          tweet.Count == 200
                    select tweet).ToListAsync();

            tweetTask.Wait();
            statuses.AddRange(tweetTask.Result);

            if (statuses.Count < 200)
            {
                return statuses;
            }

            while (statuses.Count <= 3200)
            {
                tweetTask =
                    (from tweet in twitterContext.Status
                        where tweet.Type == StatusType.User &&
                              tweet.ScreenName == credentials.ScreenName &&
                              tweet.Count == 200 &&
                              tweet.MaxID == statuses.Last().StatusID - 1
                        select tweet).ToListAsync();

                tweetTask.Wait();
                statuses.AddRange(tweetTask.Result);
                if (tweetTask.Result.Count < 200)
                {
                    break;
                }
            }

            return statuses;
        }
    }
}
