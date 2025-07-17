using DNATesting.Repository.PhienNT.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNATesting.Repository.PhienNT.Basic
{
    public class GenericRepository<T> where T : class
    {
        protected Se18Prn232Se1730G3DnatestingSystemContext _context;

        public GenericRepository()
        {
            _context ??= new Se18Prn232Se1730G3DnatestingSystemContext();
        }

        public GenericRepository(Se18Prn232Se1730G3DnatestingSystemContext context)
        {
            _context = context;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<int> CreateAsync(T entity)
        {
            _context.Add(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            //// Turning off Tracking for UpdateAsync in Entity Framework
            _context.ChangeTracker.Clear();
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
            return await _context.SaveChangesAsync();

            /*
            try
            {
                // Get primary key dynamically
                var keyValues = _context.Model.FindEntityType(typeof(T))
                                ?.FindPrimaryKey()
                                ?.Properties
                                ?.Select(p => p.PropertyInfo.GetValue(entity))
                                .ToArray();

                if (keyValues == null || keyValues.Length == 0)
                    throw new InvalidOperationException("No primary key defined for entity.");

                // Fetch existing entity without tracking
                var existingEntity = await _context.Set<T>().FindAsync(keyValues);

                if (existingEntity == null) return 0;

                _context.Entry(existingEntity).State = EntityState.Detached; // ✅ Prevent tracking conflicts
                _context.Entry(entity).State = EntityState.Modified; // ✅ Mark for update

                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return 0;
            }           
             */
        }

        public bool Remove(T entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> RemoveAsync(T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
    }
}
