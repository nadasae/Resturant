using Resturant.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.Core.Interfaces.Repositories
{
    public interface IGenericRepository<TEntity, TKey>
    where TEntity : Entity<TKey>
    where TKey : struct
    {
        Task<TEntity?> GetByIdAsync(TKey id);
        Task<List<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<bool> ExistsAsync(TKey id);
        Task SaveChangesAsync();
    }
}
