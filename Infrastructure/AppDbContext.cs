using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Infrastructure
{

    public class AppDbContext : IdentityDbContext, IAppDbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Zoo> Zoos { get; set; }

        public DbSet<Animal> Animals { get; set; }

        public DbSet<Location> Locations { get; set; }

        void IAppDbContext.SaveChanges()
        {
            this.SaveChanges();
        }
    }
}