using TweetSharp;

namespace Server
{
    public static class TwitterOauth
    {
        public static string GetAuthorizationUri()
        {
            var service = new TwitterService("1WMZ0jYYuv8ZHrYI1L6hWN4m1",
                "XYXajdaRgzMi11pIm5FM4WHc4xRzJPpPIwSMRMbACOEkOHEMDL");
            var requestToken = service.GetRequestToken();
            var uri = service.GetAuthorizationUri(requestToken, "http://localhost:12008/auth");
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