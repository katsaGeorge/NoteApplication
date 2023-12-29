using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NoteApp.Data;

namespace NoteApp.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T>
        where T : class
    {

        public readonly NotedbContext _context;
        private readonly DbSet<T> _table;

        protected BaseRepository(NotedbContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await _table.ToListAsync();
            return entities;
        }

        public virtual async Task<T?> GetAsync(int id)
        {
            var entity = await _table.FindAsync(id);
            return entity;
        }

        public virtual async Task AddAsync(T entity)
        {
           await _table.AddAsync(entity);
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _table.AddRangeAsync(entities);
        }

        public virtual void UpdateAsync(T entity)
        {   
            //_table.Attach(entity); Δεν χρειαζεται στην περιπτωση που το service layer κανει find
            _context.Entry(entity).State = EntityState.Modified;
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            T? existingEntity = await _table.FindAsync(id);
            if (existingEntity is null) return false;
            _table.Remove(existingEntity);
            return true;
        }

       

        public Task<int> GetCountAsync()
        {
            var count = _table.CountAsync();
            return count;
        }

       
    }
}
