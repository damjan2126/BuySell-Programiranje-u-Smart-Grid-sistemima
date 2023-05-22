using BuyAndSell.Data.Entities;
using BuyAndSell.Data.Resources;
using System.Linq.Expressions;

namespace BuyAndSell.Data.Repositories.Contracts
{
    public interface IUserRepository
    {
        Task<long> CountAllAsync(Query query);
        Task<User?> Get(Expression<Func<User, bool>> predicate, bool asNoTracking = false);
        Task<IEnumerable<User>> GetAllAsync(Query query);
        Task<User?> GetAsync(Expression<Func<User, bool>> predicate, bool asNoTracking = false);
        Task RemoveAllTokensAsync(long userId);
    }
}