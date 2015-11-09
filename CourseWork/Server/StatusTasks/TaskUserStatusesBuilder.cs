using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToTwitter;

namespace Server.StatusTasks
{
    public class TaskUserStatusesBuilder : ITaskBuilder
    {
        private readonly TwitterContext _twitterContext;
        private readonly string _screenName;

        public TaskUserStatusesBuilder(TwitterContext twitterContext, string screenName)
        {
            _twitterContext = twitterContext;
            _screenName = screenName;
        }

        public Task<List<Status>> Build(ulong maxId = 0, int count = 200)
        {
            if (maxId != 0)
            {
                return
                    (from tweet in _twitterContext.Status
                        where tweet.Type == StatusType.User &&
                              tweet.Count == count &&
                              tweet.ScreenName == _screenName &&
                              tweet.MaxID == maxId - 1
                        select tweet).ToListAsync();
            }
            return
                (from tweet in _twitterContext.Status
                    where tweet.Type == StatusType.User &&
                          tweet.ScreenName == _screenName &&
                          tweet.Count == count
                    select tweet).ToListAsync();
        }
    }
}