using System.Collections.Generic;
using System.Linq;
using Repository.Model;
using Account = Repository.Model.Account;

namespace Repository
{
    public interface IStorage
    {
        Account GetAccountById(long id);
        Account GetAccountByScreenName(string screenName);
        IQueryable<Account> GetAllAccounts();
        void AddAccount(Account account);
        void ResetTokens(string screenName, TwitterToken tokens);
        void UpdateIdsAccount(Account account, bool markAsInitialized = false);
        Page GetUserLine(long userId, int pageIndex, int pageSize, long pageHeaderId = long.MaxValue);
        IQueryable<TwitterStatus> GetAllStatuses(long userId);
        IQueryable<TwitterStatus> GetAllStatuses(string userName);
        void AddStatuses(IEnumerable<TwitterStatus> statuses);
        long GetLineHead(long id);
        void SetFollowing(Account account, IEnumerable<string> following);
        Page GetPageBefore(long userId, int pageIndex, int pageSize, long pageHeaderId = 0);
        void SetLastShownId(long userId, ulong statusId);
    }
}