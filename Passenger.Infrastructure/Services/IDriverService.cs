using Passenger.Infrastructure.DTO;
using System;

namespace Passenger.Infrastructure.Services
{
    public interface IDriverService
    {
        DriverDto Get(Guid userId);

    }
}