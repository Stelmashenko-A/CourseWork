using System.Collections.Generic;
using System.Linq;
using LinqToTwitter;
using Repository;
using Server.StatusTasks;
using Account = Repository.Model.Account;


namespace Server
{
    public class Updater
    {
        private readonly IStorage _storage;
        private readonly StatusMapper _statusMapper =new StatusMapper();
        public Updater(IStorage storage)
        {
            _storage = storage;
        }

        private static IList<Status> Load(IQueryBuilder queryBuilder, ulong maxId, int count = 2000)
        {
            var statuses = new List<Status>();
            //todo limit exception
            var tweetQuery = queryBuilder.BuildTaskByMinId(maxId + 1).ToList();

            
            statuses.AddRange(tweetQuery);

            if (statuses.Count < 150)
            {
                return statuses;
            }

            while (statuses.Count < count)
            {
                tweetQuery = queryBuilder.BuildTaskByMinIdAndMaxId(maxId + 1, statuses[statuses.Count - 1].StatusID - 1).ToList();

                statuses.AddRange(tweetQuery);
                if (tweetQuery.Count < 150)
                {
                    break;
                }
            }
            return statuses;
        }

        public void UpDate(Account account)
        {
            var contextBuilder = new TwitterContextBuilder();
            IQueryBuilder queryBuilder = new QueryTimeLineBuilder(contextBuilder.Build(account.TwitterCredentials));
            var result = Load(queryBuilder, account.MaxId);
            if (result == null || result.Count == 0) return;
            _storage.AddStatuses(_statusMapper.Map(result));
            account.MaxId = result[0].StatusID;
            _storage.UpdateIdsAccount(account);
        }

        public void UpDate(IQueryable<Account> accounts)
        {
            foreach (var account in accounts)
            {
                UpDate(account);
            }
        }
    }
}