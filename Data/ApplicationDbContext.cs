using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WatchTogether3.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<VideoFile> Videos { get; set; }
        public DbSet<Friendship> Friends { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Room>()
                .HasOne<ApplicationUser>(r => r.Owner)
                .WithMany(u => u.Rooms);

            builder.Entity<Room>()
                .HasMany<VideoFile>(r => r.UploadedVideos)
                .WithOne(v => v.Room)
                .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<Friendship>()
                .HasOne(f => f.Me)
                .WithMany(me => me.Friendships)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Friendship>()
                .HasOne(f => f.Friend)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Room>().Navigation(e => e.Owner).AutoInclude();

            builder.Entity<Room>().Navigation(e => e.CurrentVideo).AutoInclude();

            builder.Entity<Room>().Navigation(e => e.UploadedVideos).AutoInclude();

            builder.Entity<Friendship>().Navigation(f => f.Friend).AutoInclude();

            base.OnModelCreating(builder);
        }
    }
}
