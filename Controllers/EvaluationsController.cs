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
                else
                    reservation.StayEvaluation = evaluation;


                _context.Reservations.Update(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ReservationsController.Details),"Reservations",new { id = evaluation.ReservationId});
            }
            return View(evaluation);
        }

        private bool EvaluationExists(int id)
        {
            return _context.Evaluations.Any(e => e.Id == id);
        }
    }
}
