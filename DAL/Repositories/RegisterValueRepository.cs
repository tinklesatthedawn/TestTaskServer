using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class RegisterValueRepository : IRegisterValueRepository
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();

        public async Task<RegisterValueEntity?> AddAsync(RegisterValueEntity registerValue, CancellationToken cancellationToken = default)
        {
            registerValue = registerValue.Copy();
            registerValue.Timestamp = DateTime.Now;
            var result = await _dbContext.AddAsync(registerValue);
            return await SaveChangesAsync(cancellationToken) ? result.Entity : null;
        }

        public async Task<RegisterValueEntity?> DeleteAsync(RegisterValueEntity registerValue, CancellationToken cancellationToken = default)
        {
            var registerValues = await _dbContext.RegisterValues.ToListAsync(cancellationToken);
            var toDelete = registerValues.Where(i => i.Id == registerValue.Id).ToList().FirstOrDefault();
            if (toDelete == null) return null;
            var result = _dbContext.RegisterValues.Remove(toDelete);
            return await SaveChangesAsync(cancellationToken) ? result.Entity : null;
        }

        public async Task<List<RegisterValueEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.RegisterValues.ToListAsync(cancellationToken);
        }

        public async Task<RegisterValueEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            List<RegisterValueEntity> registerValues = await _dbContext.RegisterValues.ToListAsync(cancellationToken);
            return registerValues.Where(i => i.Id == id).ToList().FirstOrDefault();
        }

        public async Task<RegisterValueEntity?> UpdateAsync(RegisterValueEntity registerValue, CancellationToken cancellationToken = default)
        {
            List<RegisterValueEntity> registerValues = await _dbContext.RegisterValues.ToListAsync(cancellationToken);
            var toUpdate = registerValues.Where(i => i.Id != registerValue.Id).ToList().FirstOrDefault();
            if (toUpdate != null)
            {
                toUpdate.CopyPropertiesFrom(registerValue).Timestamp = DateTime.Now;
                var result = _dbContext.Update(toUpdate);
                return await SaveChangesAsync(cancellationToken) ? result.Entity : null;
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
