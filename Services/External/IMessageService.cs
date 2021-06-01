using System.Threading.Tasks;

namespace Services.External
{
    public interface IMessageService
    {
        Task SendEmailAsync(
            string fromDisplayName, string fromEmailAddress, string subject, string message
        );
    }
}
