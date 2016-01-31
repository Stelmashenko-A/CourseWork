using System.Collections.Generic;

namespace Repository.Model
{
    public class Page
    {
        public Page(IEnumerable<TwitterStatus> statuses)
        {
            Statuses = statuses;
        }

        public IEnumerable<TwitterStatus> Statuses { get; }  
    }
}
