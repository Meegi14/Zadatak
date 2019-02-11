using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Remotion.Linq.Clauses;
using Zadatak.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Zadatak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KancelarijaController : Controller
    {
        private readonly ZadatakContext _context;

        public KancelarijaController(ZadatakContext context)
        {
            _context = context;

        }
        
        /// <summary>
        /// Akcija za dodavanje nove kancelarije.
        /// </summary>
        /// <param name="input">Podaci o novoj kancelariji</param>
        /// <returns>Novu kancelariju.</returns>
        [HttpPost("NovaKancelarija")]
        public IActionResult NovaKancelarija(Kancelarija input)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var kancelarija = new Kancelarija {Ime = input.Ime};

                    if (input != null)
                    {
                        _context.Kancelarijas.Add(kancelarija);
                        _context.SaveChanges();
                        transaction.Commit();


                        return Ok();
                    }
                        

                }

                catch (Exception e)
                {
                    return BadRequest();
                }

                return BadRequest();
            }

           
        }

        /// <summary>
        /// Akcija za izlistavanje svih kancelarija.
        /// </summary>
        /// <returns>Lista kancelarija.</returns>
        [HttpGet]
        public IActionResult SveKancelarije()
        {
            var kancelarije = _context.Kancelarijas;
            var kancelarija = kancelarije.Select(n => new
                {Ime = n.Ime, Osobe = n.Osobas.Select(y => y.Ime + " " + y.Prezime)});

            if (kancelarija.Any())
            {
                return Ok(kancelarije.ToList());
            }

            return NotFound();

        }

        /// <summary>
        /// Pretraga kancelarija po Id.
        /// </summary>
        /// <param name="id">Id kancelarije.</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult KancelarijaPoId(long id)
        {
            var kancelarije = _context.Kancelarijas.Find(id);
            if (kancelarije == null)
            {
                return BadRequest("Not Found");
            }

            var kancelarija = _context.Kancelarijas;
            var kancelarijaQuary = _context.Kancelarijas.Where(x => x.Id == id);

            return Ok(kancelarijaQuary.ToList());
        }

        /// <summary>
        /// Modifikovanje podataka o kancelarijama po id.
        /// </summary>
        /// <param name="id">Id kancelarije</param>
        /// <param name="input">Novi podaci o kancelariji.</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put(long id, KancelarijaDto input)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var kancelarije = _context.Kancelarijas.Find(id);

                    if (kancelarije != null)
                    {
                        kancelarije.Ime = input.Ime;
                        _context.SaveChanges();
                        transaction.Commit();

                        return Ok("Nova Kancelarija");
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
        ///Akcija za brisanje kancelarija.
        /// </summary>
        /// <param name="id">Id kancelarije koju zelimo da izbrisemo.</param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult BrisanjeKancelarije(long id)
        {
            var kancelarija = _context.Kancelarijas.Find(id);

            if (kancelarija != null)
            {
                try
                {
                    _context.Kancelarijas.Remove(kancelarija);
                    _context.SaveChanges();

                    return Ok("Uspjesno izbrisana kancelarija.");
                }

                catch (Exception e)
                {
                    return BadRequest();
                }
                
            }

            return NoContent();

            
        }


       
    }
}
