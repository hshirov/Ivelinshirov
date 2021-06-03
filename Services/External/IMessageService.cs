using System.Threading.Tasks;

namespace Services.External
{
    public interface IMessageService
    {
        Task SendEmailToAdminAsync(string fromDisplayName, string fromEmailAddress, string subject, string message);
        Task SendEmailAsync(string emailAddress, string subject, string htmlMessage);
    }
}
