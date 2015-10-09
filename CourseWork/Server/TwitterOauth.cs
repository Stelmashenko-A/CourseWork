using TweetSharp;

namespace Server
{
    public static class TwitterOauth
    {
        public static string GetAuthorizationUri()
        {
            var service = new TwitterService("",
                "");
            var requestToken = service.GetRequestToken();
            var uri = service.GetAuthorizationUri(requestToken);
            return uri.ToString();
        }

        public static string AuthorizeCallback(string oauthToken, string oauthVerifier)
        {
            var requestToken = new OAuthRequestToken {Token = oauthToken};
            var service = new TwitterService("",
                "");
            var accessToken = service.GetAccessToken(requestToken, oauthVerifier);
            return accessToken.Token;
        }
    }
}