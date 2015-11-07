using System.Collections.Generic;
using System.Linq;
using LinqToTwitter;
using Account = Repository.Model.Account;


namespace Server
{
    public class Loader
    {
        public List<Status> Load(Account account)
        {
          var singleUserAuthorizer = new SingleUserAuthorizer
            {
                CredentialStore = new InMemoryCredentialStore
                {
                    ConsumerKey = ConsumerToken.ConsumerKey,
                    ConsumerSecret = ConsumerToken.ConsumerSecret,
                    OAuthToken = account.Tokens.Token,
                    OAuthTokenSecret = account.Tokens.TokenSecret,
                    ScreenName = account.ScreenName,
                    UserID = account.UserId
                }
            };

            var twitterCtx = new TwitterContext(singleUserAuthorizer);

            var tmp =

                (from tweet in twitterCtx.Status
                    where tweet.Type == StatusType.User &&
                          tweet.ScreenName == account.ScreenName &&
                          tweet.SinceID == account.TimeLine.MaxId()
                    select tweet).ToListAsync();
            tmp.Wait();
            return tmp.Result ;

        }


    }
}
