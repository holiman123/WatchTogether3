using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WatchTogether3.Data.OrderedList;

namespace WatchTogether3.Data
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public VideoFile? CurrentVideo { get; set; }
        public OrderedList<VideoFile> UploadedVideos { get; set; } = new OrderedList<VideoFile>();
        public ApplicationUser Owner { get; set; }
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

        /// <summary>
        /// Time of the last play action. <br/>
        /// Used to calculate the current time when the video is playing.
        /// </summary>
        public DateTime LastPlayTime { get; set; }

        public DateTime CreationDate { get; set; }

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
}
