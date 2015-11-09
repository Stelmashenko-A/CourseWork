using System.Linq;
using LinqToTwitter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;
using TestUtiles;

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
            accountRepository.DelUserStatuses(2765688547);
            var credentials = accountRepository.GetTwitterCredentials(2765688547);
            var statuses = initializer.LoadUserStatuses(credentials);

            var statusCounter = new StatusCounter();
            Assert.AreEqual(statusCounter.Count(2765688547, accountRepository), statuses.Count);
        }
    }
}