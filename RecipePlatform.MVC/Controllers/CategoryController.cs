using Microsoft.AspNetCore.Mvc;
using RecipePlatform.Models;
using RecipePlatform.DAL.Context;
using RecipePlatform.BLL.Iterface;

namespace CategoryPlatform.MVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IGenericRepository<Category> _categorys;
        private readonly ApplicationDbContext _context;

        public CategoryController(IGenericRepository<Category> categorys, ApplicationDbContext context)
        {
            _categorys = categorys;
            _context = context;
        }

        public IActionResult Index() => View(_categorys.GetAll());

        public IActionResult Details(int id)
        {
            var item = _categorys.GetById(id);
            return item == null ? NotFound() : View(item);
        }

        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _categorys.Add(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public IActionResult Edit(int id)
        {
            var category = _categorys.GetById(id);
            return category == null ? NotFound() : View(category);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _categorys.Update(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public IActionResult Delete(int id)
        {
            var category = _categorys.GetById(id);
            return category == null ? NotFound() : View(category);
        }

        //[HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        //public IActionResult DeleteConfirmed(int id)
        //{
        //    _categorys.DeleteById(id);
        //    return RedirectToAction(nameof(Index));
        //}



    }
}