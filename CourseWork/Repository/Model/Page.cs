using System.Collections.Generic;
using LinqToTwitter;

namespace Repository.Model
{
    public class Page
    {
        public Page(IEnumerable<Status> statuses)
        {
            Statuses = statuses;
        }

        public IEnumerable<Status> Statuses { get; }  
    }
}
