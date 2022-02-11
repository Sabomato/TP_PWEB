using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TP_PWEB.Data;
using TP_PWEB.Models;
using static TP_PWEB.Models.RoleNames;
namespace TP_PWEB.Controllers
{
    
    [Authorize]
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public ReservationsController(ApplicationDbContext context,UserManager<IdentityUser> userManager)
        {

            _context = context;
            _userManager = userManager;

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

            var client = await _context.Clients
                .Where(c => c.ClientId == clientId)
                .FirstOrDefaultAsync();

            if (client == null)
                return null;


            var reservations = _context.Reservations
                .Where(r => r.ClientId.Equals(clientId))
                .Include(r => r.Property)
                .Include(r => r.VerificationReservations)
                .Include(r => r.Client);

            client.User = await _context.Users.FindAsync(clientId);

            ViewData["Title"] = "Reservations Made by " + client.User.UserName;

            return reservations;

        }

        private async Task<IQueryable<Reservation>> IndexPropertyAsync(int propertyId)
        {
            var property = await _context.Properties.FindAsync(propertyId);          

            if (property == null)
                return null;

            var reservations = _context.Reservations
                .Where(r => r.PropertyId == propertyId)
                .Include(r => r.Property)
                .Include(r => r.VerificationReservations)
                .Include(r => r.Client);          

            ViewData["Title"] = "Reservations in Property " + property.Title;

            return reservations;

        }

        [ActionName("Index")]
        // GET: Reservations
        public async Task<IActionResult> Index(int? propertyId,string clientId)
        {
            IQueryable<Reservation> reservations;
            if (clientId == null)
            {
                if (!await _context.IsEmployeeOrOwnerAsync((int)propertyId))
                    return Unauthorized();

                reservations = await IndexPropertyAsync((int)propertyId);

                var reservationList = await reservations.ToListAsync();

                foreach (var reservation in reservationList)
                {
                    reservation.Client.User = await _context.Users.FindAsync(reservation.ClientId);
                }
                return View(reservationList);

            }
            else if(propertyId == null)
            {
                //Clientes podem ver reservas uns dos outros
                reservations = await IndexClientAsync(clientId);
            }
            else
            {
                reservations = await IndexClientProperty((int)propertyId, clientId);

                var reservationList = await reservations.ToListAsync();

                foreach (var reservation in reservationList)
                {
                    reservation.Client.User = await _context.Users.FindAsync(reservation.ClientId);
                }
                return View(reservationList);
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

            var reservation = await GetFullReservationAsync((int)id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        [ActionName("Create")]
        //[Authorize(Roles = RoleNames.Client)]
        public async Task<IActionResult> CreateAsync(int propertyId,string clientId)
        {
            if (!_context.IsCurrentUser(clientId))
                return Unauthorized();

            var property = await _context.GetPropertyAsync(propertyId);

            if (property == null)
                return NotFound();

            Reservation reservation = new Reservation()
            {
                PropertyId = property.Id,
                ClientId = clientId,
                Property = property,
                IsAccepted = false,
                IsDelivered = false,
                IsReceived = false,
                IsAvailable = true,
                StartDate = DateTime.Now.Date,
                EndDate = DateTime.Now.AddDays(1).Date
            };

            ViewData["Title"] = "Make a Reservation in " + property.Title;

            return View(reservation);
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
                if (await IsAvailable(reservation))
                {
                    await FillVerificationsAsync(reservation);
                    _context.Add(reservation);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index),new { clientId = reservation.ClientId });
                }
                reservation.IsAvailable = false;
            }


            return View(reservation);
        }

        private async Task<bool> IsAvailable(Reservation reservation)
        {
            var available = !await _context.Reservations
                .Where(r => r.PropertyId == reservation.PropertyId)
                .Where(r => r.StartDate < reservation.EndDate && r.EndDate > reservation.StartDate)
                .AnyAsync();

            return available;
 
        }

        private async Task FillVerificationsAsync(Reservation reservation)
        {
            List<Verification> verifications = await _context.Verifications
                .Where(v => v.PropertyId == reservation.PropertyId)
                .ToListAsync();

            reservation.VerificationReservations = new List<VerificationReservation>();

            foreach(var verification in verifications)
            {
                VerificationReservation verificationReservation = new VerificationReservation()
                {
                    IsChecked = false,
                    Verification = verification
                };

                reservation.VerificationReservations.Add(verificationReservation);
            }

        }

        public async Task<Reservation> GetFullReservationAsync(int reservationId)
        {
            var reservation = await _context.Reservations
                .Include(r=>r.Property)
                .Include(r=>r.StayEvaluation)
                .Include(r=>r.Client)
                .Include(r=>r.ClientEvaluation)
                .FirstOrDefaultAsync(r => r.Id == reservationId);

            if (reservation == null)
                return null;

            var clientId = reservation.ClientId;

            reservation.VerificationReservations = await _context
                .VerificationReservations
                .Where(v => v.ReservationId == reservationId)
                .Include(v => v.Verification)
                .ToListAsync();

            reservation.Client.User = await _context.Users.FindAsync(clientId);
            
            
            
            

            return reservation;

        }

        // GET: Reservations/Edit/5
        [Authorize(Roles = PropertyOwner + "," + PropertyEmployee)]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await GetFullReservationAsync((int)id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = PropertyOwner + "," + PropertyEmployee)]
        public async Task<IActionResult> Edit([Bind("Id,IsAccepted,IsDelivered,IsReceived,StartDate,EndDate,PropertyId,ClientId")] Reservation reservation)
        {


            if (reservation == null)
                return NotFound();
            
            if (!await _context.IsEmployeeOrOwnerAsync(reservation.PropertyId))
                return Unauthorized();

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
                return RedirectToAction(nameof(Index),new { propertyId = reservation.PropertyId });
            }

            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await GetFullReservationAsync((int)id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id,string clientId)
        {
            var reservation = await GetFullReservationAsync(id);
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index),new { clientId });
        }

        private bool ReservationExists(int id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
