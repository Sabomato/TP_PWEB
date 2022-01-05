using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TP_PWEB.Data;
using TP_PWEB.Models;

namespace TP_PWEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _data;
        private readonly UserManager<IdentityUser> _users;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext App)
        {
            _data = App;
            //_users.FindByIdAsync(User.Identity.)
            //_data.Properties.Include( p => p.Reservations.Where( r => r.))
            
            _logger = logger;
            
        }

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
