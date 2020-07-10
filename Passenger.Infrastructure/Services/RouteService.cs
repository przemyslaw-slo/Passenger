using System;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public class RouteService : IRouteService
    {
        private static readonly Random Random = new Random();
        public async Task<string> GetAddressAsync(double latitude, double longitude)
        {
            return await Task.FromResult($"Sample address {Random.Next(100)}.");
        }

        public double CalculateDistance(double startLatitude, double startLongitude, double endLatitude, double endLongitude)
        {
            return Random.Next(500, 1000);
        }
    }
}