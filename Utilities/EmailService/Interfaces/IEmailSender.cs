using RestSharp;

namespace Utilities.EmailService.Interface
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string subject
            , string htmlMessage
            , IEnumerable<string> toAddresses
            , IEnumerable<string> ccAddresses = null
            , IEnumerable<string> bccAddresses = null
        );
    }
}
