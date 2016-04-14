using System.Collections.Generic;
using System.Linq;
using Repository;
using Repository.Model;
using Server.StatusTasks;

namespace Server
{
    public class ReplyLoader
    {
        protected readonly StatusMapper StatusMapper = new StatusMapper();
        protected readonly IStorage Storage = new Storage();

        protected IEnumerable<TwitterStatus> LoadReplies(TaskBuilder taskBuilder, string userName, ulong statusId)
        {
            var task = taskBuilder.BuildRetweetTask(userName,statusId);
            var r = task.ToList();
            return StatusMapper.Map(r.Where(item=>item.InReplyToStatusID == statusId));
        }

        public IEnumerable<TwitterStatus> LoadReplies(long userId, ulong tweetId)
        {
            var account = Storage.GetAccountById(userId);
            var statusAuthor = Storage.GetTweeById(tweetId).UserName;
            var twitterContextBuilder = new TwitterContextBuilder();
            var context = twitterContextBuilder.Build(
                new TwitterCredentials(
                    new TwitterToken(account.TwitterCredentials.Tokens.Token,
                        account.TwitterCredentials.Tokens.TokenSecret), account.TwitterCredentials.ScreenName,
                    account.TwitterCredentials.UserId));
            return LoadReplies(new TaskBuilder(context), statusAuthor, tweetId).ToList();
        }
    }
}