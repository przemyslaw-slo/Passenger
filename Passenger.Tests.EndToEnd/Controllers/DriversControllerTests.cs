using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using Passenger.Infrastructure.Commands.Drivers;
using Passenger.Infrastructure.DTO;

namespace Passenger.Tests.EndToEnd.Controllers
{
    class DriversControllerTests : ControllerTestsBase
    {
        [Test]
        public async Task GivenValidUserId_DriverShouldBeCreated()
        {
            // Arrange
            const string email = "user1@gmail.com";
            var user = await GetUserAsync(email);
            var commandVehicle = new CreateDriver.DriverVehicle()
            {
                Brand = "Audi",
                Name = "RS3",
                Seats = 3
            };
            var command = new CreateDriver()
            {
                UserId = user.Id,
                Vehicle = commandVehicle
            };
            var payload = GetPayload(command);

            // Act 
            var response = await Client.PostAsync("drivers", payload);
            var driver = await GetDriverAsync(command.UserId);

            // Assert
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.Created);
            response.Headers.Location.ToString().Should().BeEquivalentTo($"drivers/{command.UserId}");
            driver.UserId.Should().Be(command.UserId);
            driver.Vehicle.Brand.Should().BeEquivalentTo(command.Vehicle.Brand);
            driver.Vehicle.Name.Should().BeEquivalentTo(command.Vehicle.Name);
            driver.Vehicle.Seats.Should().Be(command.Vehicle.Seats);
        }

        private async Task<UserDto> GetUserAsync(string email)
        {
            var response = await Client.GetAsync($"users/{email}");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserDto>(responseString);
        }

        private async Task<DriverDto> GetDriverAsync(Guid userId)
        {
            var response = await Client.GetAsync($"drivers/{userId}");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<DriverDto>(responseString);
        }
    }
}
