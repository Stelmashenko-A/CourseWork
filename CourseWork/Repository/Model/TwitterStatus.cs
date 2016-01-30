using System;
using System.Collections.Generic;

namespace Repository.Model
{
    public class TwitterStatus
    {
        public TwitterCoordinates Coordinates { get; set; }
        public bool Favorited { get; set; }
        public bool Truncated { get; set; }
        public DateTime CreatedAt { get; set; }
        public string IdStr { get; set; }
        public TwitterEntities TwitterEntities { get; set; }
        public string InReplyToUserIdStr { get; set; }
        public List<long> Contributors { get; set; }
        public string Text { get; set; }
        public int RetweetCount { get; set; }
        public string InReplyToStatusIdStr { get; set; }
        public long Id { get; set; }
        public bool Retweeted { get; set; }
        public bool PossiblySensitive { get; set; }
        //  public object place { get; set; }
        public long UserId { get; set; }
        public string InReplyToScreenName { get; set; }
        public string Source { get; set; }
    }
}