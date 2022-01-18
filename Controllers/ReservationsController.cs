using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TP_PWEB.Data;
using TP_PWEB.Models;

namespace TP_PWEB.Controllers
{
    //Adicionar verficação de login
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        private async Task<Reservation> GetReservationAsync(int? reservationId)
        {
            return reservationId != null ? await _context.Reservations.FindAsync(reservationId) : null;
        }

        private async Task<IQueryable<Reservation>> IndexClientProperty(int propertyId, string clientId)
        {
            var client = await _context.Clients.FindAsync(clientId);

            if (client == null)
                return null;


            var property = await _context.Properties.FindAsync(propertyId);

            if (property == null)
                return null;

            //ver se == com strings dá
            var reservations = _context.Reservations
                .Where(r => r.ClientId == clientId && r.PropertyId == propertyId)
                .Include(r => r.Property)
                .Include(r => r.Client);


            ViewData["Title"] = "Reservations Made by " + client.User.UserName + " on " + property.Title;
            return reservations;

        }

        private async Task<IQueryable<Reservation>> IndexClientAsync(string clientId)
        {

            var client = await _context.Clients.FindAsync(clientId);

            if (client == null)
                return null;


            var reservations = _context.Reservations
                .Where(r => r.ClientId.Equals(clientId))
                .Include(r => r.Property)
                .Include(r => r.Client);

            ViewData["Title"] = "Reservations Made by " + client.User.UserName;

            return reservations;

        }

        private async Task<IQueryable<Reservation>> IndexPropertyAsync(int propertyId)
        {
            var property = await _context.Properties.FindAsync(propertyId);          

            if (property == null)
                return null;
            
            var reservations =  _context.Reservations
                .Where(r => r.PropertyId == propertyId)
                .Include(r => r.Property)
                .Include(r => r.Client);

            ViewData["Title"] = "Reservations in Property " + property.Title;

            return reservations;

        }
        // GET: Reservations
        public async Task<IActionResult> Index(int? propertyId,string clientId)
        {
            IQueryable<Reservation> reservations = null;


            if(clientId == null)
            {
                if (!await _context.IsEmployeeOrOwnerAsync((int)propertyId))
                    return Unauthorized();

                reservations = await IndexPropertyAsync((int)propertyId);

            }else if(propertyId == null)
            {
                //Clientes podem ver reservas uns dos outros
                reservations = await IndexClientAsync(clientId);
            }
            else
            {
                reservations = await IndexClientProperty((int)propertyId, clientId);
            }

            if (reservations == null)
                return NotFound();
            else 
                return View(await reservations.ToListAsync());

        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Client)
                .Include(r => r.Property)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create(int propertyId,int clientId)
        {

            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId");
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Comodities");

            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IsDelivered,IsReceived,StartDate,EndDate,PropertyId,ClientId")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId", reservation.ClientId);
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Comodities", reservation.PropertyId);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId", reservation.ClientId);
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Comodities", reservation.PropertyId);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IsDelivered,IsReceived,StartDate,EndDate,PropertyId,ClientId")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
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
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "ClientId", reservation.ClientId);
            ViewData["PropertyId"] = new SelectList(_context.Properties, "Id", "Comodities", reservation.PropertyId);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservations
                .Include(r => r.Client)
                .Include(r => r.Property)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
