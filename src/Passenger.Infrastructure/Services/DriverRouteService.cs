using System;
using System.Threading.Tasks;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;
using Passenger.Infrastructure.Extensions;

namespace Passenger.Infrastructure.Services
{
    public class DriverRouteService : IDriverRouteService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IRouteService _routeService;


        public DriverRouteService(IDriverRepository driverRepository, IRouteService routeService)
        {
            _driverRepository = driverRepository;
            _routeService = routeService;
        }

        public async Task AddAsync(Guid userId, string name, double startLatitude, double startLongitude,
            double endLatitude, double endLongitude)
        {
            var driver = await _driverRepository.GetOrFailAsync(userId);
            var startAddress = await _routeService.GetAddressAsync(startLatitude, startLongitude);
            var endAddress = await _routeService.GetAddressAsync(endLatitude, endLongitude);
            var start = Node.Create(startAddress, startLatitude, startLongitude);
            var end = Node.Create(endAddress, endLatitude, endLongitude);
            var distance = _routeService.CalculateDistance(startLatitude, startLongitude, endLatitude, endLongitude);
            driver.AddRoute(name, start, end, distance);
            await _driverRepository.UpdateAsync(driver);
        }

        public async Task DeleteAsync(Guid userId, string name)
        {
            var driver = await _driverRepository.GetOrFailAsync(userId);
            driver.DeleteRoute(name);
            await _driverRepository.UpdateAsync(driver);
        }
    }
}
