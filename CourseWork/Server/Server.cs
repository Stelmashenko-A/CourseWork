using System;
using System.Linq;
using LinqToTwitter;
using Quartz;
using Repository;

namespace Server
{
    public class Server : IJob
    {
        private readonly AccountRepository _accountRepository = new AccountRepository();
        public void Execute(IJobExecutionContext context)
        {
            var accounts = _accountRepository.GetAll();
            foreach (var account in accounts)
            {
                

            }
        }
    }
}
