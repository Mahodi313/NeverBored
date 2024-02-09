using System.ComponentModel.DataAnnotations;

namespace NeverBored.Models
{
    public class ActivityModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Type { get; set; }
        public int Participants { get; set; }
        public string? Link { get; set; }       
    }
}
