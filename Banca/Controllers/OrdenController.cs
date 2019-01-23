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

namespace Banca.Controllers
{
    public class OrdenController : Controller
    {
        private MvcBancosContext _context;
        public OrdenController(MvcBancosContext context)
        {
            _context = context;      
        }

        //[Authorize(Roles = "Administrador,Operador2")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Orden.ToListAsync());
        }

        public IActionResult Create()
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
        public async Task<IActionResult> Create([Bind("Id,Monto,Moneda,Estado,FechaPago,IdSucursal")]  Orden orden)
        {
            if (orden == null)
                return BadRequest();

            var dataSucursales = from m in _context.Sucursal
                                 select m;

            string nombreSucursal = (from sucursal in dataSucursales
                                     where sucursal.Id == orden.IdSucursal
                                     select sucursal.Nombre).FirstOrDefault();

            orden.NombreSucursal = nombreSucursal;
            if (ModelState.IsValid)
            {
                _context.Add(orden);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orden);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orden = await _context.Orden
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orden == null)
            {
                return NotFound();
            }

            var dataSucursales = from m in _context.Sucursal
                             select m;

            string nombreSucursal = (from sucursal in dataSucursales
                                  where sucursal.Id == orden.IdSucursal
                                  select sucursal.Nombre).FirstOrDefault();

            orden.NombreSucursal = nombreSucursal;
            return View(orden);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orden = await _context.Orden.FindAsync(id);
            if (orden == null)
            {
                return NotFound();
            }

            var dataSucursales = from m in _context.Sucursal
                                 select m;

            var ordenFinal = new Orden
            {
                Sucursales = dataSucursales.ToList()
            };
            ordenFinal.Id = orden.Id;
            ordenFinal.Monto = orden.Monto;
            ordenFinal.Moneda = orden.Moneda;
            ordenFinal.Estado = orden.Estado;
            ordenFinal.IdSucursal = orden.IdSucursal;
            ordenFinal.FechaPago = orden.FechaPago;


            string nombreSucursal = (from sucursal in dataSucursales
                                     where sucursal.Id == orden.IdSucursal
                                     select sucursal.Nombre).FirstOrDefault();

            ordenFinal.NombreSucursal = nombreSucursal;

            return View(ordenFinal);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Monto,Moneda,Estado,FechaPago,IdSucursal")] Orden orden)
        {
            if (id != orden.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var dataSucursales = from m in _context.Sucursal
                                     select m;
                try
                {
                    string nombreSucursal = (from sucursal in dataSucursales
                                             where sucursal.IdSucursal == orden.IdSucursal
                                             select sucursal.Nombre).FirstOrDefault();
                    orden.NombreSucursal = nombreSucursal;

                    _context.Update(orden);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExisteOrden(orden.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(orden);
        }

        private bool ExisteOrden(int id)
        {
            return _context.Orden.Any(e => e.Id == id);
        }
        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orden = await _context.Orden
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orden == null)
            {
                return NotFound();
            }

            var dataSucursales = from m in _context.Sucursal
                             select m;

            string nombreSucursal = (from sucursal in dataSucursales
                                     where sucursal.Id == orden.Id
                                  select sucursal.Nombre).FirstOrDefault();

            orden.NombreSucursal = nombreSucursal;

            return View(orden);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orden = await _context.Orden.FindAsync(id);
            _context.Orden.Remove(orden);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}