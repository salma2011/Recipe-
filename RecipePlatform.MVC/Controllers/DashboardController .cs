using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecipePlatform.BLL.Iterface;
using RecipePlatform.DAL.Context;
using RecipePlatform.Models;
using RecipePlatform.PL.ViewModels;

namespace RecipePlatform.PL.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IGenericRepository<Recipe> _recipes;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboardController(
            IGenericRepository<Recipe> recipes,
            ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment,
            UserManager<ApplicationUser> userManager)
        {
            _recipes = recipes;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(_recipes.GetAll());
        }

        public IActionResult UserDashboard()
        {
            var userId = _userManager.GetUserId(User);
            var recipes = _context.Recipes
                .Where(r => r.UserId == userId)
                .ToList();

            return View(recipes);
        }

        public IActionResult Create()
        {
            var model = new RecipeFormViewModel
            {
                Categories = _context.Categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RecipeFormViewModel model)
        {
            if (model.CategoryId == null)
            {
                ModelState.AddModelError("CategoryId", "Please select a category.");
            }

            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);

                var recipe = new Recipe
                {
                    Title = model.Title,
                    Description = model.Description,
                    Ingredients = model.Ingredients,
                    Instructions = model.Instructions,
                    PreparationTime = model.PreparationTime,
                    CookingTime = model.CookingTime,
                    Servings = model.Servings,
                    Difficulty = model.Difficulty,
                    CategoryId = model.CategoryId.Value,
                    CreatedAt = DateTime.Now,
                    UserId = userId
                };

                if (model.ImageFile != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                    string extension = Path.GetExtension(model.ImageFile.FileName);
                    fileName += DateTime.Now.ToString("yyyyMMddHHmmss") + extension;
                    string path = Path.Combine(wwwRootPath + "/images/", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(fileStream);
                    }

                    recipe.ImagePath = "/images/" + fileName;
                }

                _context.Recipes.Add(recipe);
                _context.SaveChanges();
                return RedirectToAction("Dashboard", "Account");
            }

            model.Categories = _context.Categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var recipe = _recipes.GetById(id);
            if (recipe == null)
            {
                return NotFound();
            }

            _recipes.Delete(recipe);
            return RedirectToAction("Dashboard", "Account");
        }
    }
}
