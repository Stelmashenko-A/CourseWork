using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Model;
using Server.StatusTasks;

namespace Server.Test
{
    [TestClass]
    public class ReplyLoaderTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            for (int i = 0; i < 20; i++)
            {
                var replyLoader = new ReplyLoader();
                TwitterContextBuilder twitterContextBuilder = new TwitterContextBuilder();
                var context = twitterContextBuilder.Build(
                    new TwitterCredentials(
                        new TwitterToken("",
                            ""), "__BuS_TeR__", 2765688547));
                var t = replyLoader.LoadReplies(new TaskBuilder(context), 716202018751266817);
            }
        }
    }
}
