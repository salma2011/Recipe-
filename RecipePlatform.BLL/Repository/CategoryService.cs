using RecipePlatform.BLL.Iterface;
using RecipePlatform.DAL.Context;
using RecipePlatform.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecipePlatform.BLL.Repository
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly ApplicationDbContext _context;

        public CategoryService(IGenericRepository<Category> categoryRepository, ApplicationDbContext context)
        {
            _categoryRepository = categoryRepository;
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _categoryRepository.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _categoryRepository.Update(category);
            await _context.SaveChangesAsync();
        }

        public Task DeleteCategoryAsync(int id)
        {
            throw new NotImplementedException();
        }

        //public async Task DeleteCategoryAsync(int id)
        //{
        //    var category = await _categoryRepository.GetByIdAsync(id);
        //    if (category != null)
        //    {
        //        _categoryRepository.DeleteById(category);
        //        await _context.SaveChangesAsync();
        //    }
    }
    }


