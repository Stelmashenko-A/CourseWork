using TweetSharp;

namespace Server
{
    public static class TwitterOauth
    {
        public static string GetAuthorizationUri()
        {
            var service = new TwitterService(ConsumerToken.ConsumerKey,
                ConsumerToken.ConsumerSecret);
            var requestToken = service.GetRequestToken();
            var uri = service.GetAuthorizationUri(requestToken, "http://127.0.0.1:1400/#/twitter-tokens'");
            return uri.ToString();
        }

        public static string AuthorizeCallback(string oauthToken, string oauthVerifier)
        {
            var requestToken = new OAuthRequestToken {Token = oauthToken};
            var service = new TwitterService(ConsumerToken.ConsumerKey,
                ConsumerToken.ConsumerSecret);
            var accessToken = service.GetAccessToken(requestToken, oauthVerifier);
            return accessToken.Token;
        }

        public static void GetTokens(string oautToken, string verifier, out string token, out string tokenSecret,
            out string screenName, out long id)
        {
            var requestToken = new OAuthRequestToken {Token = oautToken};
            var service = new TwitterService(ConsumerToken.ConsumerKey,
                ConsumerToken.ConsumerSecret);
            var accessToken = service.GetAccessToken(requestToken, verifier);
            token = accessToken.Token;
            tokenSecret = accessToken.TokenSecret;
            screenName = accessToken.ScreenName;
            id = accessToken.UserId;
        }
    }
}