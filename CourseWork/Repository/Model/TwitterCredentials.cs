namespace Repository.Model
{
    public class TwitterCredentials
    {
        public TwitterCredentials(TwitterToken tokens, string screenName, ulong userId)
        {
            Tokens = tokens;
            ScreenName = screenName;
            UserId = userId;
        }

        public TwitterToken Tokens { get; private set; }

        public string ScreenName { get; private set; }

        public ulong UserId { get; private set; }
    }
}
