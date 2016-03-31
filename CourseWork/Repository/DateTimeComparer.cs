using System;
using System.Collections.Generic;

namespace Repository
{
    internal class DateTimeComparer:IComparer<DateTime>
    {
        public int Compare(DateTime x, DateTime y)
        {
            return y.CompareTo(x);
        }
    }
}
