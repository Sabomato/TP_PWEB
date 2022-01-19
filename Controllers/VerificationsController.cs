using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TP_PWEB.Data;
using TP_PWEB.Models;
using TP_PWEB.Models.Properties;
using static TP_PWEB.Models.RoleNames;

namespace TP_PWEB.Controllers
{
    [Authorize(Roles = PropertyOwner)]

    public class VerificationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        
        public VerificationsController(ApplicationDbContext context)
        {
            _context = context;

        }
        
        private async Task<IActionResult> IndexProperty(int propertyId)
        {
          

            var property = await _context.GetPropertyAsync(propertyId);
            if (property == null)
                return NotFound();

            if (_context.UserId != property.OwnerId)
                return NotFound();

            var verifications = _context.Verifications
            .Where(p => p.PropertyId == property.Id && p.isDeleted == false);

            ViewData["Title"] = "Verification for " + property.Title;
            ViewData["propertyId"] = property.Id;
            ViewData["ownerId"] = property.OwnerId;
            return View(await verifications.ToListAsync());
        
        }



        // GET: Verifications
        public async Task<IActionResult> Index(int? propertyId)
        {
            if (propertyId != null)
            {
                return await IndexProperty((int)propertyId);
            }

            return NotFound();

            
        }

        // GET: Verifications/Details/5
        public async Task<IActionResult> Details(Property property,int? id)
        {

            if (_context.GetCurrentUserId() != property.OwnerId)
                return NotFound();

            if (id == null)
            {
                
                return NotFound();
            }

            var verification = await _context.Verifications
                .Include(v => v.Property)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (verification == null)
            {
                return NotFound();
            }

            return View(verification);
        }

        // GET: Verifications/Create
        
        [ActionName("Create")]
        public IActionResult Create(string ownerId, int propertyId)
        {

            if (_context.UserId != ownerId)
                return NotFound();
          
            Verification verification = new Verification()
            {
                PropertyId = propertyId,
                isDeleted = false
            };
            //verification.IsChecked = false;

            return View(verification);

        }

        // POST: Verifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IsAtExit,PropertyId")] Verification verification, bool addAnother)
        {
            if (ModelState.IsValid)
            {
                _context.Add(verification);
                await _context.SaveChangesAsync();
                
                return addAnother ?
                    RedirectToAction("Create") : RedirectToAction(nameof(Details),"Properties", new {id = verification.PropertyId});
            }
            
            return View(verification);
        }

        // GET: Verifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var verification = await _context.Verifications.FindAsync(id);
            if (verification == null)
            {
                return NotFound();
            }
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Comodities", verification.PropertyId);
            return View(verification);
        }

        // POST: Verifications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,IsAtExit,PropertyId")] Verification verification)
        {
            if (id != verification.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(verification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VerificationExists(verification.Id))
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
            return View(verification);
        }

        // GET: Verifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var verification = await _context.Verifications
                .Include(v => v.Property)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (verification == null)
            {
                return NotFound();
            }

            return View(verification);
        }

        // POST: Verifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var verification = await _context.Verifications.FindAsync(id);
            verification.isDeleted = true;
            _context.Verifications.Update(verification);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VerificationExists(int id)
        {
            return _context.Verifications.Any(e => e.Id == id);
        }


    }
}
