namespace Repository.Model
{
    public class TwitterToken
    {
        public string Token { get; protected set; }

        public string TokenSecret { get; protected set; }

        public TwitterToken(string token, string tokenSecret)
        {
            Token = token;
            TokenSecret = tokenSecret;
        }
    }
}
