using System.Linq;
using LinqToTwitter;

namespace Server.StatusTasks
{
    public class QueryTimeLineBuilder : IQueryBuilder
    {
        private readonly TwitterContext _twitterContext;

        public QueryTimeLineBuilder(TwitterContext twitterContext)
        {
            _twitterContext = twitterContext;
        }

        public IQueryable<Status> BuildTaskByMaxId(ulong maxId = ulong.MaxValue, int count = 200)
        {
            if (maxId != ulong.MaxValue)
            {
                return
                    (from tweet in _twitterContext.Status
                        where tweet.Type == StatusType.Home &&
                              tweet.Count == count &&
                              tweet.MaxID == maxId
                        select tweet);
            }
            return
                (from tweet in _twitterContext.Status
                    where tweet.Type == StatusType.Home &&
                          tweet.Count == count
                    select tweet);
        }

        public IQueryable<Status> BuildTaskByMinId(ulong minId = 0, int count = 200)
        {
            if (minId == 0)
            {
                return
                    (from tweet in _twitterContext.Status
                        where tweet.Type == StatusType.Home &&
                              tweet.Count == count
                        select tweet);
            }
            return
                (from tweet in _twitterContext.Status
                    where tweet.Type == StatusType.Home &&
                          tweet.Count == count &&
                          tweet.SinceID == minId
                    select tweet);
        }

        public IQueryable<Status> BuildTaskByMinIdAndMaxId(ulong minId = 0, ulong maxId = uint.MaxValue, int count = 200)
        {
            if (minId == 0 & maxId == uint.MaxValue)
            {
                return
                    (from tweet in _twitterContext.Status
                        where tweet.Type == StatusType.Home &&
                              tweet.Count == count
                        select tweet);
            }
            if (minId == 0)
            {
                return
                    (from tweet in _twitterContext.Status
                        where tweet.Type == StatusType.Home &&
                              tweet.Count == count &&
                              tweet.MaxID == maxId
                        select tweet);
            }
            if (maxId == 0)
            {
                return
                    (from tweet in _twitterContext.Status
                        where tweet.Type == StatusType.Home &&
                              tweet.Count == count &&
                              tweet.SinceID == minId
                        select tweet);
            }

            return
                (from tweet in _twitterContext.Status
                    where tweet.Type == StatusType.Home &&
                          tweet.Count == count &&
                          tweet.SinceID == minId &&
                          tweet.MaxID == maxId
                    select tweet);
        }
    }
}