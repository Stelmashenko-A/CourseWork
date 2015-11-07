using Repository;
using Repository.Model;
using Server;

namespace SelfHostedRestAPI
{
    using Nancy;

    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = parameters =>
            {
                Loader loader= new Loader();
                AccountRepository accountRepository=new AccountRepository();
               //var tmp= accountRepository.Get(2765688547);
               // accountRepository.Add(12345,new Account(new TwitterToken("qwert","sdhggfd"),"sdfgh",12345));
                loader.Load(accountRepository.GetAll(),ConsumerToken.ConsumerKey,ConsumerToken.ConsumerSecret);
                return View["index"];
            };
            Post["/"] = parameters =>
            {
                return View["index"];
            };
        }
    }
}