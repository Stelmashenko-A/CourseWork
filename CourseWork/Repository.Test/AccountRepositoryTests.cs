using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}
