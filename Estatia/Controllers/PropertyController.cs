using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; // Needed for .Include()
using Estatia.Data;
using Estatia.Models;

namespace Estatia.Controllers;

    public class PropertyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PropertyController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var property = _context.Properties.ToList();
            return View(property);
        }

       
    }
