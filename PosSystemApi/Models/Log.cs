using System.ComponentModel.DataAnnotations;

namespace PosSystemApi.Models
{
    public class Log
    {
        [Key]
        public int LogId { get; set; }

        [Required]
        [MaxLength(10)]
        public string Method { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string Path { get; set; } = string.Empty;

        public int StatusCode { get; set; }

        [MaxLength(150)]
        public string? UserName { get; set; }

        public long DurationMs { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}