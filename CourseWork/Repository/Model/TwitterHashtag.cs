using System.Collections.Generic;

namespace Repository.Model
{
    public class TwitterHashtag
    {
        public string Text { get; set; }
        public List<int> Indices { get; set; }
    }
}