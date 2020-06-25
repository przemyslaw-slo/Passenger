using Passenger.Core.Repositories;
using Passenger.Infrastructure.DTO;
using System;
using System.Threading.Tasks;
using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Infrastructure.Commands.Drivers;

namespace Passenger.Infrastructure.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public DriverService(IDriverRepository driverRepository, IUserRepository userRepository, IMapper mapper)
        {
            _driverRepository = driverRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<DriverDto> GetAsync(Guid userId)
        {
            var driver = await _driverRepository.GetAsync(userId);

            var mapper = _mapper.Map<Driver, DriverDto>(driver);
            return mapper;
        }

        public async Task CreateAsync(Guid userId, CreateDriver.DriverVehicle driverVehicle)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user == null)
            {
                throw new Exception($"User {userId} not exists.");
            }

            var driver = await _driverRepository.GetAsync(userId);
            if (driver != null)
            {
                throw new Exception($"User {userId} already is a driver.");
            }

            var vehicle = Vehicle.Create(driverVehicle.Brand, driverVehicle.Name, driverVehicle.Seats);
            driver = new Driver(userId, vehicle);

            await _driverRepository.AddAsync(driver);
        }
    }
}