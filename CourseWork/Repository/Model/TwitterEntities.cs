using System.Collections.Generic;

namespace Repository.Model
{
    public class TwitterEntities
    {
        public List<TwitterUrl> TwitterUrl { get; set; }
        public List<TwitterHashtag> TwitterHashtag { get; set; }
        public List<UserMention> UserMentions { get; set; }
    }
}