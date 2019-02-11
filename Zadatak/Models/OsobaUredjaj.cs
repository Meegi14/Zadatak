using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace Zadatak.Models
{
    public class OsobaUredjaj
    {
        
        public long? Id { get; set; }
        public DateTime VrijemeOd { get; set; }
        public DateTime? VrijemeDo { get; set; }
      
        public long? OsobaId { get; set; }

        [ForeignKey("OsobaId")]
        public Osoba Osoba { get; set; }

        public long? UredjajId { get; set; }

        [ForeignKey("UredjajId")]
        public Uredjaj Uredjaj { get; set; }

        //public ICollection<OsobaUredjaj> OsobaUredjajs { get; set; }

    }
}
