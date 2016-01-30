using System.Collections.Generic;

namespace Repository.Model
{
    public class TwitterUrl
    {
        public string ExpandedUrl { get; set; }
        public string FullUrl { get; set; }
        public List<int> Indices { get; set; }
        public string DisplayUrl { get; set; }
    }
}