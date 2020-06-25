using Passenger.Infrastructure.DTO;
using System;
using System.Threading.Tasks;
using Passenger.Infrastructure.Commands.Drivers;

namespace Passenger.Infrastructure.Services
{
    public interface IDriverService : IService
    {
        Task<DriverDto> GetAsync(Guid userId);
        Task CreateAsync(Guid userId, CreateDriver.DriverVehicle vehicle);
    }
}