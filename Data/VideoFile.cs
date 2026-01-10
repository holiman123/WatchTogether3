using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WatchTogether3.Data.OrderedList;

namespace WatchTogether3.Data;

public class VideoFile : OrderableItem
{
    [Key]
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public string FriendlyName { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public DateTime UploadDate { get; set; }
    public Room Room { get; set; }


    public override bool Equals(object? obj)
    {
        return obj is VideoFile file &&
               Id == file.Id;
    }

    public static bool operator ==(VideoFile? left, VideoFile? right)
    {
        return EqualityComparer<VideoFile>.Default.Equals(left, right);
    }

    public static bool operator !=(VideoFile? left, VideoFile? right)
    {
        return !EqualityComparer<VideoFile>.Default.Equals(left, right);
    }
}
