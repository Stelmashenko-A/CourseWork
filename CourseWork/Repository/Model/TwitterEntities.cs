using System.Collections.Generic;

namespace Repository.Model
{
    public class TwitterEntities
    {
        public TwitterEntities(IEnumerable<TwitterUrl> twitterUrl, IEnumerable<TwitterHashtag> twitterHashtag, IEnumerable<UserMention> userMentions)
        {
            TwitterUrl = twitterUrl;
            TwitterHashtag = twitterHashtag;
            UserMentions = userMentions;
        }

        public IEnumerable<TwitterUrl> TwitterUrl { get; }
        public IEnumerable<TwitterHashtag> TwitterHashtag { get;}
        public IEnumerable<UserMention> UserMentions { get; }
    }
}