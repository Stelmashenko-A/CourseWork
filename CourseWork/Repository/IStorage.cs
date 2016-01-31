using System.Collections.Generic;
using System.Linq;
using Repository.Model;
using Account = Repository.Model.Account;

namespace Repository
{
    public interface IStorage
    {
        Account GetAccountById(ulong id);
        Account GetAccountByScreenName(string screenName);
        IQueryable<Account> GetAllAccounts();
        void AddAccount(Account account);
        void ResetTokens(string screenName, TwitterToken tokens);
        void UpdateIdsAccount(Account account, bool markAsInitialized = false);
        Page GetUserLine(ulong userId, int pageIndex, int pageSize, ulong pageHeaderId = ulong.MaxValue);
        IQueryable<TwitterStatus> GetAllStatuses(ulong userId);
        IQueryable<TwitterStatus> GetAllStatuses(string userName);
        void AddStatuses(IList<TwitterStatus> statuses);
        ulong GetLineHead(ulong id);
        void SetFollowing(Account account, IList<ulong> following);
    }
}