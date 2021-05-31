using System.Threading.Tasks;

namespace Services.External
{
    public interface IMessageService
    {
        Task SendEmailAsync(
            string fromDisplayName,
            string fromEmailAddress,
            string toName,
            string toEmailAddress,
            string subject,
            string message
        );
    }
}
