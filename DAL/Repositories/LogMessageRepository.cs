using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class LogMessageRepository : ILogMessageRepository
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();

        public async Task<LogMessageEntity?> AddAsync(LogMessageEntity entity, CancellationToken cancellationToken)
        {
            entity = entity.Copy();
            entity.TimeStamp = DateTime.Now;
            var result = await _dbContext.AddAsync(entity, cancellationToken);
            return await SaveChangesAsync(cancellationToken) ? result.Entity : null;
        }

        public async Task<LogMessageEntity?> DeleteAsync(LogMessageEntity entity, CancellationToken cancellationToken)
        {
            List<LogMessageEntity> logMessages = await _dbContext.LogMessages.ToListAsync(cancellationToken);
            var toDelete = logMessages.Where(i => i.Id == entity.Id).ToList().FirstOrDefault();
            if (toDelete == null) return null;
            var result = _dbContext.LogMessages.Remove(toDelete);
            return await SaveChangesAsync(cancellationToken) ? result.Entity : null;
        }

        public async Task<List<LogMessageEntity>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.LogMessages.ToListAsync(cancellationToken);
        }

        public async Task<LogMessageEntity?> GetByIdAsync(long id, CancellationToken cancellationToken)
        {
            List<LogMessageEntity> logMessages = await _dbContext.LogMessages.ToListAsync(cancellationToken);
            return logMessages.Where(i => i.Id == id).ToList().FirstOrDefault();
        }

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
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

        public async Task<LogMessageEntity?> UpdateAsync(LogMessageEntity entity, CancellationToken cancellationToken)
        {
            List<LogMessageEntity> logMessages = await _dbContext.LogMessages.ToListAsync(cancellationToken);
            var toUpdate = logMessages.Where(i => i.Id == entity.Id).ToList().FirstOrDefault();
            if (toUpdate == null) return null;
            toUpdate.CopyPropertiesFrom(entity);
            var result = _dbContext.Update(toUpdate);
            return await SaveChangesAsync(cancellationToken) ? result.Entity : null;
        }
    }
}
