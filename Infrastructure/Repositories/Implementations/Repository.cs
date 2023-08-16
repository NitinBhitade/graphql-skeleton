using Domain.Entities.Base;
using Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Implementations
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly ApplicationDbContext _dbContext;
        protected Repository(ApplicationDbContext context)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<TEntity> GetById(int id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }
        public async Task Add(TEntity entity, CancellationToken cancellationToken)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(TEntity entity, CancellationToken cancellationToken)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(TEntity entity, CancellationToken cancellationToken)
        {
            _dbContext.Set<TEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
