using BuySell.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Data.Repositories.Contracts
{
    public interface IEmailRepository
    {
        Task<Email?> GetEmailByIdAsync(long id);
        Task<bool> CreateEmailAsync(List<Email> emails);
        Task<bool> UpdateEmailStateAsync(Email email);
        Task<List<Email>> GetNotSentEmailsAsync();
    }
}
