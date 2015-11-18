using System.Linq;
using System.Transactions;
using LinqToTwitter;
using Raven.Client;
using Raven.Client.Document;
using Repository.Model;
using Account = Repository.Model.Account;

namespace Repository
{
    public class Storage
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

        public Account GetAccountById(uint id)
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

        public Page GetUserLine(uint userId, int pageIndex, int pageSize, uint pageHeaderId = uint.MaxValue)
        {
            using (new TransactionScope())
            {
                using (var session = _store.OpenSession())
                {
                    var skipCounter = 0;
                    if (pageHeaderId != uint.MaxValue)
                    {
                        skipCounter = session.Query<Status>().Count(status => status.StatusID > pageHeaderId);
                    }
                    var page = new Page(
                        session.Query<Status>()
                            .Where(status => status.UserID == userId)
                            .Skip((pageIndex - 1)*pageSize + skipCounter)
                            .Take(pageSize)
                        );
                    return page;
                }
            }
        }

        public IQueryable<Status> GetAll(uint userId)
        {
            using (var session = _store.OpenSession())
            {
                return session.Query<Status>().Where(status => status.UserID == userId);
            }
        }

        public IQueryable<Status> GetAll(string userName)
        {
            using (var session = _store.OpenSession())
            {
                var userId = GetAccountByScreenName(userName).TwitterCredentials.UserId;
                return session.Query<Status>().Where(status => status.UserID == userId);
            }
        }
    }
}
   
    

