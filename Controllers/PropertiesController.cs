using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TP_PWEB.Data;
using TP_PWEB.Models;
using TP_PWEB.Models.Properties;
using TP_PWEB.Services;

namespace TP_PWEB.Controllers
{
    public class PropertiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private string UserId
        {
            get { 
                return _context?.GetCurrentUserId(); 
            }   
            
        }
            

                
        public PropertiesController(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public double GetRating(Property property)
        {
            double rating = 0;

            foreach (var reservation in property.Reservations)
            {
                rating += reservation.StayEvaluation.Rating;
            }
            //Retorna Nan se property.Reservations.Count == 0 (0/0), mas faz sentido no contexto
            return rating /  property.Reservations.Count;
        }


        [Authorize(Roles = RoleNames.PropertyOwner)]
        // GET: Properties
        public async Task<IActionResult> Index()
        {
            IQueryable<Property> properties;

            if (User.IsInRole(RoleNames.PropertyOwner)) {
                properties = _context.Properties
                    .Where(p => p.PropertyManager.PropertyManagerId == UserId).Include(p=>p.Reservations);
                                
            }
            else
                properties = _context.Properties;

            foreach (var property in properties)
            {
                property.Rating = GetRating(property);

                property.CoverImage = await _context.Images.FirstAsync(i => i.Property.Id == property.Id);
            }

            return View(await properties.ToListAsync());
        }
        
        [HttpGet]
        public async Task<IActionResult> GetImageAsync(int imageId)
        {
            if (ModelState.IsValid)
            {
                var image = await _context.Images.FindAsync(imageId);

                return File(image.Content, image.ContentType);
            }
            //returnar image unknown property
            return null;

        }

        // GET: Properties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @property = await _context.Properties
                .Include(p => p.PropertyManager)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@property == null)
            {
                return NotFound();
            }

            return View(@property);
        }

        // GET: Properties/Create
        [Authorize(Roles = RoleNames.PropertyOwner)]
        public async Task<IActionResult> CreateAsync()
        {
            List<Category> categories = await _context.Categories.ToListAsync();

            Property model;

            if (categories != null)
                model = new Property(categories);
            else
            {
                return RedirectToAction(nameof(Index));
            }

            model.OwnerId = UserId;
            //model.PropertyManager

            return View(model);
        }

        // POST: Properties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Price,Comodities,OwnerId,CategoryId,ImagesForms")] Property @property)
        {
            
                property.Category = _context.Categories.Find(property.CategoryId);
            if (ModelState.IsValid)
            {
                property.Images = await FileManager.ConvertImagesAsync(property.ImagesForms);
                              
                _context.Add(@property);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create","Verifications",
                    new {propertyId = property.Id,ownerId = property.OwnerId });
            }

            return View(@property);
        }

        // GET: Properties/Edit/5
        [Authorize(Roles = RoleNames.PropertyOwner)]
        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var @property = await _context.Properties.FindAsync(id);

            if (@property == null || property.OwnerId != UserId)
            {
                return NotFound();
            }
            
            return View(@property);
        }

        // POST: Properties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Price,Comodities,OwnerId")] Property @property)
        {
            if (id != @property.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@property);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertyExists(@property.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["OwnerId"] = new SelectList(_context.PropertyManagers, "PropertyManagerId", "PropertyManagerId", @property.OwnerId);
            return View(@property);
        }

        // GET: Properties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @property = await _context.Properties
                .Include(p => p.PropertyManager)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@property == null)
            {
                return NotFound();
            }

            return View(@property);
        }

        // POST: Properties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @property = await _context.Properties.FindAsync(id);

            
            _context.Properties.Remove(@property);
            //_context.Images.7

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropertyExists(int id)
        {
            return _context.Properties.Any(e => e.Id == id);
        }
    }
}
