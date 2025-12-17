using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WatchTogether3.Data
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public List<Room> Rooms { get; set; }
    }

}
