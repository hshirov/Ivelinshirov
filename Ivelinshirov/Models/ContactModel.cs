using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ivelinshirov.Models
{
    public class ContactModel
    {
        [DisplayName("Your Name")]
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(64, ErrorMessage = "This name is too long.")]
        [MinLength(2, ErrorMessage = "This name is too short.")]
        public string Name { get; set; }
        [DisplayName("Your Email")]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }
        [DisplayName("Subject")]
        [Required(ErrorMessage = "Subject is required.")]
        [MaxLength(72, ErrorMessage = "This subject is too long.")]
        [MinLength(5, ErrorMessage = "This subject is too short.")]
        public string Subject { get; set; }
        [DisplayName("Content")]
        [Required(ErrorMessage = "Content is required.")]
        [MinLength(5, ErrorMessage = "This message is too short.")]
        public string Content { get; set; }
    }
}
