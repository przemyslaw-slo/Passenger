using Passenger.Infrastructure.DTO;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public interface IUserService
    {
        Task<UserDto> GetAsync(string email);
        Task RegisterAsync(string email, string username, string password);
    }
}
