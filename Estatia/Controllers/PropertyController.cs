using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; 
using Estatia.Data;
using Estatia.Models;
using Microsoft.AspNetCore.Authorization;

namespace Estatia.Controllers;

    public class PropertyController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PropertyController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment )
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index(string searchString, ListingType? type, decimal? maxPrice)
        {
            
            var query = _context.Properties.AsQueryable();

            
            if (!string.IsNullOrEmpty(searchString))
            {
                
                query = query.Where(p => p.Title.Contains(searchString) 
                                         || p.City.Contains(searchString) 
                                         || p.Area.Contains(searchString));
            }

         
            if (type.HasValue)
            {
                query = query.Where(p => p.Type == type);
            }
            
            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= maxPrice);
            }
            
            return View(await query.ToListAsync());
        }
        [Authorize(Roles = "Agent,Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Agent,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Property property)
        {
            if (ModelState.IsValid)
            {
                
                _context.Add(property);
                await _context.SaveChangesAsync(); 
                

              
                if (property.ImageFile != null)
                {
                    
                    string ext = Path.GetExtension(property.ImageFile.FileName);
                    
                   
                    string fileName = property.Id.ToString() + ext;
                    
                 
                    string folder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    string filePath = Path.Combine(folder, fileName);

                    
                    if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

                    
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await property.ImageFile.CopyToAsync(stream);
                    }

                  
                    property.ImageUrl = "/images/" + fileName;
                    _context.Update(property);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("Index", "Property");
            }
            return View(property);
        }
        

        
        [Authorize(Roles = "User,Agent,Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var property = await _context.Properties
                .Include(p => p.Agent) 
                .FirstOrDefaultAsync(m => m.Id == id);

            if (property == null) return NotFound();

            return View(property);
        }
        
        [Authorize(Roles = "Agent,Admin")]
public async Task<IActionResult> Edit(int? id)
{
    if (id == null) return NotFound();

    var property = await _context.Properties.FindAsync(id);
    if (property == null) return NotFound();


    var userIdClaim = User.FindFirst("UserId");
    if (userIdClaim == null) return RedirectToAction("Login", "Account");
    
    int currentUserId = int.Parse(userIdClaim.Value);


    bool isOwner = property.AgentId == currentUserId;
    bool isAdmin = User.IsInRole("Admin");

    if (!isOwner && !isAdmin)
    {

        return RedirectToAction("AccessDenied", "Account"); 
  
    }

    return View(property);
}


[HttpPost]
[ValidateAntiForgeryToken]
[Authorize(Roles = "Agent,Admin")]
public async Task<IActionResult> Edit(int id, Property property)
{
    if (id != property.Id) return NotFound();

    ModelState.Remove("Agent"); 
    ModelState.Remove("ImageFile");

    if (ModelState.IsValid)
    {
        try
        {
            var existingProperty = await _context.Properties
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (existingProperty == null) return NotFound();

          
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim == null) return RedirectToAction("Login", "Account");
            int currentUserId = int.Parse(userIdClaim.Value);

            if (existingProperty.AgentId != currentUserId && !User.IsInRole("Admin"))
            {
                return RedirectToAction("AccessDenied", "Account");
            }
           

           
            if (property.ImageFile != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(property.ImageFile.FileName);
                string path = Path.Combine(wwwRootPath + "/images/", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await property.ImageFile.CopyToAsync(fileStream);
                }
                property.ImageUrl = "/images/" + fileName;
            }
            else
            {
                property.ImageUrl = existingProperty.ImageUrl; 
            }

           
            property.AgentId = existingProperty.AgentId;

            _context.Update(property);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Properties.Any(e => e.Id == property.Id)) return NotFound();
            else throw;
        }

        if (User.IsInRole("Agent")) return RedirectToAction("Index", "Agent");
        return RedirectToAction("Dashboard", "Admin");
    }
    return View(property);
}

    }
