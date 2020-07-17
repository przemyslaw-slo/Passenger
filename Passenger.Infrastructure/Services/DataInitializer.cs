using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Passenger.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IUserService _userService;
        private readonly IDriverService _driverService;
        private readonly IDriverRouteService _driverRouteService;
        private readonly ILogger<DataInitializer> _logger;

        public DataInitializer(IUserService userService, IDriverService driverService, IDriverRouteService driverRouteService, ILogger<DataInitializer> logger)
        {
            _userService = userService;
            _driverService = driverService;
            _driverRouteService = driverRouteService;
            _logger = logger;
        }
        public async Task SeedAsync()
        {
            var users = await _userService.GetAllAsync();
            if (users.Any())
            {
                return;
            }

            _logger.LogTrace("Initializing data...");

            for (var i = 1; i <= 10; i++)
            {
                var userId = Guid.NewGuid();
                var username = $"user{i}";
                _logger.LogTrace($"Adding user: '{username}'.");
                await _userService.RegisterAsync(userId, $"{username}@email.com", username, "secret", "user");

                _logger.LogTrace($"Adding driver for '{username}'.");
                await _driverService.CreateAsync(userId);

                _logger.LogTrace($"Setting vehicle for '{username}'.");
                await _driverService.SetVehicleAsync(userId, "Audi", "RS7");

                _logger.LogTrace($"Adding routes for '{username}'.");
                await _driverRouteService.AddAsync(userId, "Default route",1,1,2,2);
                await _driverRouteService.AddAsync(userId, "Job route", 3, 4, 7, 8);
            }

            for (var i = 1; i <= 3; i++)
            {
                var userId = Guid.NewGuid();
                var username = $"admin{i}";
                await _userService.RegisterAsync(userId, $"{username}@email.com", username, "secret", "admin");
                _logger.LogTrace($"Adding admin: '{username}'.");
            }

            _logger.LogTrace("Data was initialized.");
        }
    }
}