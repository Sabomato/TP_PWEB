using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TP_PWEB.Data;
using TP_PWEB.Models.Properties;
using TP_PWEB.Models;
using static TP_PWEB.Models.RoleNames;
namespace TP_PWEB.Controllers
{
    [Authorize(Roles = PropertyOwner + "," + PropertyEmployee)]

    public class PropertyOwnerController : Controller
    {
        
        private readonly ApplicationDbContext _context;
        private PropertyManager _owner;
        private UserManager<IdentityUser> _userManager;
        public PropertyOwnerController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
                   
            _context = context;
            _userManager = userManager;
        }

        private String getCurrentUserId()
        {
            var user = this.User;
            return _userManager.GetUserId(User);

        }

        // GET: PropertyOwnerController
        public async Task<IActionResult> Index()
        {
            var ownerId = getCurrentUserId();

            var properties = await _context.Properties
                .Where(p => p.PropertyManager.PropertyManagerId == ownerId)
                .ToListAsync();

            return View(properties);
        }

        // GET: PropertyOwnerController/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @property = await _context.Properties
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@property == null)
            {
                return NotFound();
            }

            return View(@property);
        }

        // GET: PropertyOwnerController/Create
        public async Task<IActionResult> CreateAsync()
        {
            List<Category> categories = await _context.Categories.ToListAsync();

            PropertyCreateViewModel model;

            if(categories != null)
                model = new PropertyCreateViewModel(categories);
            else
            {
                return RedirectToAction(nameof(Index));
            }
            model.OwnerId = getCurrentUserId();
            //model.PropertyManager

            return View(model);
        }

        // POST: PropertyOwnerController/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Price,Comodities,Category,ImagesForms")] PropertyCreateViewModel property)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@property);
                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(AddVerification));
            }
            return View(@property);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVerification([Bind("Id,Title,Description,Price,Comodities")] Property @property)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@property);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@property);
        }

        [HttpGet]
        public async Task<IActionResult> AddVerification(int PropertyId)
        {
            List<Verification> verifications = await _context.Verifications
                .Where( v => v.PropertyId == PropertyId)
                .ToListAsync();

            VerificationCreateViewModel verification;

            if (verifications != null)
            {
                verification = new VerificationCreateViewModel();
                verification.NewVerification.PropertyId = PropertyId;

            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
            
            //model.PropertyManager

            return View();
        }

        // GET: PropertyOwnerController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @property = await _context.Properties.FindAsync(id);
            if (@property == null)
            {
                return NotFound();
            }
            return View(@property);
        }

        // POST: PropertyOwnerController/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Price,Comodities")] Property @property)
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
            return View(@property);
        }

        // GET: PropertyOwnerController/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @property = await _context.Properties
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@property == null)
            {
                return NotFound();
            }

            return View(@property);
        }

        // POST: PropertyOwnerController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @property = await _context.Properties.FindAsync(id);
            _context.Properties.Remove(@property);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropertyExists(int id)
        {
            return _context.Properties.Any(e => e.Id == id);
        }
    }
}
