namespace Passenger.Core.Domain
{
    public class Node
    {
        public string Address { get; protected set; }
        public double Latitude { get; protected set; }
        public double Longitude { get; protected set; }

        protected Node()
        {
        }

        protected Node(string address, double latitude, double longitude)
        {
            Address = address;
            Latitude = latitude;
            Longitude = longitude;
        }

        public static Node Create(string address, double latitude, double longitude)
            => new Node(address, latitude, longitude);
    }
}