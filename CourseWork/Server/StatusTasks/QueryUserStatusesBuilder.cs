using System.Linq;
using LinqToTwitter;

namespace Server.StatusTasks
{
    public class QueryUserStatusesBuilder : IQueryBuilder
    {
        private readonly TwitterContext _twitterContext;
        private readonly string _screenName;

        public QueryUserStatusesBuilder(TwitterContext twitterContext, string screenName)
        {
            _twitterContext = twitterContext;
            _screenName = screenName;
        }

        public IQueryable<Status> BuildTaskByMaxId(ulong maxId = ulong.MaxValue, int count = 200)
        {
            if (maxId != uint.MaxValue)
            {
                return
                    from tweet in _twitterContext.Status
                    where tweet.Type == StatusType.User &&
                          tweet.Count == count &&
                          tweet.MaxID == maxId &&
                          tweet.ScreenName == _screenName
                    select tweet;
            }
            return
                from tweet in _twitterContext.Status
                where tweet.Type == StatusType.User &&
                      tweet.Count == count &&
                      tweet.ScreenName == _screenName
                select tweet;
        }

        public IQueryable<Status> BuildTaskByMinId(ulong minId = 0, int count = 200)
        {
            if (minId == 0)
            {
                return
                    from tweet in _twitterContext.Status
                    where tweet.Type == StatusType.User &&
                          tweet.Count == count &&
                          tweet.ScreenName == _screenName
                    select tweet;
            }
            return
                from tweet in _twitterContext.Status
                where tweet.Type == StatusType.User &&
                      tweet.Count == count &&
                      tweet.SinceID == minId &&
                      tweet.ScreenName == _screenName
                select tweet;
        }

        public IQueryable<Status> BuildTaskByMinIdAndMaxId(ulong minId = 0, ulong maxId = uint.MaxValue, int count = 200)
        {
            if (minId == 0 & maxId == uint.MaxValue)
            {
                return
                    from tweet in _twitterContext.Status
                    where tweet.Type == StatusType.User &&
                          tweet.Count == count &&
                          tweet.ScreenName == _screenName
                    select tweet;
            }
            if (minId == 0)
            {
                return
                    from tweet in _twitterContext.Status
                    where tweet.Type == StatusType.User &&
                          tweet.Count == count &&
                          tweet.MaxID == maxId &&
                          tweet.ScreenName == _screenName
                    select tweet;
            }
            if (maxId == 0)
            {
                return
                    from tweet in _twitterContext.Status
                    where tweet.Type == StatusType.User &&
                          tweet.Count == count &&
                          tweet.SinceID == minId && 
                          tweet.ScreenName == _screenName
                    select tweet;
            }

            return
                from tweet in _twitterContext.Status
                where tweet.Type == StatusType.User &&
                      tweet.Count == count &&
                      tweet.SinceID == minId &&
                      tweet.MaxID == maxId &&
                      tweet.ScreenName == _screenName
                select tweet;
        }
    }
}