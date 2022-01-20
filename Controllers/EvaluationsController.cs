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

    public class EvaluationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EvaluationsController(ApplicationDbContext context)
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

        private async Task<List<Reservation>> GetClientEvaluationsAsync(string clientId)
        {

            var client = await _context.Clients.FindAsync(clientId);

            if (client == null)
                return null;


            var reservations = await _context
                .Reservations
                .Where(r => r.ClientId == clientId)
                .Include(r => r.StayEvaluation).ToListAsync();

            ViewData["Title"] = "Reservations Made by " + client.User.UserName;

            return reservations;

        }
        public async Task<ICollection<Evaluation>> GetPropertyEvaluationsAsync(int propertyId)
        {
            var reservations = await _context
                .Reservations
                .Where(r => r.PropertyId == propertyId)
                .Include(r => r.StayEvaluation).ToListAsync();

            if (reservations == null)
                return null;

            List<Evaluation> evaluations = new List<Evaluation>();
            reservations.ForEach(
                value => {

                    value.StayEvaluation.StayTime = (value.EndDate - value.StartDate).Days;
                    value.StayEvaluation.Username = value.Client.User.UserName;
                    evaluations.Add(value.StayEvaluation);

                }
            );
            ViewData["Title"] = "Commentary ";
            return evaluations;

        }

        private async Task<IQueryable<Reservation>> GetReservationEvaluationsAsync(int propertyId)
        {
            var property = await _context.Properties.FindAsync(propertyId);

            if (property == null)
                return null;

            var reservations = _context.Reservations
                .Where(r => r.PropertyId == propertyId)
                .Include(r => r.Property)
                .Include(r => r.Client);



            ViewData["Title"] = "Reservations in Property " + property.Title;

            return reservations;

        }
        // GET: Reservations
        public async Task<IActionResult> Index(int? propertyId, string clientId, int? reservationId)
        {
            IQueryable<Reservation> reservations = null;


            if (clientId == null)
            {
                if (!await _context.IsEmployeeOrOwnerAsync((int)propertyId))
                    return Unauthorized();

                //reservations = await IndexPropertyAsync((int)propertyId);

            }
            else if (propertyId == null)
            {
                //Clientes podem ver reservas uns dos outros
                //reservations = await IndexClientAsync(clientId);
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


        // GET: Evaluations/Details/5
        public async Task<IActionResult> Details(int? id, string username)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluation = await _context.Evaluations
                .FirstOrDefaultAsync(m => m.Id == id);

            if(username != null)
            {
                evaluation.Username = username;
                evaluation.IsClient = true;
            }
            evaluation.IsClient = false; 

            if (evaluation == null)
            {
                return NotFound();
            }

            return View(evaluation);
        }

        // GET: Evaluations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Evaluations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Rating,Commentary,ReservationId,IsClient")] Evaluation evaluation)
        {


            if (ModelState.IsValid)
            {
                var reservation = await _context.Reservations.FindAsync(evaluation.ReservationId);

                if (reservation == null)
                    return NotFound();

                if (evaluation.IsClient)
                {
                    reservation.ClientEvaluation = evaluation;
                }

                reservation.StayEvaluation = evaluation;

                _context.Reservations.Update(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ReservationsController.Details),"Reservations",new { id = evaluation.ReservationId});
            }
            return View(evaluation);
        }

        // GET: Evaluations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluation = await _context.Evaluations.FindAsync(id);
            if (evaluation == null)
            {
                return NotFound();
            }
            return View(evaluation);
        }

        // POST: Evaluations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Rating,Commentary")] Evaluation evaluation)
        {
            if (id != evaluation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evaluation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EvaluationExists(evaluation.Id))
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
            return View(evaluation);
        }

        // GET: Evaluations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluation = await _context.Evaluations
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evaluation == null)
            {
                return NotFound();
            }

            return View(evaluation);
        }

        // POST: Evaluations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evaluation = await _context.Evaluations.FindAsync(id);
            _context.Evaluations.Remove(evaluation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EvaluationExists(int id)
        {
            return _context.Evaluations.Any(e => e.Id == id);
        }
    }
}
