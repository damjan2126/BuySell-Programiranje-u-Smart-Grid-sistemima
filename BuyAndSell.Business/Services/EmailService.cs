using BuySell.Business.Settings;
using BuySell.Data.Repositories.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using BuySell.Data.Entities;
using BuySell.Data.Enums;
using BuySell.Contracts.Exceptions;
using BuySell.Business.Services.Contracts;

namespace BuySell.Business.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings = new();
        private readonly IEmailRepository _emailRepository;

        public EmailService(IOptions<EmailSettings> emailSettings,
                            IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
            _emailSettings = emailSettings.Value;
        }

        private async Task<MimeMessage> BuildMessage(string from, string to, string subject, string body, System.Net.Mail.Attachment? attachment = null)
        {
            var message = new MimeMessage();

            message.From.Add(MailboxAddress.Parse(from));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;

            var builder = new BodyBuilder();
            builder.TextBody = body;


            if (attachment is not null)
            {
                var mediaType = attachment.ContentType.MediaType.Split('/')[0];
                var mediaSubType = attachment.ContentType.MediaType.Split('/')[1];
                attachment.ContentStream.Position = 0;
                builder.Attachments.Add(attachment.Name, attachment.ContentStream, new MimeKit.ContentType(mediaType, mediaSubType));
            }

            message.Body = builder.ToMessageBody();

            return message;
        }

        public async Task<bool> SendEmail(string to, string subject, string body, Email? email = null, System.Net.Mail.Attachment? attachment = null)
        {


            var emailMessage = await BuildMessage(_emailSettings.UserName, to, subject, body, attachment);

            var timeOutMiliseconds = _emailSettings.EmailTimeout;

            using var smtp = new SmtpClient() { Timeout = timeOutMiliseconds };

            var success = false;

            try
            {
                if (_emailSettings.EnableSsl)
                {
                    await smtp.ConnectAsync(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTlsWhenAvailable);

                }
                else
                {
                    await smtp.ConnectAsync(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.None);
                }

                await smtp.AuthenticateAsync(_emailSettings.UserName, _emailSettings.Password);

                await smtp.SendAsync(emailMessage);

                success = true;

                if (email is not null)
                {
                    await ChangeEmailStateAsync(email.Id, (long)EmailStatus.Sent);
                }
            }
            catch (Exception ex)
            {
                if (email is not null)
                {
                    await ChangeEmailStateAsync(email.Id, (long)EmailStatus.Error, ex.Message);
                }
            }
            finally
            {
                await smtp.DisconnectAsync(true);
            }

            return success;
        }

        public async Task<bool> ChangeEmailStateAsync(long emailId, long statusId, string? message = null)
        {
            var email = await _emailRepository.GetEmailByIdAsync(emailId) ??
                throw new NotFoundException($"Nije pronađen email sa Id={emailId}");

            email.Status = statusId;
            email.Message = message ?? email.Message;
            email.UpdatedAtUtc = DateTime.UtcNow;

            var updated = await _emailRepository.UpdateEmailStateAsync(email);
            return updated;
        }

        public async Task SendEmailAsync(string host)
        {
            var notSentEmails = await _emailRepository.GetNotSentEmailsAsync();

            if (notSentEmails.Count == 0) return;

            foreach (var email in notSentEmails)
            {
                try
                {
                    await SendEmail(email.To!, email.Subject!, email.Body!, email);
                }
                catch (Exception ex)
                {
                    await ChangeEmailStateAsync(email.Id, (long)EmailStatus.Error, ex.Message);
                }
            }
        }
    }
}
