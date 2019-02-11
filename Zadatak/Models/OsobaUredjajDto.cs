using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak.Models
{
    public class OsobaUredjajDto
    {
        public long? Id { get; set; }
        public DateTime VrijemeOd { get; set; }
        public DateTime? VrijemeDo { get; set; }
        public long? OsobaId { get; set; }
        public long? UredjajId { get; set; }
    }
}
