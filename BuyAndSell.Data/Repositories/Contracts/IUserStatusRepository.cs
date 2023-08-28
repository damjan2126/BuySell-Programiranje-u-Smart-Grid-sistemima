using BuySell.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Data.Repositories.Contracts
{
    public interface IUserStatusRepository : IBaseRepository<UserStatus>
    {
        Task<UserStatus?> GetCurrentStatus(long userId);
        Task<IEnumerable<UserStatus>> GetAllAsync(Expression<Func<UserStatus, bool>> predicate, bool asNoTracking = false);
    }
}
