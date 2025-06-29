using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // ضروري للـ Include
using RecipePlatform.DAL.Context;
using RecipePlatform.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RecipePlatform.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public AdminController(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        // 🏠 لوحة التحكم
        public IActionResult AdminPanel()
        {
            return View();
        }

        // 👥 إدارة المستخدمين
        public IActionResult UserList()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> MakeAdmin(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null) return NotFound("User not found");

            if (!await _roleManager.RoleExistsAsync("Admin"))
                await _roleManager.CreateAsync(new IdentityRole("Admin"));

            if (!await _userManager.IsInRoleAsync(user, "Admin"))
                await _userManager.AddToRoleAsync(user, "Admin");

            return RedirectToAction("UserList");
        }

        // 🍳 عرض الوصفات مع التصنيفات
        public IActionResult RecipeList()
        {
            var recipes = _context.Recipes.Include(r => r.Category).ToList();
            return View(recipes);
        }

        // 🆕 GET: صفحة إضافة وصفة جديدة
        [HttpGet]
        public IActionResult CreateRecipe()
        {
            // جلب التصنيفات للعرض في القائمة المنسدلة
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        // 🆙 POST: معالجة إضافة وصفة جديدة
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateRecipe(Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                _context.Recipes.Add(recipe);
                _context.SaveChanges();
                return RedirectToAction(nameof(RecipeList));
            }
            // في حال وجود خطأ نعيد جلب التصنيفات
            ViewBag.Categories = _context.Categories.ToList();
            return View(recipe);
        }

        // لاحقًا يمكن تضيف:
        // - DeleteRecipe
        // - EditRecipe
        // - Category Management
    }
}
