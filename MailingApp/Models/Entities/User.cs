using System.ComponentModel.DataAnnotations;

namespace MailingApp.Models.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [Display(Name = "User name")]
        [StringLength(maximumLength: 50, ErrorMessage = "Username is too short or too long", MinimumLength = 1)]
        public string Name { get; set; }
    }
}
