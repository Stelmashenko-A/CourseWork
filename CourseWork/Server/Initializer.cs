using System.Collections.Generic;
using LinqToTwitter;
using Repository.Model;
using Server.StatusTasks;

namespace Server
{
    public class Initializer
    {
        protected IList<Status> LoadStatuses(ITaskBuilder taskBuilder, int count = 3200)
        {
            var statuses = new List<Status>();
            var tweetTask = taskBuilder.Build();

            tweetTask.Wait();
            statuses.AddRange(tweetTask.Result);

            if (statuses.Count < 200)
            {
                return statuses;
            }

            while (statuses.Count < count)
            {
                tweetTask = taskBuilder.Build(statuses[statuses.Count-1].StatusID);

                tweetTask.Wait();
                statuses.AddRange(tweetTask.Result);
                if (tweetTask.Result.Count < 200)
                {
                    break;
                }
            }

            return statuses;
        }

        public IList<Status> LoadUserStatuses(TwitterCredentials credentials)
        {
            var twitterContextBuilder = new TwitterContextBuilder();
            ITaskBuilder userStatusesBuilder = new TaskUserStatusesBuilder(twitterContextBuilder.Build(credentials),
                credentials.ScreenName);
            return LoadStatuses(userStatusesBuilder);
        }

        public IList<Status> LoadUserTimeLine(TwitterCredentials credentials)
        {
            var twitterContextBuilder = new TwitterContextBuilder();
            ITaskBuilder userStatusesBuilder = new TaskTimeLineBuilder(twitterContextBuilder.Build(credentials));
            return LoadStatuses(userStatusesBuilder, 3000);
        }
    }
}
