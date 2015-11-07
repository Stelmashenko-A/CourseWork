using System;
using Quartz;
using Repository;

namespace Server
{
    public class Server : IJob
    {
        private readonly AccountRepository _accountRepository = new AccountRepository();
        public void Execute(IJobExecutionContext context)
        {
            Loader loader = new Loader();
            AccountRepository accountRepository = new AccountRepository();
            //var tmp= accountRepository.Get(2765688547);
            // accountRepository.Add(12345,new Account(new TwitterToken("qwert","sdhggfd"),"sdfgh",12345));
            //loader.Load(accountRepository.GetAll(),ConsumerToken.ConsumerKey,ConsumerToken.ConsumerSecret);
            var t =  loader.Load(accountRepository.Get(2765688547));
            
            Console.Beep();
            /* var accounts = _accountRepository.GetAll();
            foreach (var account in accounts)
            {
                

            }*/
        }
    }
}
