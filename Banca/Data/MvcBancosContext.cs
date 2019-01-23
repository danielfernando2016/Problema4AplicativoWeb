using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Banca.Data
{
    public class MvcBancosContext : DbContext
    {
        public MvcBancosContext(DbContextOptions<MvcBancosContext> options)
            : base(options)
        {
        }

        public DbSet<Banca.Models.Banco> Banco { get; set; }
        public DbSet<Banca.Models.Sucursal> Sucursal { get; set; }
        public DbSet<Banca.Models.Orden> Orden { get; set; }
    }

}
