using BuySell.Data.Entities;
using BuySell.Data.Enums;
using BuySell.Data.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Data.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly DatabaseContext _context;

        public EmailRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Email?> GetEmailByIdAsync(long id)
        {
            return await _context.Emails.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> CreateEmailAsync(List<Email> emails)
        {
            await _context.Emails.AddRangeAsync(emails);
            var created = await _context.SaveChangesAsync();

            return created > 0;
        }

        public async Task<bool> UpdateEmailStateAsync(Email email)
        {
            _context.Update(email);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<List<Email>> GetNotSentEmailsAsync()
        {
            return await _context.Emails.Include(x => x.CreatedByUser).Where(x => x.Status == (long)EmailStatus.New).ToListAsync();
        }
    }
}
