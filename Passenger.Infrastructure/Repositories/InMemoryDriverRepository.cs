using System;
using System.Collections.Generic;
using System.Linq;
using Passenger.Core.Domain;
using Passenger.Core.Repositories;

namespace Passenger.Infrastructure.Repositories
{
    public class InMemoryDriverRepository : IDriverRepository
    {
        private static ISet<Driver> _drivers = new HashSet<Driver>();

        public void Add(Driver driver)
        {
            _drivers.Add(driver);
        }

        public Driver Get(Guid userId)
        {
            return _drivers.Single(x => x.UserId == userId);
        }

        public IEnumerable<Driver> GetAll()
        {
            return _drivers;
        }

        public void Update(Driver driver)
        {
        }
    }
}