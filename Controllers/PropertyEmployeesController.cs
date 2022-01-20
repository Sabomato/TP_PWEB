using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TP_PWEB.Data;
using TP_PWEB.Helpers;
using TP_PWEB.Models;
using TP_PWEB.Models.PropertyEmployees;
using TP_PWEB.Models.Users;

namespace TP_PWEB.Controllers
{
    [Authorize(Roles = RoleNames.PropertyOwner)]
    public class PropertyEmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PropertyEmployeesController(ApplicationDbContext context, 
            UserManager<IdentityUser> userManager
            )
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: PropertyEmployees
        public async Task<IActionResult> Index()
        {
            var employees = _context.PropertyEmployees
                .Include(p => p.PropertyManager)
                .Where(p => p.PropertyManagerId == _context.UserId).ToList();

            
            
            foreach(var employee in employees )
            {
                employee.User = await _context.Users.FindAsync(employee.PropertyEmployeeId);
            }


            return View(employees);
        }

        // GET: PropertyEmployees/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.PropertyEmployees.FindAsync(id);
            
            employee.User = await _context.Users.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: PropertyEmployees/Create
        public IActionResult Create()
        {



            PropertyEmployeeCreateVM propertyEmployee = new PropertyEmployeeCreateVM()
            {
                PropertyManagerId = _context.UserId,
                PropertyEmployeeId = "dummyKey",
            };

            return View(propertyEmployee);
        }

        // POST: PropertyEmployees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PropertyEmployeeId,PropertyManagerId,Password,Email")] PropertyEmployeeCreateVM propertyEmployee)
        {


            if (ModelState.IsValid)
            {
                if (!_context.IsCurrentUser(propertyEmployee.PropertyManagerId))
                    return Unauthorized();


                var user = new IdentityUser()
                {
                    Email = propertyEmployee.Email,
                    UserName = propertyEmployee.Email,
                };
                
                propertyEmployee.User =  await DatabaseSeeding.CreateUserWithRole(user, _userManager, RoleNames.PropertyEmployee,propertyEmployee.Password);

                if (propertyEmployee.User == null)
                {
                    ModelState.AddModelError(string.Empty, "That email is already registered!");
                    return View(propertyEmployee);
                }
                propertyEmployee.PropertyEmployeeId = propertyEmployee.User.Id;
                _context.Add(propertyEmployee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(propertyEmployee);
        }

        // GET: PropertyEmployees/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            PropertyEmployeeEditVM propertyEmployee = new PropertyEmployeeEditVM();
            var employee = await _context.PropertyEmployees.FindAsync(id);
            employee.User = await _context.Users.FindAsync(id);

            propertyEmployee.SetEmployee(employee);

            if (propertyEmployee == null)
            {
                return NotFound();
            }
           
            return View(propertyEmployee);
        }

        // POST: PropertyEmployees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("UserName,PropertyEmployeeId,PropertyManagerId,Email,Password,NewPassword")] PropertyEmployeeEditVM employee)
        {
            if (!await _context.IsEmployeeAsync(employee.PropertyEmployeeId))
                return Unauthorized();

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(employee.PropertyEmployeeId);
                    employee.User = user;
                    
                    
                    if (employee.Password != null)
                    {

                        IdentityResult result;
                        result = await _userManager.ChangePasswordAsync(user, employee.Password, employee.NewPassword);
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError(string.Empty, "Invalid parameters attempt.");
                            return View(employee);
                        }
                      
                    }
                    if(employee.Email != null)
                        employee.User.Email = employee.Email;
                    if (employee.UserName != null)
                        employee.User.UserName = employee.UserName;
                    _context.PropertyEmployees.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertyEmployeeExists(employee.PropertyEmployeeId))
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
           
            return View(employee);
        }

        // GET: PropertyEmployees/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.PropertyEmployees.FindAsync(id);
            employee.User = await _context.Users.FindAsync(id);

            if (employee== null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: PropertyEmployees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var propertyEmployee = await _context.PropertyEmployees.FindAsync(id);
            _context.PropertyEmployees.Remove(propertyEmployee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropertyEmployeeExists(string id)
        {
            return _context.PropertyEmployees.Any(e => e.PropertyEmployeeId == id);
        }
    }
}
