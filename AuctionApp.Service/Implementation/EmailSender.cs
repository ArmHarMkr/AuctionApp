using AuctionApp.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AuctionApp.Service.Implementation
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var mail = "EMAIL@MAIL.COM"; //change email
            var pw = "Test1234*$"; //Change password

            var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pw)
            };

            return client.SendMailAsync(
                    new MailMessage(
                        from: mail,
                        to: email,
                        subject,
                        message
                        ));

        }
    }
}
