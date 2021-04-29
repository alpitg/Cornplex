namespace Cornplex.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Cornplex.Domain.Entities;
    using Cornplex.Persistence.IRepositories;
    using Microsoft.EntityFrameworkCore;

    public class Repository<TModel> : IRepository<TModel> where TModel : BaseEntity
    {
        private readonly ApplicationDbContext _dbContext;
        protected readonly DbSet<TModel> context;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException();
            context = _dbContext.Set<TModel>();
        }

        public async Task<TModel> GetAsync(Expression<Func<TModel, bool>> predicate)
        {
            return await context.AsNoTracking().Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TModel>> GetListAsync(Expression<Func<TModel, bool>> predicate)
        {
            return await context.AsNoTracking().Where(predicate).ToListAsync();
        }

        public IQueryable<TModel> Queryable(Expression<Func<TModel, bool>> predicate)
        {
            return context.Where(predicate);
        }

        public void Add(TModel entity)
        {
            context.Add(entity);
        }

        public void AddRange(IEnumerable<TModel> entities)
        {
            context.AddRange(entities);
        }

        public void Remove(TModel entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached) context.Attach(entity);

            context.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TModel> entities)
        {
            foreach (var entity in entities)
            {
                if (_dbContext.Entry(entity).State == EntityState.Detached) context.Attach(entity);

                context.Remove(entity);
            }
        }

        public void Update(TModel entity)
        {
            context.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
        
        public void Dispose()
        {
            _dbContext?.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
