using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banca.Models
{
    public class Orden
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Monto { get; set; }
        public string Moneda { get; set; }
        public string Estado { get; set; }  
        
        public string NombreSucursal { get; set; }

        public List<Sucursal> Sucursales;

        [DataType(DataType.Date)]
        public DateTime FechaPago { get; set; } = DateTime.Now;
        public virtual int IdSucursal { get; set; }

    }

}