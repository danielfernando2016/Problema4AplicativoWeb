using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Banca.Models;
using Banca.Data;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;

namespace Banca.Controllers
{
    public class OrdenBusquedaController : Controller
    {
        static HttpClient client = new HttpClient();
        private MvcBancosContext _context;
        public OrdenBusquedaController(MvcBancosContext context)
        {
            _context = context;      
        }
        
        public IActionResult Busqueda()
        {
            var dataSucursales = from m in _context.Sucursal
                             select m;

            var listaSucursalesOrdenes = new Orden
            {
                Sucursales = dataSucursales.ToList()
            };

            return View(listaSucursalesOrdenes);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Busqueda(string moneda, int IdSucursal)
        {            
            
            OrdenesSucursalController objetoApi = new OrdenesSucursalController(_context);            
            return new JsonResult(objetoApi.GetOrdenesItems(moneda, IdSucursal));
        }

    }
}