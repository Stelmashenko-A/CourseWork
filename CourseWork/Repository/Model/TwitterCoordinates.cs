namespace Repository.Model
{
    public class TwitterCoordinates
    {
        public TwitterCoordinates(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
        }

        public double Longitude { get; }
        public double Latitude { get; }
    }
}