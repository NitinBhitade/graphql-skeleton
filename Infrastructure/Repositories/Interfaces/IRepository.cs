using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<TEntity> GetById(int id);
        Task<IEnumerable<TEntity>> GetAll();
        Task Add(TEntity entity, CancellationToken cancellationToken);
        Task Delete(TEntity entity, CancellationToken cancellationToken);
        Task Update(TEntity entity, CancellationToken cancellationToken);
    }
}
