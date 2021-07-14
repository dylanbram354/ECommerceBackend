using eCommerceStarterCode.Configuration;
using eCommerceStarterCode.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eCommerceStarterCode.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions options)
            :base(options)
        {

        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<ShoppingCartEntry> ShoppingCartEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RolesConfiguration());

            modelBuilder.Entity<ShoppingCartEntry>().HasKey(bc => new { bc.GameId, bc.UserId });
            modelBuilder.Entity<ShoppingCartEntry>()
                .HasOne(bc => bc.Game)
                .WithMany(b => b.ShoppingCartEntries)
                .HasForeignKey(bc => bc.GameId);
            modelBuilder.Entity<ShoppingCartEntry>()
                .HasOne(bc => bc.User)
                .WithMany(c => c.ShoppingCartEntries)
                .HasForeignKey(bc => bc.UserId);
        }

    }
}
