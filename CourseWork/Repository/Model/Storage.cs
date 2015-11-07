using System.Collections.Generic;

namespace Repository.Model
{
    class Storage
    {
        public string Id { get; set; }
        public Dictionary<ulong, Account> Data { get; set; }

        public Storage()
        {
            Data=new Dictionary<ulong, Account>();
        }
    }

    public class Account
    {
        public Account(TwitterToken tokens, string screenName, ulong userId)
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
