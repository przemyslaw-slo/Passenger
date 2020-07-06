using System.Collections.Generic;
using System.Threading.Tasks;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services
{
    public interface IVehicleProvider : IService
    {
        Task<IEnumerable<VehicleDto>> GetAllAsync();
        Task<VehicleDto> GetAsync(string brand, string name);
    }
}
