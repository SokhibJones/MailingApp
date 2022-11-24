using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace MailingApp.Models.Entities
{
    public class Message
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [Required]
        public User From { get; set; }

        [Required]
        public User To { get; set; }

        [MaxLength(500)]
        public string? Subject { get; set; }

        public string Body { get; set; } = string.Empty;

        public DateTimeOffset SentTime { get; set; } = DateTimeOffset.Now;
    }
}