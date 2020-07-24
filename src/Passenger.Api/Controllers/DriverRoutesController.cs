using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Passenger.Infrastructure.Commands;
using Passenger.Infrastructure.Commands.Drivers;

namespace Passenger.Api.Controllers
{
    [Route("drivers/routes")]
    public class DriverRoutesController : ApiControllerBase
    {

        public DriverRoutesController(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateDriverRoute command)
        {
            await DispatchAsync(command);

            return NoContent();
        }

        [HttpDelete("{name}")]
        public async Task<IActionResult> Delete(string name)
        {
            var command = new DeleteDriverRoute()
            {
                Name = name
            };
            await DispatchAsync(command);

            return NoContent();
        }
    }
}
