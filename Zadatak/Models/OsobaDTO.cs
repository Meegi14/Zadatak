using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak.Models
{
    public class OsobaDto
    {
        [Key]
        public long? Id { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }

        public long KancelarijaId { get; set; }
    }
}
