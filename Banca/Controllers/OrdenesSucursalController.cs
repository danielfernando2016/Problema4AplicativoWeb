using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banca.Models;
using Banca.Data;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Banca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenesSucursalController : ControllerBase
    {
        private readonly MvcBancosContext _context;

        public OrdenesSucursalController(MvcBancosContext context)
        {
            _context = context;         
        }
        // GET: api/OrdenesSucursal
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orden>>> GetOrdenesItems()
        {
            return await _context.Orden.ToListAsync();
        }

        [HttpGet("{id}")]
        public List<Orden>  GetOrdenesItems(string moneda, int IdSucursal)
        {

            var ordenes = from m in _context.Orden
                         select m;

            if (!String.IsNullOrEmpty(moneda))
            {
                ordenes = ordenes.Where(s => s.IdSucursal==IdSucursal && s.Moneda==moneda);
            }
            
            return ordenes.ToList();
            
        }
        
    }
}
