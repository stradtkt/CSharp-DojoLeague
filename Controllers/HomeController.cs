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
            List<Ninja> rogues = _dContext.ninjas.Include(d => d.Dojos).Where(d => d.dojo_id == 3).ToList();
            ViewBag.rogues = rogues;
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

        [HttpGet("Dojo/{dojo_id}")]
        public IActionResult Dojo(int dojo_id)
        {
            Dojo dojo = _dContext.dojos.Where(d => d.dojo_id == dojo_id).SingleOrDefault();
            List<Ninja> ninjas = _dContext.ninjas.Include(d => d.Dojos).Where(d => d.dojo_id == dojo_id).ToList();
            List<Ninja> rogues = _dContext.ninjas.Include(d => d.Dojos).Where(d => d.dojo_id == 3).ToList();
            ViewBag.rogues = rogues;
            ViewBag.ninjas = ninjas;
            ViewBag.dojo = dojo;
            return View();
        }
        [Route("Dojo/{dojo_id}/Banish/{ninja_id}")]
        public IActionResult Banish(int dojo_id, int ninja_id)
        {
            Ninja ninja = _dContext.ninjas.SingleOrDefault(n => n.ninja_id == ninja_id);
            ninja.dojo_id = 3;
            _dContext.SaveChanges();
            return Redirect("/Dojo/" + dojo_id);
        }

        [Route("Dojo/{dojo_id}/Recruit/{ninja_id}")]
        public IActionResult Recruit(int dojo_id, int ninja_id)
        {
            Ninja ninja = _dContext.ninjas.SingleOrDefault(n => n.ninja_id == ninja_id);
            ninja.dojo_id = dojo_id;
            _dContext.SaveChanges();
            return Redirect("/Dojo/"+ dojo_id);
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
