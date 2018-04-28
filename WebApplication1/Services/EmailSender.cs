using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WebApplication1.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com",587)
            {
                Credentials = new System.Net.NetworkCredential("confirmation4helpotron@gmail.com", "Aston123"),
                EnableSsl = true
            };
            MailMessage mail = new MailMessage
            {
                //Setting From , To and CC
                From = new MailAddress("confirmation4helpotron@gmail.com"),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };
            mail.To.Add(new MailAddress(email));
            return smtpClient.SendMailAsync(mail);
        }
    }
}
