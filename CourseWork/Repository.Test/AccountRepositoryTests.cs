using System.Linq;
using LinqToTwitter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server;

namespace Repository.Test
{
    [TestClass]
    public class AccountRepositoryTests
    {
        readonly AccountRepository _accountRepository=new AccountRepository();

        [TestMethod]
        public void TestGetAccountById()
        {
            var t = _accountRepository.Get(2765688547);
            Assert.AreEqual("__BuS_TeR__", t.TwitterCredentials.ScreenName);
        }

        [TestMethod]
        public void TestGetAllTwitterCredentials()
        {
            var t = _accountRepository.GetAllTwitterCredentialses();
            Assert.IsNotNull(t);
        }

        [TestMethod]
        public void TestGetTwitterCredentials()
        {
            var t = _accountRepository.GetTwitterCredentials(2765688547);
            Assert.IsNotNull(t);
        }
        [TestMethod]
        public void TestSetUserStatuses()
        {
            var initializer = new Initializer();
            var accountRepository = new AccountRepository();
            var credentials = accountRepository.GetTwitterCredentials(2765688547);
            var statuses = initializer.LoadStatuses(credentials);
            
            accountRepository.DelUserStatuses(2765688547);
            accountRepository.SetUserStatuses(2765688547,statuses.ToList());

            var contextBuilder = new TwitterContextBuilder();
            var twitterContext = contextBuilder.Build(accountRepository.GetTwitterCredentials(2765688547));
            var userResponse =
                (from user in twitterContext.User
                 where user.Type == UserType.Lookup &&
                       user.UserIdList == "2765688547"
                 select user).ToListAsync();
            userResponse.Wait();
            var statusesCount = userResponse.Result[0].StatusesCount;

            Assert.AreEqual(_accountRepository.Get(2765688547).UserStatuses.Statuses.Count, statusesCount);
           
        }
    }
}
