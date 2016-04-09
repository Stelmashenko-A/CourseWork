using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Server.Test
{
    [TestClass]
    public class ReplyLoaderTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            for (var i = 0; i < 20; i++)
            {
                var replyLoader = new ReplyLoader();
                replyLoader.LoadReplies(2765688547, 716202018751266817);
            }
        }
    }
}
