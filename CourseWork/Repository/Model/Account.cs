namespace Repository.Model
{
    public class Account
    {
        public Account(TwitterCredentials twitterCredentials)
        {
            TwitterCredentials = twitterCredentials;
            TimeLine = new TimeLine();
        }

        public TwitterCredentials TwitterCredentials { get; private set; }

        public TimeLine TimeLine { get; private set; }
    }
}