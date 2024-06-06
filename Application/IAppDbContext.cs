using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IAppDbContext
    {
        public DbSet<Zoo> Zoos { get; set; }

        public DbSet<Animal> Animals { get; set; }

        public DbSet<Location> Locations { get; set; }

        void SaveChanges();
    }
}
