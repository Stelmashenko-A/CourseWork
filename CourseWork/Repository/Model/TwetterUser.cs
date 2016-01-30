namespace Repository.Model
{
    public class TwetterUser
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string IdStr { get; set; }
        public TwitterEntities Entities { get; set; }
        public bool ContributorsEnabled { get; set; }
        public int FavouritesCount { get; set; }
        public string Url { get; set; }
        public string ProfileImageUrlHttps { get; set; }
        public int UtcOffset { get; set; }
        public long Id { get; set; }
        public int ListedCount { get; set; }
        public string Lang { get; set; }
        public int FollowersCount { get; set; }
        public bool Protected { get; set; }
        //  public object notifications { get; set; }
        public bool Verified { get; set; }
        public bool GeoEnabled { get; set; }
        public string TimeZone { get; set; }
        public string Description { get; set; }
        public bool DefaultProfileImage { get; set; }
        public string ProfileBackgroundImageUrl { get; set; }
        public int StatusesCount { get; set; }
        public int FriendsCount { get; set; }
        public bool ShowAllInlineMedia { get; set; }
        public string ScreenName { get; set; }
    }
}