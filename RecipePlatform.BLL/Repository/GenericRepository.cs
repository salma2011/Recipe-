using Microsoft.EntityFrameworkCore;
using RecipePlatform.DAL.Context;
using RecipePlatform.Models;
using RecipePlatform.BLL.Iterface;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace RecipePlatform.BLL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        // إضافة كيان (Sync)
        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        // إضافة كيان (Async)
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        // حذف كيان (بناء على الكيان نفسه)
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        // جلب جميع الكيانات (Sync)
        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        // جلب جميع الكيانات (Async)
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        // جلب كيان بالمعرف (Sync)
        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        // جلب كيان بالمعرف (Async)
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        // تحديث كيان
        public void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }
    }
}
