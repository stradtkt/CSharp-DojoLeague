using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DojoLeague.Models;
using Microsoft.EntityFrameworkCore;

namespace DojoLeague.Controllers
{
    public class HomeController : Controller
    {
        private DojoContext _dContext;
        public HomeController(DojoContext context)
        {
            _dContext = context;
        }
        [HttpGet("")]
        public IActionResult Ninjas()
        {
            List<Dojo> Dojos = _dContext.dojos.ToList();
            List<Ninja> Ninjas = _dContext.ninjas.Include(d => d.Dojos).ToList();
            ViewBag.dojos = Dojos;
            ViewBag.ninjas = Ninjas;
            return View();
        }
        [HttpGet("Dojos")]
        public IActionResult Dojos()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
