namespace Repository.Model
{
    public class Account
    {
        public Account(TwitterCredentials twitterCredentials)
        {
            TwitterCredentials = twitterCredentials;
            UserStatuses = new TimeLine();
            UserTimeLine = new TimeLine();
        }

        public TwitterCredentials TwitterCredentials { get; protected set; }

        public TimeLine UserStatuses { get; protected set; }

        public TimeLine UserTimeLine { get; protected set; }
    }
}