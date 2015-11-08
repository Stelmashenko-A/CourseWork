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
            //_accountRepository.
            Console.Beep();
        }
    }
}
