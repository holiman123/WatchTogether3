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
    }
}
