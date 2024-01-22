﻿using Microsoft.Extensions.Options;
using System.CodeDom;
using System.Text;
using TechQwerty.BookStore.Models;

namespace TechQwerty.BookStore.Service
{
    public class EmailService : IEmailService
    {

        private const string templatePath = @"EmailTemplate/{0}.html";
        private readonly SMTPConfigModel _smtpConfig;

        public EmailService(IOptions<SMTPConfigModel> smtpConfig)
        {
            _smtpConfig = smtpConfig.Value;
        }

        // 1. Email to be sent 
        public async Task SendTestEmail(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceHolders("Hello {{ UserName }},", userEmailOptions.PlaceHolders);
            userEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("TestEmail"), userEmailOptions.PlaceHolders);
            await SendEmail(userEmailOptions);
        }


        private async Task SendEmail(UserEmailOptions userEmailOptions)
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage
            {
                Subject = userEmailOptions.Subject,
                Body = userEmailOptions.Body,
                From = new System.Net.Mail.MailAddress(_smtpConfig.SenderAddress, _smtpConfig.SenderDisplayName),
                IsBodyHtml = _smtpConfig.IsBodyHTML
            };

            foreach (var toEmail in userEmailOptions.ToEmails)
            {
                mail.To.Add(toEmail);
            }

            System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient
            {
                Host = _smtpConfig.Host,
                Port = _smtpConfig.Port,
                EnableSsl = _smtpConfig.EnableSSL,
                Credentials = new System.Net.NetworkCredential(_smtpConfig.UserName, _smtpConfig.Password)
            };


            mail.BodyEncoding = Encoding.Default;

            await smtpClient.SendMailAsync(mail);

            smtpClient.Dispose();
        }

        // get email template from /EmailTemplate
        private string GetEmailBody(string templateName)
        {
            var body = File.ReadAllText(string.Format(templatePath, templateName));
            return body;
        }

        // update email placeholders 
        private string UpdatePlaceHolders(string text, List<KeyValuePair<string, string>> keyValuePairs)
        {
            if (!string.IsNullOrEmpty(text) && keyValuePairs != null)
            {
                foreach (var placeholder in keyValuePairs)
                {
                    if (text.Contains(placeholder.Key))
                    {
                        text = text.Replace(placeholder.Key, placeholder.Value);
                    }
                }
            }
            return text;
        }

    }
}
