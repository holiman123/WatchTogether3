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
                .HasKey(r => r.Id);

            builder.Entity<Room>()
                .HasOne<ApplicationUser>(r => r.Owner)
                .WithMany(u => u.Rooms);

            builder.Entity<Room>()
                .HasMany<VideoFile>(r => r.UploadedVideos)
                .WithOne(v => v.Room).OnDelete(DeleteBehavior.Cascade); // TODO: Remove OnDelete and check

            //builder.Entity<ApplicationUser>()
            //    .HasMany(u => u.Friendships)
            //    .WithOne(fr => fr.Me)
            //    .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Friendship>()
                .HasOne(fr => fr.Me)
                .WithMany(u => u.Friendships)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Friendship>()
                .HasOne(fr => fr.Friend)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            //builder.Entity<Friendship>()
            //    .HasOne(fr => fr.FriendshipOfFriend)
            //    .WithOne()
            //    .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Room>().Navigation(e => e.Owner).AutoInclude();

            builder.Entity<Room>().Navigation(e => e.CurrentVideo).AutoInclude();

            builder.Entity<Room>().Navigation(e => e.UploadedVideos).AutoInclude();

            //builder.Entity<ApplicationUser>().Navigation(e => e.Friendships).AutoInclude();

            builder.Entity<Friendship>().Navigation(f => f.Friend).AutoInclude();

            base.OnModelCreating(builder);
        }
    }
}
