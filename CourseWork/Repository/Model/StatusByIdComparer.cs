using System.Collections.Generic;
using LinqToTwitter;

namespace Repository.Model
{
    public class StatusByIdComparer : IComparer<Status>
    {
        public int Compare(Status x, Status y)
        {
            return x.ID.CompareTo(y.StatusID);
        }
    }
}