using LinqToTwitter;
using Repository.Model;

namespace Server
{
    public class TwitterContextBuilder
    {
        public TwitterContext Build(TwitterCredentials credentials)
        {
            var singleUserAuthorizer = new SingleUserAuthorizer
            {
                CredentialStore = new InMemoryCredentialStore
                {
                    ConsumerKey = ConsumerToken.ConsumerKey,
                    ConsumerSecret = ConsumerToken.ConsumerSecret,
                    OAuthToken = credentials.Tokens.Token,
                    OAuthTokenSecret = credentials.Tokens.TokenSecret,
                    ScreenName = credentials.ScreenName,
                    UserID = (ulong)credentials.UserId
                }
            };
            return new TwitterContext(singleUserAuthorizer);
        }
    }
}