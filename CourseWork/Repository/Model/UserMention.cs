using System.Collections.Generic;

namespace Repository.Model
{
    public class UserMention
    {
        public string Name { get; set; }
        public List<int> Indices { get; set; }
        public string ScreenName { get; set; }
        public int Id { get; set; }
        public string IdStr { get; set; }
    }
}