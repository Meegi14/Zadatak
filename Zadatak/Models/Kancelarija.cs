using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak.Models
{
    public class Kancelarija
    {
        [Key]
        public long Id { get; set; }
        public string Ime { get; set; }

        public ICollection<Osoba> Osobas { get; set; }
        
    }

}
