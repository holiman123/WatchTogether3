using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WatchTogether3.Data.OrderedList;

namespace WatchTogether3.Data;

public class VideoFile : OrderableItem
{
    [Key]
    public int Id { get; set; }
    public VideoSourceType SourceType { get; set; }
    public string YouTubeUrl { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string FriendlyName { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public DateTime UploadDate { get; set; }
    public Room Room { get; set; }
    public bool IsPlaying { get; set; }
    public double CurrentTime
    {
        get
        {
            if (IsPlaying)
            {
                double timeSinceLastPlay = (DateTime.Now - LastPlayTime).TotalSeconds;
                return field + timeSinceLastPlay;
            }
            else
            {
                return field;
            }
        }

        set => field = value;
    }

    [NotMapped]
    public string YouTubeId
    { 
        get
        {
            if (String.IsNullOrEmpty(YouTubeUrl))
                return string.Empty;

            return YouTubeUrl.Split('/', '=').Last();
        }
    }

    /// <summary>
    /// Time of the last play action. <br/>
    /// Used to calculate the current time when the video is playing.
    /// </summary>
    public DateTime LastPlayTime { get; set; }


    public void Play(double time)
    {
        if (!IsPlaying)
        {
            IsPlaying = true;
            CurrentTime = time;
            LastPlayTime = DateTime.Now;
        }
    }

    public void Pause(double time)
    {
        if (IsPlaying)
        {
            // Update CurrentTime to the exact time when paused
            CurrentTime = time;
            IsPlaying = false;
        }
    }

    public void Pause()
    {
        Pause(CurrentTime);
    }

    public void Seek(double time)
    {
        CurrentTime = time;
        if (IsPlaying)
        {
            LastPlayTime = DateTime.Now;
        }
    }


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

public enum VideoSourceType
{
    Local,
    YouTube
}