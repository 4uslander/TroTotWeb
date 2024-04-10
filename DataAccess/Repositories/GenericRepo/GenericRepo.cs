using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System;
using BusinessObject.Context;

namespace DataAccess.Repositories.GenericRepo
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        protected readonly TroTotDBContext context;
        private DbSet<T> _entities;

        public GenericRepo(TroTotDBContext context)
        {
            this.context = context;
            _entities = context.Set<T>();
        }

        public async Task<IList<T>> GetAll()
        {
            return _entities.ToList();
        }

        public virtual async Task CreateAsync(T entity)
        {
            await _entities.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task CreateRangeAsync(List<T> entities)
        {
            await context.AddRangeAsync(entities);
            await context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T updated)
        {
            context.Attach(updated).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(IList<T> entities)
        {
            context.UpdateRange(entities);
            await context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _entities.Remove(entity);
            await context.SaveChangesAsync();
        }

        public virtual async Task DeleteRangeAsync(IList<T> entities)
        {
            _entities.RemoveRange(entities);
            await context.SaveChangesAsync();
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _entities.AsQueryable().AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>[]? includes)
        {
            return await AsQueryableWithIncludes(includes).AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<IList<T>> WhereAsync(Expression<Func<T, bool>> predicate, params string[] navigationProperties)
        {
            List<T> list;
            var query = _entities.AsQueryable();
            foreach (string navigationProperty in navigationProperties)
                query = query.Include(navigationProperty);
            list = await query.Where(predicate).AsNoTracking().ToListAsync<T>();
            return list;
        }

        public async Task<IList<T>> WhereAsync(Expression<Func<T, bool>> predicate, Expression<Func<T, object>>[]? includes)
        {
            return await AsQueryableWithIncludes(includes).Where(predicate).AsNoTracking().ToListAsync();
        }

        private IQueryable<T> AsQueryableWithIncludes(Expression<Func<T, object>>[]? includes)
        {
            var query = _entities.AsQueryable();
            if (includes == null) return query;

            foreach (var item in includes)
            {
                query = query.Include(item);
            }

            return query;
        }
    }
}
