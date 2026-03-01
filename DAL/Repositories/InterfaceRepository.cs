using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using Domain.Repository;

namespace DAL.Repository
{
    public class InterfaceRepository : IInterfaceRepository
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();

        public async Task<InterfaceEntity?> AddAsync(InterfaceEntity @interface, CancellationToken cancellationToken = default)
        {
            @interface = @interface.Copy();
            @interface.EditingDate = DateTime.Now;
            var result = await _dbContext.Interfaces.AddAsync(@interface, cancellationToken);
            return await SaveChangesAsync(cancellationToken) == true ? result.Entity : null;
        }

        public async Task<InterfaceEntity?> DeleteAsync(InterfaceEntity @interface, CancellationToken cancellationToken = default)
        {
            List<InterfaceEntity> interfaces = await _dbContext.Interfaces.ToListAsync(cancellationToken);
            var toDelete = interfaces.Where(i => i.Id == @interface.Id).ToList().FirstOrDefault();
            if (toDelete == null) return null;
            var result = _dbContext.Interfaces.Remove(toDelete);
            return await SaveChangesAsync(cancellationToken) == true ? result.Entity : null;
        }

        public async Task<List<InterfaceEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Interfaces.ToListAsync(cancellationToken);
        }

        public async Task<InterfaceEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            List<InterfaceEntity> interfaces = await _dbContext.Interfaces.ToListAsync(cancellationToken);
            return interfaces.Where(i => i.Id == id).ToList().FirstOrDefault();
        }

        public async Task<InterfaceEntity?> UpdateAsync(InterfaceEntity @interface, CancellationToken cancellationToken = default)
        {
            List<InterfaceEntity> interfaces = await _dbContext.Interfaces.ToListAsync(cancellationToken);
            var toUpdate = interfaces.Where(i => i.Id == @interface.Id).FirstOrDefault();
            if (toUpdate != null)
            {
                toUpdate.CopyPropertiesFrom(@interface).EditingDate = DateTime.Now;
                var result = _dbContext.Interfaces.Update(toUpdate);
                return await SaveChangesAsync(cancellationToken) == true ? result.Entity : null;
            }
            return null;
        }

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
