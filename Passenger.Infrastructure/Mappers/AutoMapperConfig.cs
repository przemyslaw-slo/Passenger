using AutoMapper;
using Passenger.Core.Domain;
using Passenger.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Passenger.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
        {
            var configuration = new MapperConfiguration(cfg =>
             {
                 cfg.CreateMap<User, UserDto>();
                 cfg.CreateMap<Driver, DriverDto>();
             }).CreateMapper();

            return configuration;
        }
    }
}
