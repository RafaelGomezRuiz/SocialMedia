using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SocialMedia.Core.Application.Dtos.Email;
using SocialMedia.Core.Domain.Settings;
using StockApp.Core.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infraestructure.Shared.Services
{
    public class EmailService : IEmailService
    {
        private MailSettings _mailSettings { get; }
        public EmailService(IOptions<MailSettings> _mailSettings)
        {
            this._mailSettings = _mailSettings.Value;
        }
        public async Task SendAsync(EmailRequest request)
        {
            try
            {
                MimeMessage email= new();
                email.Sender = MailboxAddress.Parse($"{_mailSettings.DisplayName} <{_mailSettings.EmailFrom}>");
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject= request.Subject;
                BodyBuilder builder = new();
                builder.HtmlBody = request.Body;
                email.Body=builder.ToMessageBody();

                using SmtpClient smtp = new();
                smtp.Connect(_mailSettings.SmtpHost,_mailSettings.SmtpPort,SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.SmtpUser, _mailSettings.SmtpPass);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }
            catch (Exception ex) { }
            throw new NotImplementedException();
        }
    }
}
