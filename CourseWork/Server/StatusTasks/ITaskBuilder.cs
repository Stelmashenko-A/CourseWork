using System.Collections.Generic;
using System.Threading.Tasks;
using LinqToTwitter;

namespace Server.StatusTasks
{
    public interface ITaskBuilder
    {
        Task<List<Status>> Build(ulong maxId = 0, int count = 200);
    }
}