using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatchTogether3.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public List<Room> Rooms { get; set; }

        public List<Friendship> Friendships { get; set; }

        //public List<Friendship> FriendshipsSent { get; set; }

        //public List<Friendship> FriendshipsReceived { get; set; }

        //[NotMapped]
        //public List<Friendship> Friendships => FriendshipsSent
        //    .Concat(FriendshipsReceived)
        //    .Distinct()
        //    .ToList();

        [NotMapped]
        public List<Friendship> PendingFriendRequests => Friendships?
            .FindAll(f => f.Status == FriendshipStatus.Requested);
    }
}
