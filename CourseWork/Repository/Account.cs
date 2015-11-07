using Repository.Model;

namespace Repository
{
    public class Account 
    {
        public Account(string username, string pass)
        {
            Username = username;
            Pass = pass;
            TimeLine=new TimeLine();
        }

        public long Id { get; set; }
        public string Username { get; private set; }
        public string Pass { get; private set; }
        public TimeLine TimeLine { get; private set; }
    }
}
