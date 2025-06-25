using Microsoft.AspNetCore.Mvc;
using RecipePlatform.BLL.Iterface;
using RecipePlatform.DAL.Context;
using RecipePlatform.Models;

namespace RecipePlatform.MVC.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IGenericRepository<Recipe> _recipes;
        private readonly ApplicationDbContext _context;

        public RecipeController(IGenericRepository<Recipe> recipes, ApplicationDbContext context)
        {
            _recipes = recipes;
            _context = context;
        }

        public IActionResult Index() => View(_recipes.GetAll());

        public IActionResult Details(int id)
        {
            var item = _recipes.GetById(id);
            return item == null ? NotFound() : View(item);
        }

        public IActionResult Create() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                _recipes.Add(recipe);
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }

        public IActionResult Edit(int id)
        {
            var recipe = _recipes.GetById(id);
            return recipe == null ? NotFound() : View(recipe);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                _recipes.Update(recipe);
                return RedirectToAction(nameof(Index));
            }
            return View(recipe);
        }

        public IActionResult Delete(int id)
        {
            var recipe = _recipes.GetById(id);
            return recipe == null ? NotFound() : View(recipe);
        }

        //[HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        //public IActionResult DeleteConfirmed(int id)
        //{
        //    _recipes.DeleteById(id);
        //    return RedirectToAction(nameof(Index));
        //}
    }
}