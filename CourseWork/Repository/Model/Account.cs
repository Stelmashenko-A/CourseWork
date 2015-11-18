namespace Repository.Model
{
    public class Account
    {
        public Account(TwitterCredentials twitterCredentials)
        {
            TwitterCredentials = twitterCredentials;
        }

        public TwitterCredentials TwitterCredentials { get; protected set; }

        public ulong LastReadedTweetId { get; set; }
        public ulong MaxId { get; set; }
    }
}