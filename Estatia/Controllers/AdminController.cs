using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Estatia.Data;
using Estatia.Models;
using Estatia.Models.ViewModels;

namespace Estatia.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

    
        
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Dashboard()
        {
         
            var agents = await _context.Agents
                .ToListAsync();

            var users = await _context.Users
                .Where(u => u.Role == UserRole.User)
                .ToListAsync();

            var totalProperties = await _context.Properties.CountAsync();

            var viewModel = new AdminDashboardViewModel
            {
                TotalAgents = agents.Count,
                TotalUsers = users.Count,
                TotalProperties = totalProperties,
                Agents = agents,
                Users = users
            };

            return View(viewModel);
        }

  
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Dashboard));
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
           
            var existingUser = await _context.Users.FindAsync(user.Id);
            
            if (existingUser != null)
            {
                existingUser.FullName = user.FullName;
                existingUser.Email = user.Email;
                existingUser.PhoneNumber = user.PhoneNumber;
                
                _context.Update(existingUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Dashboard));
            }
            return View(user);
        }
        

        [HttpGet]
        public async Task<IActionResult> EditAgent(int id)
        {
            var agent = await _context.Agents.FirstOrDefaultAsync(u => u.Id == id);
            if (agent == null) return NotFound();
            return View(agent);
        }

        [HttpPost]
        public async Task<IActionResult> EditAgent(Agent model)
        {
         
            ModelState.Remove("PasswordHash"); 
            ModelState.Remove("Properties"); 

         
            if (ModelState.IsValid)
            {
                var agent = await _context.Agents.FindAsync(model.Id);

                if (agent != null)
                {
                   
                    agent.Name = model.Name;
                    agent.Email = model.Email;
            
               
            
                    _context.Update(agent);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Dashboard));
                }
            }
            
            return View(model);
        }
        
        [HttpGet]
        public async Task<IActionResult> EditUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id && u.Role == UserRole.User);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(User model)
        {
            var user = await _context.Users.FindAsync(model.Id);
            if (user != null)
            {
                
                user.FullName = model.FullName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;

                _context.Update(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Dashboard));
            }
            return View(model);
        }
    }
}