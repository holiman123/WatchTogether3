using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WatchTogether3.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<VideoFile> Videos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Room>()
                .HasKey(r => r.Id);

            builder.Entity<Room>()
                .HasOne<ApplicationUser>(r => r.Owner)
                .WithMany(u => u.Rooms);

            //builder.Entity<Room>()
            //    .HasOne<VideoFile>(r => r.CurrentVideo)
            //    .WithOne(v => v.Room).HasForeignKey(nameof(Room), "CurrentVideoUrl");

            builder.Entity<Room>()
                .HasMany<VideoFile>(r => r.UploadedVideos)
                .WithOne(v => v.Room).OnDelete(DeleteBehavior.NoAction);


            builder.Entity<Room>().Navigation(e => e.Owner).AutoInclude();

            builder.Entity<Room>().Navigation(e => e.CurrentVideo).AutoInclude();

            builder.Entity<Room>().Navigation(e => e.UploadedVideos).AutoInclude();

            base.OnModelCreating(builder);
        }
    }
}
