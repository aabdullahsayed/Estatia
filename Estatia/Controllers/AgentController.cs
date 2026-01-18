using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Estatia.Data;
using Estatia.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Estatia.Controllers
{
    public class AgentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AgentController(ApplicationDbContext context)
        {
            _context = context;
        }

   
       [Authorize(Roles = "Agent")] 
       public async Task<IActionResult> Index()
       {
           
           var agentIdClaim = User.FindFirst("UserId");

           if (agentIdClaim == null)
           {
               return RedirectToAction("Login", "Account");
           }

           int currentAgentId = int.Parse(agentIdClaim.Value);

  
           var myProperties = await _context.Properties
               .Where(p => p.AgentId == currentAgentId)
               .OrderByDescending(p => p.Id) // Show newest first
               .ToListAsync();

           return View(myProperties);
       }
       
       [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
   
        
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(Agent agent)
        {
            
            if (_context.Agents.Any(a => a.Email == agent.Email))
            {
                ModelState.AddModelError("Email", "Email already taken.");
                return View(agent);
            }

            if (ModelState.IsValid)
            {
                
                agent.PasswordHash = BCrypt.Net.BCrypt.HashPassword(agent.PasswordHash);

               
                _context.Add(agent);
                await _context.SaveChangesAsync();

                
                
                return RedirectToAction("Index", "Property");
            }

            return View(agent);
        }

    
        public IActionResult Login() => View();
        
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var agent = _context.Agents.FirstOrDefault(a => a.Email == email);
            
            if (agent != null && BCrypt.Net.BCrypt.Verify(password, agent.PasswordHash))
            {
                await SignIn(agent);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid credentials";
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        
        private async Task SignIn(Agent agent)
        {
            var claims = new List<Claim>
            {
                new Claim("UserId", agent.Id.ToString()), // Critical for posting properties
                new Claim(ClaimTypes.Name, agent.Name),
                new Claim(ClaimTypes.Role, "Agent")
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
        }
        
        
    }
}
