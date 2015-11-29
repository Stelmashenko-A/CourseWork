using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using LinqToTwitter;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Linq;
using Repository.Model;
using Account = Repository.Model.Account;

namespace Repository
{
    public class Storage : IStorage
    {
        private readonly IDocumentStore _store;

        public Storage()
        {
            _store = new DocumentStore
            {
                Url = "http://localhost:8081/",
                DefaultDatabase = "Twitty"
            };
            _store.Initialize();
        }

        public Account GetAccountById(ulong id)
        {
            using (var session = _store.OpenSession())
            {
                return session.Query<Account>().First(account => account.TwitterCredentials.UserId == id);
            }
        }

        public Account GetAccountByScreenName(string screenName)
        {
            using (var session = _store.OpenSession())
            {
                return session.Query<Account>().First(account => account.TwitterCredentials.ScreenName == screenName);
            }
        }

        public IQueryable<Account> GetAllAccounts()
        {
            using (var session = _store.OpenSession())
            {
                return session.Query<Account>();
            }
        }

        public void AddAccount(Account account)
        {
            using (var session = _store.OpenSession())
            {
                session.Store(account);
                session.SaveChanges();
            }
        }

        public void ResetTokens(string screenName, TwitterToken tokens)
        {
            var oldAccount = GetAccountByScreenName(screenName);
            var newAccount =
                new Account(new TwitterCredentials(tokens, oldAccount.TwitterCredentials.ScreenName,
                    oldAccount.TwitterCredentials.UserId)) {LastReadedTweetId = oldAccount.LastReadedTweetId};
            using (var session = _store.OpenSession())
            {
                session.Delete(oldAccount);
                session.Store(newAccount);
                session.SaveChanges();
            }
        }

        public void SetFollowing(Account account, IList<string> following)
        {
            using (var session = _store.OpenSession())
            {
                var firstOrDefault =
                    session.Query<Account>()
                        .FirstOrDefault(x => x.TwitterCredentials.UserId == account.TwitterCredentials.UserId);
                if (firstOrDefault == null) return;
                firstOrDefault.Following = following;
                session.SaveChanges();
            }

        }

        public IList<string> GetFollowing(ulong id)
        {
            using (var session = _store.OpenSession())
            {
                var account = session.Query<Account>().FirstOrDefault(x => x.TwitterCredentials.UserId == id);
                return account?.Following;
            }
        }

        public void UpdateIdsAccount(Account account, bool markAsInitialized = false)
        {
            _store.DatabaseCommands.Patch(
                account.Id,
                new[]
                {
                    new PatchRequest
                    {

                        Name = "MaxId",
                        Value = account.MaxId
                    }
                });
            _store.DatabaseCommands.Patch(
                account.Id,
                new[]
                {
                    new PatchRequest
                    {

                        Name = "MinId",
                        Value = account.MinId
                    }
                });
            _store.DatabaseCommands.Patch(
                account.Id,
                new[]
                {
                    new PatchRequest
                    {

                        Name = "IsInitialized",
                        Value = account.IsInitialized
                    }
                });

        }


        public Page GetUserLine(ulong userId, int pageIndex, int pageSize, ulong pageHeaderId = ulong.MaxValue)
        {
            var following = GetFollowing(userId);
            using (new TransactionScope())
            {
                using (var session = _store.OpenSession())
                {
                    var skipCounter = 0;
                    if (pageHeaderId != uint.MaxValue)
                    {
                        skipCounter = session.Query<StatusModel, StatusesByIds>()
                            .Count(status => status.Status.StatusID > pageHeaderId);
                    }
                    var t = session.Query<StatusModel>().OrderByDescending(x => x.Status.CreatedAt)
                        .Where(status => status.Status.User.Name.In(following))
                        .Skip(skipCounter)
                        .Take(pageSize).Select(item => item.Status).ToList();
                    var page = new Page(t);
                    return page;
                }
            }
        }

        public IQueryable<Status> GetAllStatuses(uint userId)
        {
            using (var session = _store.OpenSession())
            {
                return Queryable.Select(session.Query<StatusModel>().Where(status => status.Status.UserID == userId),
                    item => item.Status);
            }
        }

        public IQueryable<Status> GetAllStatuses(string userName)
        {
            using (var session = _store.OpenSession())
            {
                var userId = GetAccountByScreenName(userName).TwitterCredentials.UserId;
                return Queryable.Select(session.Query<StatusModel>().Where(status => status.Status.UserID == userId),
                    item => item.Status);
            }
        }

        public void AddStatuses(IList<Status> statuses)
        {
            using (var session = _store.OpenSession())
            {
                foreach (var statuse in statuses)
                {
                    session.Store(new StatusModel(statuse));
                }
                session.SaveChanges();
            }
        }
    }
}
   
    

