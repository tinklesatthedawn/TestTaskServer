using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public interface IEntityRepository<T> where T : class
    {
        Task<T?> AddAsync(
            T entity,
            CancellationToken cancellationToken);

        Task<T?> GetByIdAsync(
            long id,
            CancellationToken cancellationToken);

        Task<List<T>> GetAllAsync(CancellationToken cancellationToken);

        Task<T?> UpdateAsync(
         T entity,
         CancellationToken cancellationToken);

        Task<T?> DeleteAsync(
            T entity,
            CancellationToken cancellationToken);
        
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
