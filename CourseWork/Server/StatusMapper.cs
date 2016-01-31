using System.Collections.Generic;
using System.Linq;
using LinqToTwitter;
using Repository.Model;
using TwitterEntities = Repository.Model.TwitterEntities;
using TwitterStatus = Repository.Model.TwitterStatus;
using TwitterUrl = Repository.Model.TwitterUrl;

namespace Server
{
    internal class StatusMapper
    {
        public TwitterStatus Map(Status status)
        {
            return new TwitterStatus(new TwitterCoordinates(status.Coordinates.Longitude, status.Coordinates.Latitude),
                status.Favorited, status.Truncated, status.CreatedAt, status.StatusID.ToString(), Map(status.Entities),
                status.InReplyToUserID.ToString(), Map(status.Contributors), status.Text, status.RetweetCount,
                status.InReplyToStatusID.ToString(), (long)status.StatusID, status.Retweeted, status.PossiblySensitive,
                (long)status.User.UserID, status.InReplyToScreenName, status.Source,status.User.UserID.ToString());
        }

        public IEnumerable<TwitterStatus> Map(IEnumerable<Status> statuses)
        {
            return statuses.Select(Map);
        } 

        private static TwitterEntities Map(Entities entities)
        {
            return new TwitterEntities(Map(entities.UrlEntities), Map(entities.HashTagEntities),
                Map(entities.UserMentionEntities));
        }

        private static TwitterUrl Map(UrlEntity urlEntity)
        {
            return new TwitterUrl(urlEntity.ExpandedUrl, urlEntity.Url, urlEntity.DisplayUrl, urlEntity.Start,
                urlEntity.End);
        }

        static IEnumerable<TwitterUrl> Map(IEnumerable<UrlEntity> urlEntities)
        {
            return urlEntities.Select(Map);
        }

        private static TwitterHashtag Map(HashTagEntity hashTagEntity)
        {
            return new TwitterHashtag(hashTagEntity.Tag, hashTagEntity.Start, hashTagEntity.End);
        }

        private static IEnumerable<TwitterHashtag> Map(IEnumerable<HashTagEntity> hashTagEntities)
        {
            return hashTagEntities.Select(Map);
        }

        private static UserMention Map(UserMentionEntity userMentionEntity)
        {
            return new UserMention(userMentionEntity.Name, userMentionEntity.ScreenName, (long)userMentionEntity.Id,
                userMentionEntity.Id.ToString(), userMentionEntity.Start, userMentionEntity.End);
        }

        private static IEnumerable<UserMention> Map(IEnumerable<UserMentionEntity> userMentionEntities)
        {
            return userMentionEntities.Select(Map);
        }

        private static TwitterContributor Map(Contributor contributor)
        {
            return new TwitterContributor(contributor.ID, contributor.ScreenName);
        }

        private static IEnumerable<TwitterContributor> Map(IEnumerable<Contributor> contributors)
        {
            return contributors.Select(Map);
        }
    }
}
