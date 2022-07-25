using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;
using falcon2.Core.Models.EmailService;
using falcon2.Core.Services;

namespace falcon2.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _settings;
        
        public EmailService(IOptions<SmtpSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<string> SendEmailAsync(string receiverEmail, string receiverFirstName, string Link)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_settings.SenderEmail));
            message.To.Add(MailboxAddress.Parse(receiverEmail));
            message.Subject = "Hello from Falcon!";
            message.Body = new TextPart("plain")
            {
                Text = "I am the Mighty Falcon, pleased to meet you!"
            };

            var client = new SmtpClient();

            try
            {
                await client.ConnectAsync(_settings.Server, _settings.Port, true);
                await client.AuthenticateAsync(new NetworkCredential(_settings.SenderEmail, _settings.Password));
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
                return "Email sent with success!";
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {
                client.Dispose();
            }
        }
    }
}
