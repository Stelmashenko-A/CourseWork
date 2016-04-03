using System.Collections.Generic;
using System.Linq;
using LinqToTwitter;
using Server.StatusTasks;

namespace Server
{
    public class ReplyLoader
    {
        public List<Status> LoadReplies(TaskBuilder taskBuilder, ulong id)
        {
            var task = taskBuilder.BuildRetweetTask(id);
            return task.ToList();
        }
    }
}
