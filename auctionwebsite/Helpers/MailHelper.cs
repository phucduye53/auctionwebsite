using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Net.Mail;
using System.Net;

namespace auctionwebsite.Helpers
{
    public class MailHelper
    {
        public static void SendMail(string toEmail,string subject,string content)
        {
            var fromEmail = "auctionwebsiteckus@gmail.com";
            var fromEmailPassword = "0919061624";
            var smtpHost = "smtp.gmail.com";
            string body = content;
            MailMessage message = new MailMessage(fromEmail,toEmail);
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = body;

            var client = new SmtpClient();
            client.Credentials = new NetworkCredential(fromEmail, fromEmailPassword);
            client.Host = smtpHost;
            client.EnableSsl = true;
            client.Port = 587;
            client.Send(message);
        }
    }
}