using System;
using System.Collections.Generic;
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
            _logger.LogTrace("Initializing data...");

            var tasks = new List<Task>();
            for (var i = 1; i <= 10; i++)
            {
                var userId = Guid.NewGuid();
                var username = $"user{i}";
                _logger.LogTrace($"Adding user: '{username}'.");
                tasks.Add(_userService.RegisterAsync(userId, $"{username}@email.com", username, "secret", "user"));

                _logger.LogTrace($"Adding driver for '{username}'.");
                tasks.Add(_driverService.CreateAsync(userId));

                _logger.LogTrace($"Setting vehicle for '{username}'.");
                tasks.Add(_driverService.SetVehicleAsync(userId, "Audi", "RS7"));

                _logger.LogTrace($"Adding routes for '{username}'.");
                tasks.Add(_driverRouteService.AddAsync(userId, "Default route",1,1,2,2));
                tasks.Add(_driverRouteService.AddAsync(userId, "Job route", 3, 4, 7, 8));
            }
            await Task.WhenAll(tasks);

            for (var i = 1; i <= 3; i++)
            {
                var userId = Guid.NewGuid();
                var username = $"admin{i}";
                tasks.Add(_userService.RegisterAsync(userId, $"{username}@email.com", username, "secret", "admin"));
                _logger.LogTrace($"Adding admin: '{username}'.");
            }
            await Task.WhenAll(tasks);

            _logger.LogTrace("Data was initialized.");
        }
    }
}