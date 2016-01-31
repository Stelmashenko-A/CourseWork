namespace Repository.Model
{
    public class TwitterUrl
    {
        public TwitterUrl(string expandedUrl, string fullUrl, string displayUrl, int start, int end)
        {
            ExpandedUrl = expandedUrl;
            FullUrl = fullUrl;
            DisplayUrl = displayUrl;
            Start = start;
            End = end;
        }

        public string ExpandedUrl { get; }
        public string FullUrl { get;  }
        
        public string DisplayUrl { get;  }

        public int Start { get; }
        public int End { get; }
    }
}