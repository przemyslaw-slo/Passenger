using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using Passenger.Infrastructure.Commands.Users;
using Passenger.Infrastructure.DTO;

namespace Passenger.Tests.EndToEnd.Controllers
{
    public class UsersControllerTests : ControllerTestsBase
    {
        [Test]
        public async Task GivenValidEmail_UserShouldExists()
        {
            // Arrange
            const string email = "user1@email.com";

            // Act
            var user = await GetUserAsync(email);

            // Assert
            user.Email.Should().BeEquivalentTo(email);
        }

        [Test]
        public async Task GivenInvalidEmail_UserShouldNotExists()
        {
            // Arrange
            const string email = "invalid@email.com";

            // Act
            var response = await Client.GetAsync($"users/{email}");

            // Assert
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.NotFound);
        }

        [Test]
        public async Task GivenUniqueEmail_UserShouldBeCreated()
        {
            // Arrange
            var command = new CreateUser()
            {
                Email = "user0@email.com",
                Password = "secret",
                Username = "user0"
            };
            var payload = GetPayload(command);

            // Act 
            var response = await Client.PostAsync("users", payload);
            var user = await GetUserAsync(command.Email);

            // Assert
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.Created);
            response.Headers.Location.ToString().Should().BeEquivalentTo($"users/{command.Email}");
            user.Email.Should().BeEquivalentTo(command.Email);
        }

        private async Task<UserDto> GetUserAsync(string email)
        {
            var response = await Client.GetAsync($"users/{email}");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserDto>(responseString);
        }
    }
}
