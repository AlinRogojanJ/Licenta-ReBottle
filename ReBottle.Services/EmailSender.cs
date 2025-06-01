using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ReBottle.Services
{
    public class EmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var emailConfig = _configuration.GetSection("EmailConfiguration");

            using var client = new SmtpClient
            {
                Host = emailConfig["SmtpServer"],
                Port = 587, // Gmail STARTTLS port
                EnableSsl = false, // Not SSL, use TLS
                Credentials = new NetworkCredential(emailConfig["Username"], emailConfig["Password"]),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };

            client.EnableSsl = true; // Enable STARTTLS


            var mailMessage = new MailMessage
            {
                From = new MailAddress(emailConfig["From"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(toEmail);

            await client.SendMailAsync(mailMessage);
        }
    }

}
