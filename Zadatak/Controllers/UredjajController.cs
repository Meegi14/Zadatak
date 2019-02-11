using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zadatak.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Zadatak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UredjajController : Controller
    {
        private readonly ZadatakContext _context;

        public UredjajController(ZadatakContext context)
        {
            _context = context;

        }

        /// <summary>
        /// Kreiranje uredjaja.
        /// </summary>
        /// <param name="input">Podaci o novom uredjaju.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult NoviUredjaj(UredjajDto input)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {

                    if (input != null)
                    {
                        var uredjaj = new Uredjaj {Ime = input.Ime, Id = input.Id};
                        _context.Uredjajs.Add(uredjaj);
                        _context.SaveChanges();
                        transaction.Commit();

                        return Ok();

                    }

                }

                catch (Exception e)
                {
                    return BadRequest();
                }
            }

            return BadRequest();
        }

        /// <summary>
        /// Izlistavanje svih uredjaja.
        /// </summary>
        /// <returns>Lista uredjaja.</returns>
        [HttpGet]
        public IActionResult SviUredjaji()
        {
            var uredjaji = _context.Uredjajs;
            var uredjajQuary =
                uredjaji.Select(n => new {Ime = n.Ime, Id = n.Id, OsobaUredjaj = n.Osoba.Ime + " " + n.Osoba.Prezime});

            if (uredjajQuary.Any())
            {
                return Ok(uredjajQuary.ToList());
            }

            return Ok();
        }

        /// <summary>
        ///Akcija vraca uredjaj sa datim Id.
        /// </summary>
        /// <param name="id">Id trazenog uredjaja.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult UredjajiPoId(long id)
        {
            var uredjaji = _context.Uredjajs.Find(id);
            if (uredjaji == null)
            {
                return NotFound("Ne postoji trazeni uredjaj.");
            }

            var uredjaj = _context.Uredjajs;
            var uredjajiQuary = _context.Uredjajs.Where(u => u.Id == id);
            return Ok(uredjajiQuary.ToList());
        }

        /// <summary>
        ///Mijenjanje podataka o uredjaju. 
        /// </summary>
        /// <param name="id">Id uredjanja koji zelimo da mijenjamo.</param>
        /// <param name="input">Novi podaci.</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult ModifikovaniUredjaj(long id, UredjajDto input)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var uredjaji = _context.Uredjajs.Find(id);

                    if (uredjaji != null)
                    {
                        uredjaji.Ime = input.Ime;
                        _context.SaveChanges();
                        transaction.Commit();

                        return Ok($"Uredjaj sa id: {id} je modifikovan.");
                    }

                }

                catch (Exception e)
                {
                    return BadRequest();
                }
            }

            return NotFound();
        }


        /// <summary>
        /// Brisanje osobe sa datim Id.
        /// </summary>
        /// <param name="id">Id osobe koju zelimo da izbrisemo.</param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult BrisanjeUredjaja(long id)
        {
            var uredjaj = _context.Uredjajs.Find(id);

            if (uredjaj != null)
            {
                try
                {
                    _context.Uredjajs.Remove(uredjaj);
                    _context.SaveChanges();

                    return Ok($"Uredjaj sa id:{id} je uspjesno izbrisan.");
                }

                catch (Exception e)
                {
                    return BadRequest();
                }

            }

            return NotFound();
        }
    }

}
