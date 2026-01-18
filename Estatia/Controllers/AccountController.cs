using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Estatia.Data;
using Estatia.Models;
using Microsoft.AspNetCore.Authorization;

namespace Estatia.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

  [Authorize(Roles = "User")]
        public IActionResult UserProfile()
        {
            return View("User");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            ClaimsPrincipal principal = null;
            string userRole = "";

       
            if (email == "admin@estatia.com" && password == "admin123")
            {
                userRole = "Admin";
                principal = CreatePrincipal("Super Admin", "0", userRole);
            }

            else
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

                if (user != null)
                {
                    userRole = user.Role.ToString(); // Likely "User"
                    principal = CreatePrincipal(user.FullName, user.Id.ToString(), userRole);
                }
                else
                {
 
                    var agent = _context.Agents.FirstOrDefault(a => a.Email == email);
                    
                    if (agent != null && BCrypt.Net.BCrypt.Verify(password, agent.PasswordHash))
                    {
                        userRole = "Agent";
                
                        principal = CreatePrincipal(agent.Name, agent.Id.ToString(), userRole);
                    }
                }
            }

 
            if (principal != null)
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                if (userRole == "Admin") return RedirectToAction("Dashboard", "Admin");
                if (userRole == "Agent") return RedirectToAction("Index", "Agent");
                return RedirectToAction("User", "Account");
            }

            ViewBag.Error = "Invalid Email or Password";
            return View();
        }
        
        private ClaimsPrincipal CreatePrincipal(string name, string id, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, name),
                new Claim("UserId", id),
                new Claim(ClaimTypes.Role, role)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            return new ClaimsPrincipal(identity);
        }

        public async Task<IActionResult> Logout()
        {
            // Delete the Cookie
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Property");
        }
        
        public IActionResult AccessDenied()
        {
            return View();
        }


        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "This email is already registered.");
                    return View(user);
                }


                user.Role = UserRole.User;

                _context.Add(user);
                await _context.SaveChangesAsync();
                
                return RedirectToAction("Login");
            }
    
            return View(user);
        }
        
    }
}