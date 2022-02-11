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

        public async Task<double> GetRatingAsync(Property property)
        {
            double rating = 0;
            List<Reservation> reservations = await _context.Reservations
                .Where(r => r.PropertyId == property.Id)
                .Include(r => r.ClientEvaluation).ToListAsync();

            int count = 0;
            foreach (var reservation in reservations)
            {
                if(reservation.ClientEvaluation != null)
                {
                    rating += reservation.ClientEvaluation.Rating;
                    ++count;
                }
            }
            //Retorna Nan se property.Reservations.Count == 0 (0/0), mas faz sentido no contexto
            return rating /  count;
        }


        
        // GET: Properties
        public async Task<IActionResult> Index()
        {
            List<Property> properties;

            if (User.IsInRole(RoleNames.PropertyOwner)) {
                properties =await  _context.Properties
                    .Where(p => p.PropertyManager.PropertyManagerId == UserId)
                    .Include(p => p.Reservations)
                    .Include(p=>p.Category)
                    .ToListAsync();
                ViewData["Title"] = "Your ";
            }
            else
            {
                properties =await _context.Properties.ToListAsync();
                ViewData["Title"] = "All ";
            }

            foreach (var property in properties)
            {

                property.Rating = await GetRatingAsync(property);

                property.CoverImage = await _context.Images.FirstAsync(i => i.Property.Id == property.Id);
            }
            ViewData["Title"] += "Properties";
            return View(properties);
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

       public async Task<ICollection<Evaluation>> GetCommentaryAsync(int propertyId)
        {
            var reservations = await _context
                .Reservations
                .Where(r => r.PropertyId == propertyId)
                .Include(r => r.ClientEvaluation)
                .Include(r=>r.Client)
                .ToListAsync();
            
            List<Evaluation> evaluations = new List<Evaluation>();

            foreach(var reservation in reservations)
            {
                if (reservation.ClientEvaluation != null)
                {
                    reservation.ClientEvaluation.StayTime = (reservation.EndDate - reservation.StartDate).Days;
                    reservation.Client.User = await _context.Users.FindAsync(reservation.ClientId);
                    reservation.ClientEvaluation.Username = reservation.Client.User.UserName;
                    reservation.ClientEvaluation.IsClient = true;
                    evaluations.Add(reservation.ClientEvaluation);
                }

            }
            
            return evaluations;
                        
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
                .Include(p=>p.Images)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (@property == null)
            {
                return NotFound();
            }
            @property.CurrentClientId = _context.UserId;
            property.ClientEvaluations = await  GetCommentaryAsync((int)id);
            property.Rating = await  GetRatingAsync(property);
            return View(@property);
        }

        // GET: Properties/Create
        [Authorize(Roles = RoleNames.PropertyOwner)]
        [ActionName("Create")]
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


        public async Task<Property> GetFullProperty(int propertyId)
        {
            var property = await _context.Properties
                .Include(p => p.Category)
                .Include(p => p.PropertyManager)
                .Include(p => p.Images)
                .FirstOrDefaultAsync(p => p.Id == propertyId);
                

            if (property == null)
                return null;

            property.SetCategory(await _context.Categories.ToListAsync());

            return property;

        }


        // GET: Properties/Edit/5
        [Authorize(Roles = RoleNames.PropertyOwner)]
        public async Task<IActionResult> Edit(int? id)
        {

            if (!await _context.IsEmployeeOrOwnerAsync((int)id))
                return Unauthorized();

            if (id == null)
            {
                return NotFound();
            }

            var @property = await GetFullProperty((int)id);

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
        public async Task<IActionResult> Edit( [Bind("Id,Title,Description,Price,Comodities,OwnerId,CategoryId,ImagesForms")] Property @property)
        {

            if (property == null)
                return NotFound();

            if (_context.UserId != property.OwnerId)
                return Unauthorized();

            if (ModelState.IsValid)
            {
                try
                {
                   if(property.ImagesForms != null)
                    {
                        ICollection<Image> images = await FileManager.ConvertImagesAsync(property.ImagesForms);
                        property.Images = images;
                    }
                  
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
            
            return View(@property);
        }

        // GET: Properties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @property = await GetFullProperty((int)id);
            @property.ClientEvaluations = await GetCommentaryAsync((int)id);
            @property.Rating = await GetRatingAsync(@property);

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

            var images = await _context.Images.Where(i => i.PropertyId == id).ToListAsync();

            _context.RemoveRange(images);

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
