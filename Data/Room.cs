using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WatchTogether3.Data.OrderedList;

namespace WatchTogether3.Data;

public enum PrivacyLevel
{
    Public,
    Password,
    Friends,
    PrivateList,
    Owner
}

public class Room
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public VideoFile? CurrentVideo { get; set; }
    public OrderedList<VideoFile> UploadedVideos { get; set; } = new OrderedList<VideoFile>();
    public ApplicationUser Owner { get; set; }
    public DateTime CreationDate { get; set; }

    public PrivacyLevel EnterRoomPrivacyLevel { get; set; }
    public PrivacyLevel ControlPlayerPrivacyLevel { get; set; }
    public PrivacyLevel ControlVideoPrivacyLevel { get; set; }

    public List<ApplicationUser> Participants { get; set; } = new List<ApplicationUser>();

    public List<ApplicationUser> AllowedToEnterUsers { get; set; } = new List<ApplicationUser>();
    public List<ApplicationUser> AllowedToControlUsers { get; set; } = new List<ApplicationUser>();

    public string HashedPassword { get; set; } = string.Empty;

    public override bool Equals(object? obj)
    {
        return obj is Room room &&
               Id == room.Id;
    }

    public static bool operator ==(Room? left, Room? right)
    {
        return EqualityComparer<Room>.Default.Equals(left, right);
    }

    public static bool operator !=(Room? left, Room? right)
    {
        return !(left == right);
    }
}
