using System;
using System.Collections.Generic;

namespace Repository.Model
{
    public class TwitterStatus
    {
        public TwitterStatus(TwitterCoordinates coordinates, bool favorited, bool truncated, DateTime createdAt,
            string idStr, TwitterEntities twitterEntities, string inReplyToUserIdStr, IEnumerable<TwitterContributor> contributors,
            string text, int retweetCount, string inReplyToStatusIdStr, long id, bool retweeted, bool possiblySensitive,
            long userId, string inReplyToScreenName, string source, string userIdStr)
        {
            Coordinates = coordinates;
            Favorited = favorited;
            Truncated = truncated;
            CreatedAt = createdAt;
            IdStr = idStr;
            TwitterEntities = twitterEntities;
            InReplyToUserIdStr = inReplyToUserIdStr;
            Contributors = contributors;
            Text = text;
            RetweetCount = retweetCount;
            InReplyToStatusIdStr = inReplyToStatusIdStr;
            Id = id;
            Retweeted = retweeted;
            PossiblySensitive = possiblySensitive;
            UserId = userId;
            InReplyToScreenName = inReplyToScreenName;
            Source = source;
            UserIdStr = userIdStr;
            InternalId = Id;
        }
        internal long InternalId { get; }
        public TwitterCoordinates Coordinates { get; }
        public bool Favorited { get; }
        public bool Truncated { get; }
        public DateTime CreatedAt { get; }
        public string IdStr { get; }
        public TwitterEntities TwitterEntities { get; }
        public string InReplyToUserIdStr { get; }
        public IEnumerable<TwitterContributor> Contributors { get; }
        public string Text { get; }
        public int RetweetCount { get; }
        public string InReplyToStatusIdStr { get; }
        public long Id { get; }
        public bool Retweeted { get; }
        public bool PossiblySensitive { get; }
        //  public object place { get; set; }
        public long UserId { get; }
        public string UserIdStr { get; }
        public string InReplyToScreenName { get; }
        public string Source { get; }
    }
}