using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WatchTogether3.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Room>()
                .HasOne<ApplicationUser>(r => r.Owner)
                .WithMany(u => u.Rooms);

            //builder.Entity<ApplicationUser>()
            //    .HasMany<Room>(u => u.Rooms)
            //    .WithOne(r => r.Owner);

            builder.Entity<Room>().Navigation(e => e.Owner).AutoInclude();

            base.OnModelCreating(builder);
        }
    }
}
