namespace Cornplex.Service.Implementation
{
    using System;
    using System.Threading.Tasks;
    using Cornplex.Domain.Settings;
    using Cornplex.Service.Contract;
    using MailKit.Security;
    using Microsoft.Extensions.Options;
    using MimeKit;
    using MailKit.Net.Smtp;

    public class MailService : IEmailService
    {
        public MailSettings _mailSettings { get; }

        public MailService(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            try
            {
                // create message
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(mailRequest.From ?? _mailSettings.EmailFrom);
                email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
                email.Subject = mailRequest.Subject;
                var builder = new BodyBuilder();
                builder.HtmlBody = mailRequest.Body;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.SmtpHost, _mailSettings.SmtpPort, SecureSocketOptions.StartTls);

                // TODO: https://myaccount.google.com/security - set "Less secure app access" - ON
                smtp.Authenticate(_mailSettings.SmtpUserName, _mailSettings.SmtpPassword);

                await smtp.SendAsync(email);
                smtp.Disconnect(true);

            }
            catch (Exception ex)
            {
                //throw new ApiException(ex.Message);
                throw new Exception(ex.Message);
            }
        }

    }
}