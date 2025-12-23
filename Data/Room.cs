using System.ComponentModel.DataAnnotations;

namespace WatchTogether3.Data
{
    public class Room
    {
        [Key]
        public string Name { get; set; } = string.Empty;

        public string VideoUrl { get; set; } = string.Empty;

        public List<string> UploadedVideoUrls { get; set; } = new List<string>();

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
    }
}
