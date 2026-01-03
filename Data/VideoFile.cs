using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatchTogether3.Data;

public class VideoFile
{
    [Key]
    public string Url { get; set; } = string.Empty;
    public string FriendlyName { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public DateTime UploadDate { get; set; }

    //[ForeignKey("Room")]
    //public string RoomName { get; set; } = string.Empty;
    public Room Room { get; set; }

    //public VideoFile(string friendlyName, string fileName, Room room)
    //{
    //    FriendlyName = friendlyName;
    //    FileName = fileName;
    //    //Room = room;
    //    Path = $"D:\\WatchTogether3_files\\{Room.Name}_{FileName}";
    //    Url = $"api/Videos/get/{Room.Name}_{FileName}";
    //    UploadDate = DateTime.Now;
    //}
}
