using System.Linq;
using Server;

namespace TestUtiles
{
    public class StatusCounter
    {
        public int Count(ulong userId)
        {
            var contextBuilder = new TwitterContextBuilder();
            var twitterContext = contextBuilder.Build(accountRepository.GetTwitterCredentials(2765688547));
            var userResponse =
                (from user in twitterContext.User
                 where user.Type == UserType.Lookup &&
                       user.UserIdList == "2765688547"
                 select user).ToListAsync();
            userResponse.Wait();
            var statusesCount = userResponse.Result[0].StatusesCount;
        }
    }
}
