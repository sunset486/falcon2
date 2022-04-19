using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using falcon2.Core.Models;
using falcon2.Core.Models.Auth;
using falcon2.Data.Configurations;

namespace falcon2.Data
{
    public class SuperDbContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<SuperHero> SuperHeroes { get; set; }
        public DbSet<SuperPower> SuperPowers { get; set; }

        public SuperDbContext(DbContextOptions<SuperDbContext> options)
            :base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new SuperHeroConfiguration());
            builder.ApplyConfiguration(new SuperPowerConfiguration());
        }
    }
}
