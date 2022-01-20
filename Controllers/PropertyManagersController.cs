using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TP_PWEB.Data;
using TP_PWEB.Models;

namespace TP_PWEB.Controllers
{
    public class PropertyManagersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PropertyManagersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PropertyManagers
        public async Task<IActionResult> Index()
        {
            var managers = await _context.PropertyManagers.ToListAsync();

            foreach (var manager in managers)
            {
                manager.User = await _context.Users.FindAsync(manager.PropertyManagerId);
            }

            return View(managers);
        }

        // GET: PropertyManagers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyManager = await _context.PropertyManagers
                .FirstOrDefaultAsync(m => m.PropertyManagerId == id);
            propertyManager.User = await _context.Users.FindAsync(propertyManager.PropertyManagerId);

            
            if (propertyManager == null)
            {
                return NotFound();
            }

            return View(propertyManager);
        }

        // GET: PropertyManagers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyManager = await _context.PropertyManagers.FindAsync(id);
            propertyManager.User = await _context.Users.FindAsync(propertyManager.PropertyManagerId);

            if (propertyManager == null)
            {
                return NotFound();
            }
            return View(propertyManager);
        }

        // POST: PropertyManagers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PropertyManagerId")] PropertyManager propertyManager, [Bind("UserName")] IdentityUser user)
        {
            if (id != propertyManager.PropertyManagerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                propertyManager.User = user;
                try
                {
                    _context.Update(propertyManager);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertyManagerExists(propertyManager.PropertyManagerId))
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
            return View(propertyManager);
        }

        // GET: PropertyManagers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyManager = await _context.PropertyManagers.FindAsync(id);
            propertyManager.User = await _context.Users.FindAsync(propertyManager.PropertyManagerId);


            propertyManager.User = await _context.Users.FindAsync(propertyManager.PropertyManagerId);
            if (propertyManager == null)
            {
                return NotFound();
            }

            return View(propertyManager);
        }

        // POST: PropertyManagers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var propertyManager = await _context.PropertyManagers.FindAsync(id);
            _context.PropertyManagers.Remove(propertyManager);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropertyManagerExists(string id)
        {
            return _context.PropertyManagers.Any(e => e.PropertyManagerId == id);
        }
    }
}
