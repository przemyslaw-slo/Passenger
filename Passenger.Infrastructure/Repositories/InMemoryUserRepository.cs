using Passenger.Core.Repositories;
using Passenger.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        private static readonly ISet<User> Users = new HashSet<User>();

        public async Task AddAsync(User user)
        {
            Users.Add(user);
            await Task.CompletedTask;
        }

        public async Task<User> GetAsync(Guid id)
        {
            return await Task.FromResult(Users.SingleOrDefault(x => x.Id == id));
        }

        public async Task<User> GetAsync(string email)
        {
            return await Task.FromResult(Users.SingleOrDefault(x => x.Email == email.ToLowerInvariant()));
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await Task.FromResult(Users);
        }

        public async Task RemoveAsync(Guid id)
        {
            var user = await GetAsync(id);
            Users.Remove(user);
        }

        public async Task UpdateAsync(User user)
        {
            await Task.CompletedTask;
        }
    }
}
