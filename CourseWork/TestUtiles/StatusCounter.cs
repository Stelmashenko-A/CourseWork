using System.Linq;
using LinqToTwitter;
using Repository;
using Server;

namespace TestUtiles
{
    public class StatusCounter
    {
        public int Count(ulong userId, AccountRepository accountRepository)
        {
            var contextBuilder = new TwitterContextBuilder();
            var twitterContext = contextBuilder.Build(accountRepository.GetTwitterCredentials(2765688547));
            var userResponse =
                (from user in twitterContext.User
                    where user.Type == UserType.Lookup &&
                          user.UserIdList == userId.ToString()
                    select user).ToListAsync();
            userResponse.Wait();
            return userResponse.Result[0].StatusesCount;
        }
    }
}
