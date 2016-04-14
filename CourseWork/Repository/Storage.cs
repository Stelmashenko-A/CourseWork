using System.Collections.Generic;
using System.Linq;
using System.Transactions;
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

        public Account GetAccountById(long id)
        {
            using (var session = _store.OpenSession())
            {
                return session.Query<Account>().FirstOrDefault(account => account.TwitterCredentials.UserId == id);
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

            using (var session = _store.OpenSession())
            {
                var firstOrDefault = session.Advanced.LuceneQuery<Account>()
                    .FirstOrDefault(item => item.TwitterCredentials.ScreenName == screenName);
                if (firstOrDefault != null)
                    firstOrDefault
                        .TwitterCredentials.Tokens = tokens;
                session.SaveChanges();
            }
        }

        public void SetFollowing(Account account, IEnumerable<string> following)
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

        public IEnumerable<string> GetFollowing(long id)
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


        public Page GetUserLine(long userId, int pageIndex, int pageSize, long pageHeaderId = long.MaxValue)
        {
            var following = GetFollowing(userId);
            using (new TransactionScope())
            {
                using (var session = _store.OpenSession())
                {
                    var skipCounter = 0;
                    if (pageHeaderId != -1)
                    {
                        skipCounter = session.Query<TwitterStatus>()
                            .Count(status => status.InternalId > pageHeaderId);
                    }
                    var t = session.Query<TwitterStatus>().OrderByDescending(x => x.CreatedAt)
                        .Where(status => status.UserIdStr.In(following))
                        .Skip(skipCounter)
                        .Take(pageSize).ToList();
                    var page = new Page(t);
                    return page;
                }
            }
        }

        public Page GetPageBefore(long userId, int pageIndex, int pageSize, long pageHeaderId = 0)
        {
            var following = GetFollowing(userId);
            using (new TransactionScope())
            {//problem in logic
                using (var session = _store.OpenSession())
                {
                    var skipCounter = 0;
                    if (pageHeaderId != -1)
                    {
                        skipCounter = session.Query<TwitterStatus>()
                            .Count(status => status.InternalId > pageHeaderId);
                        skipCounter -= (pageIndex + 1) * pageSize;
                    }
                    if (skipCounter < -pageSize)
                    {
                        return  new Page(new List<TwitterStatus>());
                    }
                    if (skipCounter < 0)
                    {
                        var f = session.Query<TwitterStatus>().OrderByDescending(x => x.CreatedAt)
                        .Where(status => status.UserIdStr.In(following))
                        .Take(pageSize+skipCounter)
                        .ToList();
                        return new Page(f);

                    }
                    var r = session.Query<TwitterStatus>()
                        .Where(status => status.InternalId > pageHeaderId && status.UserIdStr.In(following)).OrderByDescending(x => x.CreatedAt, new DateTimeComparer()).Skip(pageIndex * pageSize)
                        .Take(pageSize).ToList();
                    var t = session.Query<TwitterStatus>().OrderByDescending(x => x.CreatedAt)
                        .Where(status => status.UserIdStr.In(following))
                        .Skip(skipCounter)
                        .Take(pageSize)
                        .ToList();
                    var page = new Page(t);
                    return page;
                }
            }
        }

        public void SetLastShownId(long userId, ulong statusId)
        {
            using (var session = _store.OpenSession())
            {

                var updated = session.Query<Account>().FirstOrDefault(account => account.TwitterCredentials.UserId == userId);
                if (updated == null) return;
                updated.LastReadedTweetId = statusId;
                AddAccount(updated);
            }
        }

        public TwitterStatus GetTweeById(ulong tweetId)
        {
            using (var session = _store.OpenSession())
            {
                return session.Query<TwitterStatus>().FirstOrDefault(status => status.IdStr == tweetId.ToString());
            }
        }

        public IQueryable<TwitterStatus> GetAllStatuses(long userId)
        {
            using (var session = _store.OpenSession())
            {
                return Queryable.Select(session.Query<TwitterStatus>().Where(status => status.UserId == userId),
                    item => item);
            }
        }

        public IQueryable<TwitterStatus> GetAllStatuses(string userName)
        {
            using (var session = _store.OpenSession())
            {
                var userId = GetAccountByScreenName(userName).TwitterCredentials.UserId;
                return Queryable.Select(session.Query<TwitterStatus>().Where(status => status.UserId == userId),
                    item => item);
            }
        }

        public void AddStatuses(IEnumerable<TwitterStatus> statuses)
        {
            using (var session = _store.OpenSession())
            {
                foreach (var status in statuses)
                {
                    session.Store(status);
                }
                session.SaveChanges();
            }
        }

        public long GetLineHead(long id)
        {
            using (var session = _store.OpenSession())
            {
                return session.Query<TwitterStatus>().OrderByDescending(x => x.CreatedAt).First().Id;
            }
        }


    }
}



