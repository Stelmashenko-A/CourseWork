using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Server;
using TestUtiles;

namespace Repository.Test
{
    [TestClass]
    public class AccountRepositoryTests
    {
        private readonly AccountRepository _accountRepository = new AccountRepository();

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
            var statuses = initializer.LoadUserStatuses(credentials);

            accountRepository.DelUserStatuses(2765688547);
            accountRepository.SetUserStatuses(2765688547, statuses.ToList());

            var statusCounter = new StatusCounter();
            Assert.AreEqual(statusCounter.Count(2765688547, accountRepository),
                _accountRepository.Get(2765688547).UserStatuses.Statuses.Count
                );
        }
    }
}