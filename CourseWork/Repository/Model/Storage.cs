using System.Collections.Generic;

namespace Repository.Model
{
    internal class Storage
    {
        public string Id { get; set; }
        public Dictionary<ulong, Account> Data { get; set; }

        public Storage()
        {
            Data = new Dictionary<ulong, Account>();
        }
    }
}
