using System.Collections.Generic;
using System.Linq;
using LinqToTwitter;
using Repository;
using Repository.Model;
using Server.StatusTasks;
using Account = Repository.Model.Account;

namespace Server
{
    public class Initializer
    {
        private readonly Storage _storage;

        protected IList<Status> LoadStatuses(IQueryBuilder queryBuilder, ulong maxId, int count = 3200)
        {
            var statuses = new List<Status>();
            var tweetQuery = queryBuilder.BuildTaskByMaxId(maxId).ToList();

            
            statuses.AddRange(tweetQuery);


            while (statuses.Count < count)
            {
                tweetQuery = queryBuilder.BuildTaskByMaxId(statuses[statuses.Count - 1].StatusID - 1).ToList();

                statuses.AddRange(tweetQuery);
                if (tweetQuery.Count ==0)
                {
                    break;
                }
            }
            return statuses;
        }

        public IList<Status> LoadUserTimeLine(TwitterCredentials credentials)
        {
            var twitterContextBuilder = new TwitterContextBuilder();
            IQueryBuilder userStatusesBuilder = new QueryTimeLineBuilder(twitterContextBuilder.Build(credentials));
            return LoadStatuses(userStatusesBuilder, 3000);
        }

        public Initializer(Storage storage)
        {
            _storage = storage;
        }

        public void Initialize(Account account)
        {
            var twitterContextBuilder = new TwitterContextBuilder();
            IQueryBuilder queryBuilder = new QueryTimeLineBuilder(twitterContextBuilder.Build(account.TwitterCredentials));
            var result = LoadStatuses(queryBuilder, account.MinId - 1);
            account.MarkAsInitialized();
            account.MinId = result[result.Count - 1].StatusID;
            if (account.MaxId < result[0].StatusID)
            {
                account.MaxId = result[0].StatusID;
            }
            _storage.AddStatuses(result);
            _storage.UpdateIdsAccount(account, true);
        }

        public void Initialize(IList<Account> accounts)
        {
            foreach (var account in accounts)
            {
                Initialize(account);
            }
        }
    }
}
