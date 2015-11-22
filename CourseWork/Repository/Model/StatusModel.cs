using LinqToTwitter;

namespace Repository.Model
{
    public class StatusModel
    {
        public string Id { get; protected set; }
        public Status Status { get; protected set; }

        public StatusModel(Status status)
        {
            Status = status;
            Id = "statusmodels/" + status.StatusID;
        }
    }
}
