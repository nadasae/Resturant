using Microsoft.EntityFrameworkCore;
using Resturant.Core.Entities.Base;
using Resturant.Core.Interfaces.Repositories;
using Resturant.DA.Context;


namespace Resturant.DA.Implementations.Base
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
     where TEntity : Entity<TKey>
     where TKey : struct
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public  IQueryable<TEntity> GetAllAsync()
        {
            return  _dbSet;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<bool> ExistsAsync(TKey id)
        {
            return await _dbSet.AnyAsync(e => e.Id.Equals(id));
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
