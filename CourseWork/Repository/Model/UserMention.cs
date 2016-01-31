namespace Repository.Model
{
    public class UserMention
    {
        public UserMention(string name, string screenName, long id, string idStr, int start, int end)
        {
            Name = name;
            ScreenName = screenName;
            Id = id;
            IdStr = idStr;
            Start = start;
            End = end;
        }

        public string Name { get;  }
        public string ScreenName { get;  }
        public long Id { get;  }
        public string IdStr { get; }
        public int Start { get; }
        public int End { get; }
    }
}