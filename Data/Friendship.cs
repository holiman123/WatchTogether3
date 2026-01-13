using System.ComponentModel.DataAnnotations;

namespace WatchTogether3.Data;

public class Friendship
{
    [Key]
    public int Id { get; set; }
    //public Friendship? FriendshipOfFriend { get; set; }
    public ApplicationUser? Me { get; set; }
    public ApplicationUser? Friend { get; set; }
    public FriendshipStatus Status { get; set; }
    public DateTime CreationDate { get; set; }
}

public enum FriendshipStatus
{
    Requested,
    Accepted,
    Sent
}
