using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class ContactInfo
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [EmailAddress]
        [DisplayName("Emails will be sent to this address")]
        public string ReceiverEmail { get; set; }
    }
}
