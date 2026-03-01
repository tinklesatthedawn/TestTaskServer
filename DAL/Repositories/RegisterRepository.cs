using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class RegisterRepository : IRegisterRepository
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();

        public async Task<RegisterEntity?> AddAsync(RegisterEntity register, CancellationToken cancellationToken = default)
        {
            register = register.Copy();
            register.EditingDate = DateTime.Now;
            var result = await _dbContext.AddAsync(register, cancellationToken);
            return await SaveChangesAsync(cancellationToken) ? result.Entity : null;
        }

        public async Task<RegisterEntity?> DeleteAsync(RegisterEntity register, CancellationToken cancellationToken = default)
        {
            List<RegisterEntity> registers = await _dbContext.Registers.ToListAsync(cancellationToken);
            var toDelete = registers.Where(i => i.Id == register.Id).ToList().FirstOrDefault();
            if (toDelete == null) return null;
            var result = _dbContext.Registers.Remove(toDelete);
            return await SaveChangesAsync(cancellationToken) ? result.Entity : null;
        }

        public async Task<List<RegisterEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Registers.ToListAsync(cancellationToken);
        }

        public async Task<RegisterEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            List<RegisterEntity> registers = await _dbContext.Registers.ToListAsync(cancellationToken);
            return registers.Where(i => i.Id == id).ToList().FirstOrDefault();
        }

        public async Task<RegisterEntity?> UpdateAsync(RegisterEntity register, CancellationToken cancellationToken = default)
        {
            List<RegisterEntity> registers = await _dbContext.Registers.ToListAsync(cancellationToken);
            var toUpdate = registers.Where(i => i.Id == register.Id).ToList().FirstOrDefault();
            if (toUpdate != null)
            {
                toUpdate.CopyPropertiesFrom(register).EditingDate = DateTime.Now;
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
