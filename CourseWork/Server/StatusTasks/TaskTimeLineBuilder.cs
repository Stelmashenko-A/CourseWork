using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToTwitter;

namespace Server.StatusTasks
{
    public class TaskTimeLineBuilder : ITaskBuilder
    {
        private readonly TwitterContext _twitterContext;

        public TaskTimeLineBuilder(TwitterContext twitterContext)
        {
            _twitterContext = twitterContext;
        }

        public Task<List<Status>> Build(ulong maxId = 0, int count = 200)
        {
            if (maxId != 0)
            {
                return
                    (from tweet in _twitterContext.Status
                        where tweet.Type == StatusType.User &&
                              tweet.Count == count &&
                              tweet.MaxID == maxId - 1
                        select tweet).ToListAsync();
            }
            return
                (from tweet in _twitterContext.Status
                    where tweet.Type == StatusType.User &&
                          tweet.Count == count
                    select tweet).ToListAsync();
        }
    }
}