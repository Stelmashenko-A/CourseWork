namespace Repository
{
    public class Account
    {
        public Account(long id, long username, long pass)
        {
            Id = id;
            Username = username;
            Pass = pass;
        }

        public long Id { get; private set; }
        public long Username { get; private set; }
        public long Pass { get; private set; }
    }
}
