using System.Collections.Generic;

namespace Repository.Model
{
    class AccountInfoStorage
    {
        public Dictionary<Id, AccountInfo> Data { get; set; }
    }

    public class AccountInfo
    {
        public TwitterToken Tokens { get; set; }
    }

    public class Id
    {
    }
}
