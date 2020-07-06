using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Passenger.Infrastructure.DTO;

namespace Passenger.Infrastructure.Services
{
    public class VehicleProvider : IVehicleProvider
    {
        private readonly IMemoryCache _cache;
        private static readonly string CacheKey = "vehicles";
        private static readonly Dictionary<string, IEnumerable<VehicleDetails>> AvailableVehicles = new Dictionary<string, IEnumerable<VehicleDetails>>
        {
            ["Audi"] = new List<VehicleDetails>
            {
                new VehicleDetails("RS7", 5),
                new VehicleDetails("RS8", 5)
            },
            ["BMW"] = new List<VehicleDetails>
            {
                new VehicleDetails("i8", 5),
                new VehicleDetails("E36", 5)
            },
            ["Ford"] = new List<VehicleDetails>
            {
                new VehicleDetails("Fiesta", 5),
                new VehicleDetails("Focus", 5)
            },
            ["Mitsubishi"] = new List<VehicleDetails>
            {
                new VehicleDetails("Colt", 5),
                new VehicleDetails("Lancer", 5)
            },
            ["Skoda"] = new List<VehicleDetails>
            {
                new VehicleDetails("Fabia", 5),
                new VehicleDetails("Rapid", 5)
            },
            ["Volkswagen"] = new List<VehicleDetails>
            {
                new VehicleDetails("Passat", 5)
            }
        };

        public VehicleProvider(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<IEnumerable<VehicleDto>> GetAllAsync()
        {
            var vehicles = _cache.Get<IEnumerable<VehicleDto>>(CacheKey);
            if (vehicles == null)
            {
                vehicles = await GetAllFromDbAsync();
                _cache.Set(CacheKey, vehicles);
            }

            return vehicles;
        }

        public async Task<VehicleDto> GetAsync(string brand, string name)
        {
            if (!AvailableVehicles.ContainsKey(brand))
            {
                throw new Exception($"Vehicle brand: '{brand}' is not available.");
            }

            var vehicles = AvailableVehicles[brand];
            var vehicle = vehicles.SingleOrDefault(x => x.Name == name);
            if (vehicle == null)
            {
                throw new Exception($"Vehicle: '{name}' for brand: '{brand}' is not available.");
            }

            return await Task.FromResult(new VehicleDto
            {
                Brand = brand,
                Name = vehicle.Name,
                Seats = vehicle.Seats
            });
        }

        private async Task<IEnumerable<VehicleDto>> GetAllFromDbAsync()
        {
            return await Task.FromResult(AvailableVehicles
                .GroupBy(x => x.Key)
                .SelectMany(vehicle => vehicle
                    .SelectMany(vehicleDetails => vehicleDetails.Value
                        .Select(details => new VehicleDto
                        {
                            Brand = vehicle.Key,
                            Name = details.Name,
                            Seats = details.Seats
                        }))));
        }

        private class VehicleDetails
        {
            public string Name { get; private set; }
            public int Seats { get; private set; }

            public VehicleDetails(string name, int seats)
            {
                Name = name;
                Seats = seats;
            }
        }
    }
}
