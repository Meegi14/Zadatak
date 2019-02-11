using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.EntityFrameworkCore.Design;
using Zadatak.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Zadatak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OsobaController : Controller
    {
        private readonly ZadatakContext _context;
        public OsobaController(ZadatakContext context)
        {
            _context = context;

        }

       /// <summary>
       /// Kreiranje nove osobe.
       /// </summary>
       /// <param name="input">Novi podaci o osobi.</param>
       /// <returns></returns>
        [HttpPost]
        public IActionResult NovaOsoba(OsobaDto input)
        {
            using (var transaction = _context.Database.BeginTransaction())

            {
                try
                {

                    if (input != null)
                    {
                        var osoba = new Osoba
                        {
                            Ime = input.Ime,
                            Prezime = input.Prezime,
                            KancelarijaId = input.KancelarijaId
                        };

                        _context.Osobas.Add(osoba);
                        _context.SaveChanges();

                        var novaOsoba = _context.Osobas.Last();
                        var novaOsobaKancelarija = novaOsoba.KancelarijaId;

                        var kancelarijaIme = _context.Kancelarijas.Where(k => k.Id == novaOsobaKancelarija)
                            .FirstOrDefault();

                        var lista = kancelarijaIme.Osobas;
                        lista.Add(osoba);
                        transaction.Commit();

                        return Ok("Nova osoba je kreirana.");

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
        /// Izlistavanje svih osoba.
        /// </summary>
        /// <returns>Listu osoba.</returns>
        [HttpGet]
        public IActionResult SveOsobe()
        {
            var osobe = _context.Osobas;
            var osoba = osobe.Select(n => new
            {
                Ime = n.Ime, Prezime = n.Prezime, Kancelarija = n.KancelarijaId, Uredjaj = n.Uredjajs.Select(y => y.Ime)

            });

            if (osoba.Any())
            {
                return Ok(osobe.ToList());
            }

            return NotFound();
        }

        /// <summary>
        /// Mijenjanje podataka o osobi.
        /// </summary>
        /// <param name="input">Novi podaci.</param>
        /// <param name="id">Id osobe.</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult UpdateOsoba(OsobaDto input, long id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var osoba = _context.Osobas.Find(id);
                    if (osoba != null)
                    {
                        osoba.Ime = input.Ime;
                        osoba.Prezime = input.Prezime;
                        osoba.KancelarijaId = input.KancelarijaId;

                        _context.SaveChanges();
                        transaction.Commit();

                        return Ok($"Osoba sa id: {id} je modifikovana.");
                    }

                }

                catch (Exception e)
                {
                    return NoContent();
                }
                
            }

            return BadRequest();
        }


        /// <summary>
        /// Brisanje osobe.
        /// </summary>
        /// <param name="id">Id osobe koju zelimo da izbrisemo.</param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult BrisanjeOsobe(long id)
        {
            var osobe = _context.Osobas.Find(id);

            if (osobe != null)
            {
                try
                {
                    _context.Osobas.Remove(osobe);
                    _context.SaveChanges();

                    return BadRequest();
                }

                catch (Exception e)
                {
                    return BadRequest();
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Osoba sa datim Id.
        /// </summary>
        /// <param name="id">Id osobe.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult OsobaId(long id)
        {
            var osobe = _context.Osobas.Find(id);
            if (osobe == null)
            {
                return BadRequest("Not Found.");
            }

            var osoba = _context.Osobas;
            var osobaQuary = _context.Osobas.Where(x => x.Id == id).Select(s => new
            {
                Ime = s.Ime, Prezime = s.Prezime, Kancelarija = s.Kancelarija.Ime

            });

            return Ok(osobaQuary.ToList());
        }
    }
}
