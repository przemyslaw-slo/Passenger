using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Passenger.Infrastructure.Commands.Users;

namespace Passenger.Tests.EndToEnd.Controllers
{
    public class AccountControllerTests : ControllerTestsBase
    {
        [Test]
        public async Task GivenValidCurrentAndNewPassword_ShouldBeChanged()
        {
            // Arrange
            var command = new ChangeUserPassword()
            {
                CurrentPassword = "secret",
                NewPassword = "secret2"
            };
            var payload = GetPayload(command);

            // Act 
            var response = await Client.PutAsync("account/password", payload);

            // Assert
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.NoContent);
        }
    }
}
