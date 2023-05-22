using BuyAndSell.Data.Entities;
using BuyAndSell.Data.Resources;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace BuyAndSell.Data.Repositories.Contracts
{
    public interface IBaseRepository<TEntityModel> where TEntityModel : Base
    {
        Task<long> CountAllAsync(Query query);
        Task<TEntityModel> CreateAsync(TEntityModel entity);
        Task<bool> DeleteAsync(TEntityModel entity);
        Task<IEnumerable<TEntityModel>> GetAllAsync(Query query);
        Task<IEnumerable<TEntityModel>> GetAllByIds(IEnumerable<long> ids, bool asNoTracking = false);
        Task<TEntityModel?> GetAsync(Expression<Func<TEntityModel, bool>> predicate, bool asNoTracking = false);
        Task<TEntityModel?> GetAsync(long id, bool asNoTracking = false);
        Task<TEntityModel?> GetAsync(long id, Query query);
        IDbContextTransaction NewDbContextTransaction();
        Task<bool> UpdateAsync(TEntityModel entity);
    }
}