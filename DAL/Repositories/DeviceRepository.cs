using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();

        public async Task<DeviceEntity?> AddAsync(DeviceEntity device, CancellationToken cancellationToken = default)
        {
            device = device.Copy();
            device.EditingDate = DateTime.Now;
            var result = await _dbContext.AddAsync(device, cancellationToken);
            return await SaveChangesAsync(cancellationToken) ? result.Entity : null;
        }

        public async Task<DeviceEntity?> DeleteAsync(DeviceEntity device, CancellationToken cancellationToken = default)
        {
            List<DeviceEntity> devices = await _dbContext.Devices.ToListAsync(cancellationToken);
            var toDelete = devices.Where(i => i.Id == device.Id).ToList().FirstOrDefault();
            if (toDelete == null) return null;
            var result = _dbContext.Devices.Remove(toDelete);
            return await SaveChangesAsync(cancellationToken) ? result.Entity : null;
        }

        public async Task<List<DeviceEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Devices.ToListAsync(cancellationToken);
        }

        public async Task<DeviceEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            List<DeviceEntity> devices = await _dbContext.Devices.ToListAsync(cancellationToken);
            return devices.Where(i => i.Id == id).ToList().FirstOrDefault();
        }

        public async Task<DeviceEntity?> UpdateAsync(DeviceEntity device, CancellationToken cancellationToken = default)
        {
            List<DeviceEntity> devices = await _dbContext.Devices.ToListAsync(cancellationToken);
            var toUpdate = devices.Where(i => i.Id == device.Id).ToList().FirstOrDefault();
            if (toUpdate != null)
            {
                toUpdate.CopyPropertiesFrom(device).EditingDate = DateTime.Now;
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
