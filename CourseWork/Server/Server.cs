using System;
using System.Collections.Generic;
using System.Linq;
using Quartz;
using Repository;
using Repository.Model;

namespace Server
{
    [DisallowConcurrentExecution]
    public class Server : IJob
    {
        public Server(IStorage storage)
        {
            _storage = storage;
        }
        private readonly IStorage _storage;

        public void Execute(IJobExecutionContext context)
        {
            
            var accounts = GetAllAccounts();
            var notInitializedAccounts = SelectNotInitialized(accounts);
            var initializer = new Initializer(_storage);
            initializer.Initialize(notInitializedAccounts);
            var upDater = new UpDater(_storage);
            upDater.UpDate(accounts);
        }

        protected IQueryable<Account> GetAllAccounts()
        {
            return _storage.GetAllAccounts();
        }

        protected IList<Account> SelectNotInitialized(IQueryable<Account> accounts)
        {
            return accounts.Where(account => account.IsInitialized == false).ToList();
        }
    }
}
