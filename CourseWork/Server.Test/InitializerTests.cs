using System.Linq;
using LinqToTwitter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;

namespace Server.Test
{
    [TestClass]
    public class InitializerTests
    {
        [TestMethod]
        public void TestLoadStatuses()
        {
           
            var initializer = new Initializer();
            var accountRepository = new AccountRepository();
            var credentials = accountRepository.GetTwitterCredentials(2765688547);
            var statuses = initializer.LoadStatuses(credentials);

            var contextBuilder = new TwitterContextBuilder();
            var twitterContext = contextBuilder.Build(accountRepository.GetTwitterCredentials(2765688547));
            var userResponse =
                (from user in twitterContext.User
                    where user.Type == UserType.Lookup &&
                          user.UserIdList == "2765688547"
                    select user).ToListAsync();
            userResponse.Wait();
            var statusesCount = userResponse.Result[0].StatusesCount;
            Assert.AreEqual(statusesCount, statuses.Count);
        }
    }
}