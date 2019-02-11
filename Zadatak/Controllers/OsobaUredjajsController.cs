using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zadatak.Models;

namespace Zadatak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OsobaUredjajsController : ControllerBase
    {
        private readonly ZadatakContext _context;

        public OsobaUredjajsController(ZadatakContext context)
        {
            _context = context;
        }

        
        [HttpPut]
        public IActionResult UpdateUredjajOsoba(OsobaUredjaj input, long id)
        {
            if (input == null || input.Id != id) return BadRequest();
            var updated = _context.OsobaUredjajs.Update(input);
            if (updated == null) return NotFound();
            return Ok(updated);
        }
        

        [HttpGet]
        public IActionResult GetAll()
        {
            var koriscenje = _context.OsobaUredjajs;
            var koriscenjeQuary = koriscenje.Select(y => new
            {
                Ime = y.Osoba.Ime + " " + y.Osoba.Prezime,
                Uredjaj = y.Uredjaj.Ime,
                VrijemeOd = y.VrijemeOd,
                VrijemeDo = y.VrijemeDo
            });

            if (koriscenjeQuary.Any())
            {
                return Ok(koriscenjeQuary.ToList());
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult OsobaUredjajResult(string ime, string prezime, string uredjaj)
        {

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var istorija = new OsobaUredjaj
                    {
                        VrijemeOd = DateTime.Now,
                    };
                   
                    var osobe = _context.Osobas;
                    var osobeQuery =
                        osobe.Where(x => x.Ime.Equals(ime) && x.Prezime.Equals(prezime)).Select(osoba => osoba.Id).FirstOrDefault();
                   
                    var uredjaji = _context.Uredjajs;
                    var uredjajiQuery =
                        uredjaji.Where(x => x.Ime.Equals(uredjaj)).Select(d => d.Id).FirstOrDefault();

                    
                    var korUredjaji = _context.OsobaUredjajs;
                    var korUredjajiQuery =
                        korUredjaji.Where(x => x.UredjajId == uredjajiQuery && x.VrijemeDo == null).Select(y => y.Id);

                    var izmjena = _context.OsobaUredjajs.Find(korUredjajiQuery.FirstOrDefault());

                    if (korUredjajiQuery.Count() != 0)
                    {
                        izmjena.VrijemeDo = DateTime.Now;
                        _context.SaveChanges();
                    }
                    if (osobeQuery != null && uredjajiQuery != null)
                    {
                        istorija.OsobaId = osobeQuery;
                        istorija.UredjajId = uredjajiQuery;
                    }
                    else
                    {
                        return BadRequest();
                    }
                    _context.OsobaUredjajs.Add(istorija);
                    _context.SaveChanges();
                    transaction.Commit();
                    return Ok(korUredjajiQuery.ToString());
                }
                catch (Exception e)
                {
                    return BadRequest();
                }
            }

        }
    }
}
