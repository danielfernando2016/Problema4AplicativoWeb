using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banca.Models;
using Banca.Data;
using System;

namespace Banca.Controllers
{
    public class SucursalBusquedaController : Controller
    {
        
        private MvcBancosContext _context;
        public SucursalBusquedaController(MvcBancosContext context)
        {
            _context = context;      
        }
        
        public IActionResult Busqueda()
        {
            var dataBancos = from m in _context.Banco
                             select m;

            var listaSucursalesBancos = new Sucursal
            {
                Bancos = dataBancos.ToList()
            };

            return View(listaSucursalesBancos);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Busqueda(int Id)
        {

            SucursalBancoController objetoApi = new SucursalBancoController(_context);            
            return new JsonResult(objetoApi.GetSucursalesItems( Id));
        }

    }
}