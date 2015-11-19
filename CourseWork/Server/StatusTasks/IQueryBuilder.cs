using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToTwitter;

namespace Server.StatusTasks
{
    public interface IQueryBuilder
    {
        IQueryable<Status> BuildTaskByMaxId(ulong maxId = ulong.MaxValue, int count = 200);
        IQueryable<Status> BuildTaskByMinId(ulong minId = 0, int count = 200);
        IQueryable<Status> BuildTaskByMinIdAndMaxId(ulong minId = 0, ulong maxId = uint.MaxValue, int count = 200);
    }   
}