using System.Collections.Generic;

namespace Repository.Model
{
    class TimeLine
    {
        public string Id;
        public SortedList<string,HashSet<KeyValuePair<string,string>>> Tweets { get; set; } 
    }

    class TextTimeLine
    {
        public string Id;
        public SortedList<string, Metadata> Tweet { get; set; } 
    }

    internal class Metadata
    {
        public IEnumerable<string> Ids { get; set; } 
    }
}
