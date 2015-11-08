using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository;

namespace Server.Test
{
    [TestClass]
    public class LoaderTests
    {
        [TestMethod]
        public void TestTweetLoadingWithoutCounter()
        {
            var loader = new Loader();
            var accountRepository = new AccountRepository();
            var t = loader.Load(accountRepository.Get(2765688547));
            Assert.AreEqual(20,t.Count);
        }

        [TestMethod]
        public void TestTweetLoadingWithCounter()
        {
            var loader = new Loader();
            var accountRepository = new AccountRepository();
            var t = loader.Load(accountRepository.Get(2765688547),123);
            Assert.AreEqual(123, t.Count);
        }
    }
}
