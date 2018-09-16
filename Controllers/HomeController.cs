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
            List<Dojo> Dojos = _dContext.dojos.ToList();
            List<Ninja> Ninjas = _dContext.ninjas.Include(d => d.Dojos).ToList();
            ViewBag.dojos = Dojos;
            ViewBag.ninjas = Ninjas;
            return View();
        }

        [HttpPost("AddNinja")]
        public IActionResult AddNinja(Ninja nin)
        {
            Ninja ninja = new Ninja
            {
                name = nin.name,
                level = nin.level,
                description = nin.description,
                dojo_id = nin.dojo_id
            };
            _dContext.ninjas.Add(ninja);
            _dContext.SaveChanges();
            return RedirectToAction("Ninjas");
        }

        [HttpPost("AddDojo")]
        public IActionResult AddDojo(Dojo doj)
        {
            Dojo dojo = new Dojo
            {
                name = doj.name,
                location = doj.location,
                description = doj.description,
            };
            _dContext.dojos.Add(dojo);
            _dContext.SaveChanges();
            return RedirectToAction("Dojos");
        }

        [HttpGet("Ninja/{ninja_id}")]
        public IActionResult Ninja(int ninja_id)
        {
            Ninja ninja = _dContext.ninjas.Include(d => d.Dojos).Where(n => n.ninja_id == ninja_id).SingleOrDefault();
            ViewBag.ninja = ninja;
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
