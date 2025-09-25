using Microsoft.Extensions.Configuration;
using MimeKit;
using Shop.Core.DTO.AllAccountDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace Shop.Core.Services.EmailServices
{
    
    public class EmailService : IEmailService
    {
        private readonly IConfiguration configuration;
        public EmailService(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }
        public async Task SendEmail(EmailDTO emailDTO)
        {
            
                MimeMessage message = new MimeMessage();

            message.From.Add(new MailboxAddress("shop Site", configuration["EmailSetting:From"]));
            message.Subject = emailDTO.Subject;
            //message.To.Add(new  MailboxAddress(emailDTO.To, emailDTO.To));
            message.To.Add(new  MailboxAddress("my shop", emailDTO.To));
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailDTO.Content
            };

            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    await smtp.ConnectAsync(
                        configuration["EmailSetting:Smtp"],
                       int.Parse(configuration["EmailSetting:Port"]), true);
                    await smtp.AuthenticateAsync(configuration["EmailSetting:Username"],
                        configuration["EmailSetting:Password"]);

                    await smtp.SendAsync(message);
                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    smtp.Disconnect(true);
                    smtp.Dispose();
                }
            }
                //------------------
        }
    }
}
