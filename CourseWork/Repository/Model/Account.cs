namespace Repository.Model
{
    public class Account
    {
        public Account(TwitterToken tokens, string screenName, ulong userId)
        {
            Tokens = tokens;
            ScreenName = screenName;
            UserId = userId;
            TimeLine=new TimeLine();
        }

        public TwitterToken Tokens { get; protected set; }

        public string ScreenName { get; protected set; }

        public ulong UserId { get; protected set; }

        public TimeLine TimeLine { get; private set; }
    }
}