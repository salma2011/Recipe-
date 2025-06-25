using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecipePlatform.Models;
using RecipePlatform.PL.ViewModels;
using System.Threading.Tasks;

namespace RecipePlatform.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Login() => View(new LoginViewModel());

     

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "User not found");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(
                user.UserName, model.Password, false, false);

            if (result.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user);

                if (roles.Contains("Admin"))
                    return RedirectToAction("AdminPanel", "Account");

                return RedirectToAction("Dashboard", "Account");
            }

            ModelState.AddModelError("", "Invalid login attempt");
            return View(model);
        }






        [HttpGet]
        public IActionResult Register() => View();

       

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // تأكد من وجود الدور Admin
                if (!await _roleManager.RoleExistsAsync("Admin"))
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));

                // اجعل أول مستخدم هو الأدمن
                if (!_userManager.Users.Any())
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
                else
                {
                    if (!await _roleManager.RoleExistsAsync("User"))
                        await _roleManager.CreateAsync(new IdentityRole("User"));

                    await _userManager.AddToRoleAsync(user, "User");
                }

                return RedirectToAction("Login");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }





        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        // ——— حماية صفحة الـ Dashboard للمستخدمين المسجلين فقط ———
        [Authorize]
        public IActionResult Dashboard()
        {
            return View();
        }

        // ——— حماية صفحة خاصة بالأدمن فقط ———
        [Authorize(Roles = "Admin")]
        public IActionResult AdminPanel()
        {
            return View();
        }



        // ——— صفحة لعرض المستخدمين (مفترض تكون محمية للأدمن فقط) ———
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UserList()
        {
            var users = _userManager.Users;
            return View(users);
        }

        // ——— تعيين دور Admin لمستخدم ———
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> MakeAdmin(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user == null)
            {
                return NotFound("User not found");
            }

            if (!await _roleManager.RoleExistsAsync("Admin"))
                await _roleManager.CreateAsync(new IdentityRole("Admin"));

            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            if (!isAdmin)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }

            return RedirectToAction("UserList");
        }
    }
}
