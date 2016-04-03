

using System.Linq;
using LinqToTwitter;

namespace Server.StatusTasks
{
    public class TaskBuilder
    {
        private readonly TwitterContext _twitterContext;

        public TaskBuilder(TwitterContext twitterContext)
        {
            _twitterContext = twitterContext;
        }

        public IQueryable<Status> BuildRetweetTask(ulong tweetId)
        {
            var publicTweets =

                (from tweet in _twitterContext.Status
                    where tweet.Type == StatusType.Retweets &&
                          tweet.ID == 716202018751266817
                 select tweet)
                    .ToList();
            return
                (from tweet in _twitterContext.Status
                    where tweet.Type == StatusType.Retweets &&
                          tweet.ID == tweetId
                    select tweet);
        }
    }
}
