using BuySell.Data.Entities;
using BuySell.Data.Extensions;
using BuySell.Data.Repositories.Contracts;
using BuySell.Data.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;


namespace BuySell.Data.Repositories
{
    public abstract class BaseRepository<TEntityModel> : IBaseRepository<TEntityModel> where TEntityModel : Base
    {
        protected readonly DatabaseContext Ctx;
        private readonly DbSet<TEntityModel> Entity;

        protected BaseRepository(DatabaseContext ctx)
        {
            Ctx = ctx;
            Entity = Ctx.Set<TEntityModel>();
        }
        public virtual async Task<TEntityModel> CreateAsync(TEntityModel entity)
        {
            await Entity.AddAsync(entity);
            await Ctx.SaveChangesAsync();

            Ctx.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public virtual async Task<bool> UpdateAsync(TEntityModel entity)
        {
            Entity.Update(entity);
            return await Ctx.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> DeleteAsync(TEntityModel entity)
        {
            Entity.Remove(entity);
            return await Ctx.SaveChangesAsync() > 0;
        }

        public virtual async Task<TEntityModel?> GetAsync(long id, Query query)
        {
            return await Entity
                .Include(x => x.CreatedByUser)
                .AddAsNoTracking(query)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<TEntityModel?> GetAsync(long id, bool asNoTracking = false)
        {
            return await Entity
                .Include(x => x.CreatedByUser)
                .AddAsNoTracking(asNoTracking)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<IEnumerable<TEntityModel>> GetAllAsync(Query query)
        {
            return await Entity
                .Include(x => x.CreatedByUser)
                .FilterBy(query)
                .AddAsNoTracking(query)
                .Sort(query)
                .Paginate(query)
                .ToListAsync();
        }

        public virtual async Task<TEntityModel?> GetAsync(Expression<Func<TEntityModel, bool>> predicate,
            bool asNoTracking = false)
        {
            return await Entity
                .Include(x => x.CreatedByUser)
                .AddAsNoTracking(asNoTracking)
                .FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<long> CountAllAsync(Query query)
        {
            var queryable = Entity
                .FilterBy(query)
                .AddAsNoTracking(query);


            return await queryable
                .LongCountAsync();
        }

        public virtual async Task<IEnumerable<TEntityModel>> GetAllByIds(IEnumerable<long> ids, bool asNoTracking = false)
        {
            return await Entity
                    .Include(x => x.CreatedByUser)
                    .Where(x => ids.Contains(x.Id))
                    .AddAsNoTracking(asNoTracking)
                    .ToListAsync();
        }

        public IDbContextTransaction NewDbContextTransaction()
        {
            return Ctx.Database.BeginTransaction();
        }

    }
}
