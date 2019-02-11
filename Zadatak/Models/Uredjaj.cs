using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak.Models
{
    public class Uredjaj
    {
        public long? Id { get; set; }
        public string Ime { get; set; }

        public long? OsobaId { get; set; }

        [ForeignKey("OsobaId")]
        public Osoba Osoba { get; set; }

        //public ICollection<OsobaUredjaj> OsobaUredjajs { get; set; }

    }
}
