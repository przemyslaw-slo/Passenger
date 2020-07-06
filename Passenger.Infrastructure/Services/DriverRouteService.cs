using System;
using System.Threading.Tasks;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;

namespace Passenger.Infrastructure.Services
{
    public class DriverRouteService : IDriverRouteService
    {
        private readonly IDriverRepository _driverRepository;

        public DriverRouteService(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public async Task AddAsync(Guid userId, string name, double startLatitude, double startLongitude,
            double endLatitude, double endLongitude)
        {
            var driver = await _driverRepository.GetAsync(userId);
            if (driver == null)
            {
                throw new Exception($"Driver {userId} does not exist.");
            }

            var start = Node.Create("Start address", startLongitude, startLatitude);
            var end = Node.Create("End address", endLongitude, endLatitude);
            driver.AddRoute(name, start, end);
            await _driverRepository.UpdateAsync(driver);
        }

        public async Task DeleteAsync(Guid userId, string name)
        {
            var driver = await _driverRepository.GetAsync(userId);
            if (driver == null)
            {
                throw new Exception($"Driver {userId} does not exist.");
            }

            driver.DeleteRoute(name);
            await _driverRepository.UpdateAsync(driver);
        }
    }
}
