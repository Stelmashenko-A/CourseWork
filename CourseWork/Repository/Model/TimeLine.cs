using System.Collections.Generic;
using System.Linq;
using LinqToTwitter;

namespace Repository.Model
{
    public class TimeLine
    {
        protected SortedSet<Status> Statuses = new SortedSet<Status>(new StatusByIdComparer());

        public void AddStatus(Status status)
        {
            Statuses.Add(status);
        }

        public void AddRange(IList<Status> statuses)
        {
            foreach (var status in statuses)
            {
                Statuses.Add(status);
            }
        }

        public ulong MaxId => Statuses.Max().ID;

        public int Count => Statuses.Count;

        public Status At(ulong id)
        {
            return Statuses.First(status=>status.ID==id);
        }

        public IList<Status> GetAll()
        {
            return Statuses.ToList();
        }

        public IList<Status> Get(ulong minId, ulong maxId)
        {
            return Statuses.Where(status => status.ID >= minId && status.ID <= maxId).ToList();
        }
    }
}
