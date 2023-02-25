using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using RestSharp;
using RestSharp.Authenticators;
using Utilities.EmailService.DTOs;
using Utilities.EmailService.Interface;

namespace Utilities.EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly MailgunSetting _mailgunSettings;
        private readonly MailkitSetting _mailkitSettings;

        public EmailSender(IOptions<MailgunSetting> mailgunSettingDTO
            , IOptions<MailkitSetting> mailkitSettings)
        {
            _mailgunSettings = mailgunSettingDTO.Value;
            _mailkitSettings = mailkitSettings.Value;
        }

        public async Task SendEmailAsync(string subject
            , string htmlMessage
            , IEnumerable<string> toAddresses
            , IEnumerable<string> ccAddresses = null
            , IEnumerable<string> bccAddresses = null)
        {
            if (_mailgunSettings.Enable)
                await SendByMailgunAsync(subject
                    , htmlMessage
                    , toAddresses
                    , ccAddresses
                    , bccAddresses);
            else
                await SendByMailkitAsync(subject
                        , htmlMessage
                        , toAddresses
                        , ccAddresses
                        , bccAddresses);
        }

        private async Task SendByMailkitAsync(string subject
            , string htmlMessage
            , IEnumerable<string> toAddresses
            , IEnumerable<string> ccAddresses = null
            , IEnumerable<string> bccAddresses = null)
        {
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(MailboxAddress.Parse(_mailkitSettings.From));

            foreach (var toAddress in toAddresses)
                mailMessage.To.Add(MailboxAddress.Parse(toAddress));

            if (ccAddresses != null)
            {
                foreach (var ccAddress in ccAddresses)
                    mailMessage.Cc.Add(MailboxAddress.Parse(ccAddress));
            }

            if (bccAddresses != null)
            {
                foreach (var bccAddress in bccAddresses)
                    mailMessage.Bcc.Add(MailboxAddress.Parse(bccAddress));
            }

            mailMessage.Subject = subject;
            mailMessage.Body = new TextPart(TextFormat.Html) { Text = htmlMessage };

            using (var smtpClient = new SmtpClient())
            {
                try
                {
                    await smtpClient.ConnectAsync(_mailkitSettings.Host, _mailkitSettings.Port, SecureSocketOptions.StartTls);
                    await smtpClient.AuthenticateAsync(_mailkitSettings.User, _mailkitSettings.Password);
                    await smtpClient.SendAsync(mailMessage);
                    await smtpClient.DisconnectAsync(true);
                }
                catch (Exception e)
                {

                    throw;
                }
            }
        }

        private async Task<RestResponse> SendByMailgunAsync(string subject
            , string htmlMessage
            , IEnumerable<string> toAddresses
            , IEnumerable<string> ccAddresses = null
            , IEnumerable<string> bccAddresses = null)
        {
            var client = GetRestClient();
            var request = GetRestRequest(subject, htmlMessage);

            foreach (var toAddress in toAddresses)
                request.AddParameter("to", toAddress);

            if (ccAddresses != null)
            {
                foreach (var ccAddress in ccAddresses)
                    request.AddParameter("cc", ccAddress);
            }

            if (bccAddresses != null)
            {
                foreach (var bccAddress in bccAddresses)
                    request.AddParameter("bcc", bccAddress);
            }

            var result = await client.ExecuteAsync(request);
            return result;
        }

        private RestClient GetRestClient()
        {
            RestClient client = new RestClient(new Uri(_mailgunSettings.APIUrl));
            client.Authenticator = new HttpBasicAuthenticator("api", _mailgunSettings.APIKey);

            return client;
        }

        private RestRequest GetRestRequest(string subject, string htmlMessage)
        {
            RestRequest request = new RestRequest();
            request.AddParameter("domain", _mailgunSettings.Domain, ParameterType.UrlSegment);
            request.AddParameter("from", _mailgunSettings.From);
            request.AddParameter("subject", subject);
            request.AddParameter("html", htmlMessage);
            request.Resource = "{domain}/messages";
            request.Method = Method.Post;

            return request;
        }
    }
}
