using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using OganiApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using OganiApp.Service.Services.Interface;

namespace OganiApp.Service.Services
{
    public class MessageSend : IMessageSend
    {
        public void MimeKitConfrim(AppUser appUser, string url, string token)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("Ashion", "projectogani@gmail.com"));

            message.To.Add(new MailboxAddress(appUser.UserName, appUser.Email));

            message.Subject = "Confirm Email";

            string emailbody = string.Empty;

            using (StreamReader streamReader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Account/Templates", "Confirm.html")))
            {
                emailbody = streamReader.ReadToEnd();
            }

            emailbody = emailbody.Replace("{{username}}", $"{appUser.UserName}").Replace("{{code}}", $"{url}");

            message.Body = new TextPart(TextFormat.Html) { Text = emailbody };

            using var smtp = new SmtpClient();

            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("projectogani@gmail.com", "cjywfdxcacwbtixw");
            smtp.Send(message);
            smtp.Disconnect(true);
        }


        public void MimeMessageResetPassword(AppUser user, string url, string code)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("Ashion", "projectogani@gmail.com"));

            message.To.Add(new MailboxAddress(user.UserName, user.Email));

            message.Subject = "Reset Password";

            string emailbody = string.Empty;

            using (StreamReader streamReader = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Account/Templates", "Confirm.html")))
            {
                emailbody = streamReader.ReadToEnd();
            }

            emailbody = emailbody.Replace("{{username}}", $"{user.UserName}").Replace("{{code}}", $"{url}");

            message.Body = new TextPart(TextFormat.Html) { Text = emailbody };

            using var smtp = new SmtpClient();

            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("projectogani@gmail.com", "cjywfdxcacwbtixw");
            smtp.Send(message);
            smtp.Disconnect(true);
        }
    }
}
