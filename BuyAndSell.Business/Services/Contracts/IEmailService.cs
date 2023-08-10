using BuySell.Data.Entities;
using System.Net.Mail;

namespace BuySell.Business.Services.Contracts
{
    public interface IEmailService
    {
        Task<bool> ChangeEmailStateAsync(long emailId, long statusId, string? message = null);
        Task<bool> SendEmail(string to, string subject, string body, Email? email = null, Attachment? attachment = null);
        Task SendEmailAsync(string host);
    }
}