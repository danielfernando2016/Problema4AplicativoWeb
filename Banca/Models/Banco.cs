using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Banca.Models
{
    public class Banco
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        //public virtual ICollection<Sucursal> Sucursal { get; set; } // This is new
        //public string Genre { get; set; }
        //public decimal Price { get; set; }
    }
}
