using System.Collections.Generic;
using System.Linq;
using Nancy.Security;

namespace SelfHostedRestAPI
{
    public class UserIdentity : IUserIdentity
    {
        public string UserName { get; }
        public IEnumerable<string> Claims { get; }
        
        public UserIdentity()
        {
            UserName = "user";
            var s = new List<string>();
            s.Add("admin");
            s.Add("user");
            Claims = s;

        }

        public UserIdentity(string username, IEnumerable<string> claims)
        {
            UserName = username;
            var list = claims.ToList();
            list.Add(UserName);
            Claims = list;
        }
    }
}