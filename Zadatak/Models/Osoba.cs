using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Zadatak.Models
{
    public class Osoba
    {
        [Key]
        public long? Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }

        public long KancelarijaId { get; set; }

        [ForeignKey("KancelarijaId")]
        public Kancelarija Kancelarija { get; set; }

        public ICollection<Uredjaj> Uredjajs { get; set; }
    }
}
