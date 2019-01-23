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
    public class SucursalBancoController : ControllerBase
    {
        private readonly MvcBancosContext _context;

        public SucursalBancoController(MvcBancosContext context)
        {
            _context = context;         
        }
   
        [HttpGet("{id}")]
        public List<Sucursal>  GetSucursalesItems(int Id)
        {

            var sucursal = from m in _context.Sucursal
                           select m;

            sucursal = sucursal.Where(s => s.Id == Id);
            return sucursal.ToList();
            
        }
        
    }
}
