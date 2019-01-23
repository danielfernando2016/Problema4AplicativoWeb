using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Banca.Models
{
    public class Sucursal
    {

        [Key]
        public int IdSucursal { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }        

        [DataType(DataType.Date)]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public virtual  int Id { get; set; }
        public string NombreBanco { get; set; }

        public List<Banco> Bancos;       

    }
}
