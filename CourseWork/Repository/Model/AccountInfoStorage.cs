using System.Collections.Generic;

namespace Repository.Model
{
    class AccountInfoStorage
    {
        public string Id { get; set; }
        public Dictionary<ulong, AccountInfo> Data { get; set; }

        public AccountInfoStorage()
        {
            Data=new Dictionary<ulong, AccountInfo>();
        }
    }

    public class AccountInfo
    {
        public AccountInfo(TwitterToken tokens, string screenName, ulong userId)
        {
            Tokens = tokens;
            ScreenName = screenName;
            UserId = userId;
            
        }

        public TwitterToken Tokens { get; protected set; }

        public string ScreenName { get; protected set; }

        public ulong UserId { get; protected set; }
    }

    public class Id
    {
        public Id(ulong id)
        {
            Value = id;
        }

        public ulong Value { get; protected set; }
    }
}
