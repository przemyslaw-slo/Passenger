using System;
using System.Collections.Generic;
using System.Linq;

namespace Passenger.Core.Domain
{
    public class Driver
    {
        private ISet<Route> _routes = new HashSet<Route>();
        public Guid UserId { get; protected set; }
        public string Name { get; protected set; }
        public Vehicle Vehicle { get; protected set; }

        public IEnumerable<Route> Routes
        {
            get => _routes;
            protected set => _routes = new HashSet<Route>(value);
        }

        public IEnumerable<DailyRoute> DailyRoutes { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        protected Driver()
        {
        }

        public Driver(User user)
        {
            UserId = user.Id;
            Name = user.Username;
        }

        public void SetVehicle(Vehicle vehicle)
        {
            Vehicle = vehicle;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddRoute(string name, Node start, Node end, double distance)
        {
            var route = Routes.SingleOrDefault(x => x.Name == name);
            if (route != null)
            {
                throw new Exception($"Route '{name}' already exists for driver {Name}.");
            }

            _routes.Add(Route.Create(name, start, end, distance));
            UpdatedAt = DateTime.UtcNow;
        }

        public void DeleteRoute(string name)
        {
            var route = Routes.SingleOrDefault(x => x.Name == name);
            if (route == null)
            {
                throw new Exception($"Route '{name}' not found for driver {Name}.");
            }

            _routes.Remove(route);
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
