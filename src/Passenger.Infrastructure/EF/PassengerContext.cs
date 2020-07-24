using Microsoft.EntityFrameworkCore;
using Passenger.Core.Domain;

namespace Passenger.Infrastructure.EF
{
    public class PassengerContext : DbContext
    {
        private readonly SqlSettings _sqlSettings;
        public DbSet<User> Users { get; set; }

        public PassengerContext(DbContextOptions options, SqlSettings sqlSettings) : base(options)
        {
            _sqlSettings = sqlSettings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_sqlSettings.InMemory)
            {
                optionsBuilder.UseInMemoryDatabase("PassengerInMemory");

                return;
            }

            optionsBuilder.UseSqlServer(_sqlSettings.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var itemBuilder = modelBuilder.Entity<User>();
            itemBuilder.HasKey(x => x.Id);
        }
    }
}
