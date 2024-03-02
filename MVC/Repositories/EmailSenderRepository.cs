using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Web.CodeGeneration.Utils.Messaging;
using System.Net.Mail;
using System.Net;
using MVC.Models;
using System.Threading.Tasks;

namespace MVC.Repositories
{
    public class EmailSenderRepository : IEmailSender
    {
        private readonly MessageSender _messageSender;

        public EmailSenderRepository(IOptions<MessageSender> messageSender)
        {
            _messageSender = messageSender.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var fromemail = _messageSender.Email;
            var frompass = _messageSender.Password;
            var message = new MailMessage();
            message.From = new MailAddress(fromemail);
            message.Subject = subject;
            message.Body = $"<html><body>{htmlMessage}</body></html>";
            message.To.Add(email);
            message.IsBodyHtml = true;
            var smtpclient = new SmtpClient("smtp-relay.brevo.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromemail, frompass),
                EnableSsl = true
            };
            smtpclient.Send(message);

        }

    }
}
