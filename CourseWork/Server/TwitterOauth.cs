using TweetSharp;

namespace Server
{
    public static class TwitterOauth
    {
        public static string GetAuthorizationUri()
        {
            var service = new TwitterService("не забыл стереть", "не забыл стереть 2.0");

            var requestToken = service.GetRequestToken();

            var uri = service.GetAuthorizationUri(requestToken);
            return uri.ToString();
        }
    }
}
