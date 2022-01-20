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
using TP_PWEB.Services;
using static TP_PWEB.Models.RoleNames;

namespace TP_PWEB.Controllers
{
    [Authorize(Roles = PropertyOwner + "," + PropertyEmployee)]

    public class VerificationReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VerificationReservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        

        // GET: VerificationReservations
        public async Task<IActionResult> Index(int reservationId)

        {
           

            if (!await _context.IsEmployeeReservationAsync(reservationId))
                return NotFound();

            var verifications = _context.VerificationReservations
            .Where(v => v.ReservationId == reservationId).Include(v => v.Verification);


          
            return View(await verifications.ToListAsync());
        }

        // GET: VerificationReservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var verificationReservation = await _context.VerificationReservations
                .Include(v => v.Reservation)
                .Include(v => v.Verification)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (verificationReservation == null)
            {
                return NotFound();
            }

            return View(verificationReservation);
        }
        /*
        // GET: VerificationReservations/Create
        public IActionResult Create()
        {
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "Id", "ClientId");
            ViewData["VerificationId"] = new SelectList(_context.Verifications, "Id", "Name");
            return View();
        }

        // POST: VerificationReservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VerificationId,ReservationId,IsChecked,Observation")] VerificationReservation verificationReservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(verificationReservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "Id", "ClientId", verificationReservation.ReservationId);
            ViewData["VerificationId"] = new SelectList(_context.Verifications, "Id", "Name", verificationReservation.VerificationId);
            return View(verificationReservation);
        }
        */
        // GET: VerificationReservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var verificationReservation = await _context.
                VerificationReservations
                .Include(v => v.Images)
                .Include(v => v.Verification)
                .FirstOrDefaultAsync(v => v.Id == id);
            if (verificationReservation == null)
            {
                return NotFound();
            }
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "Id", "ClientId", verificationReservation.ReservationId);
            ViewData["VerificationId"] = new SelectList(_context.Verifications, "Id", "Name", verificationReservation.VerificationId);
            return View(verificationReservation);
        }

        // POST: VerificationReservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VerificationId,ReservationId,IsChecked,Observation,ImagesForms")] VerificationReservation verificationReservation)
        {
            if (id != verificationReservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    verificationReservation.Images = await FileManager.ConvertImagesAsync(verificationReservation.ImagesForms);
                    _context.Update(verificationReservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VerificationReservationExists(verificationReservation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Edit", "Reservations", new { id = verificationReservation.ReservationId });
            }
           
            return View(verificationReservation);
        }
        /*
        // GET: VerificationReservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var verificationReservation = await _context.VerificationReservations
                .Include(v => v.Reservation)
                .Include(v => v.Verification)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (verificationReservation == null)
            {
                return NotFound();
            }

            return View(verificationReservation);
        }

        // POST: VerificationReservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var verificationReservation = await _context.VerificationReservations.FindAsync(id);
            _context.VerificationReservations.Remove(verificationReservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        */
        private bool VerificationReservationExists(int id)
        {
            return _context.VerificationReservations.Any(e => e.Id == id);
        }
        
    }
}
