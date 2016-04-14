using System.Collections.Generic;
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

        public List<Status> BuildRetweetTask(string userName, ulong statusId)
        {
            var singleOrDefault = (from search in _twitterContext.Search
                where search.Type == SearchType.Search &&
                      search.Query == "\"to:" + userName + "\"" &&
                      search.Count == 100 &&
                      search.SinceID == statusId
                                   select search).SingleOrDefault();
            if (singleOrDefault != null)
            {
                return singleOrDefault.Statuses;
            }
            return new List<Status>();
        }
    }
}
