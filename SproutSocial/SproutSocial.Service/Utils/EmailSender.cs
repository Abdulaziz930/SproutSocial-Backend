using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SproutSocial.Service.Utils
{
    public static class EmailSender
    {
        public static async Task SendEmailAsync(string? email, string body, string subject)
        {
            using (MailMessage mail = new MailMessage())
            {
                if (email == null)
                    throw new ArgumentNullException("Email cannot be null");

                mail.From = new MailAddress(Constants.EmailAddress);
                mail.To.Add(email);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.Normal;

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.UseDefaultCredentials = false;
                    NetworkCredential network = new NetworkCredential(Constants.EmailAddress, Constants.EmailPassword);
                    smtp.Credentials = network;
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Port = 587;

                    try
                    {
                        await smtp.SendMailAsync(mail);
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                }
            }
        }
}}
