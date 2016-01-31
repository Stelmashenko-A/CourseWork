namespace Repository.Model
{
    public class TwitterContributor
    {
        public TwitterContributor(string id, string screeName)
        {
            Id = id;
            ScreeName = screeName;
        }

        public string Id { get; }
        public string ScreeName { get; }
    }
}