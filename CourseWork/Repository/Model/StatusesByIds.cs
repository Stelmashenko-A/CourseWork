using System.Linq;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace Repository.Model
{
    public class StatusesByIds : AbstractIndexCreationTask<StatusModel>
    {
        public StatusesByIds()
        {
            Map = statuses => from statuse in statuses
                              select new
                              {
                                  statuse.Status.User.Name

                              };

            Sort(x => x.Status.StatusID, SortOptions.Long);
        }
    }




}
