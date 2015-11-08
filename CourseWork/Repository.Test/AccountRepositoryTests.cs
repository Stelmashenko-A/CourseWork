using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Repository.Test
{
    [TestClass]
    public class AccountRepositoryTests
    {
        [TestMethod]
        public void TestGetAccountById()
        {
            var accountRepository = new AccountRepository();
            var t = accountRepository.Get(2765688547);
            Assert.AreEqual("__BuS_TeR__", t.ScreenName);
        }
    }
}
