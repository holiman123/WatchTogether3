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
            //builder.Entity<Room>()
            //    .HasOne<ApplicationUser>(r => r.Owner)
            //    .WithMany(u => u.Rooms);

            builder.Entity<ApplicationUser>()
                .HasMany<Room>(u => u.Rooms)
                .WithOne(r => r.Owner);

            builder.Entity<Room>()
                .HasMany<VideoFile>(r => r.UploadedVideos)
                .WithOne(v => v.Room)
                .OnDelete(DeleteBehavior.Cascade);

            //builder.Entity<Room>()
            //    .HasMany<ApplicationUser>(r => r.AllowedToEnterUsers)
            //    .WithMany()
            //    .UsingEntity<Dictionary<string, object>>(
            //        "ApplicationUserRoom",
            //        j => j.HasOne<ApplicationUser>().WithMany().HasForeignKey("user"),
            //        j => j.HasOne<Room>().WithMany().HasForeignKey("room"));
            builder.Entity<Room>()
                .HasMany<ApplicationUser>(r => r.AllowedToEnterUsers)
                .WithMany();

            builder.Entity<Room>()
                .HasMany<ApplicationUser>(r => r.AllowedToControlUsers)
                .WithMany();


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
