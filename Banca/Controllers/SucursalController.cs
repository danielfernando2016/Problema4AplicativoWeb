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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Banca.Controllers
{
    public class SucursalController : Controller
    {
        private MvcBancosContext _context;
        public SucursalController(MvcBancosContext context)
        {
            _context = context;            
        }

        //INI Index 001
        //[Authorize(Roles = "Administrador,Operador1")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sucursal.ToListAsync());
        }

       
        public IActionResult Create()
        {
            var dataBancos = from m in _context.Banco
                         select m;          

            var listaBancosSucursales = new Sucursal
            {                
                Bancos = dataBancos.ToList()
            };            

            return View(listaBancosSucursales);
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSucursal,Nombre,Direccion,FechaRegistro,Id")]  Sucursal sucursal)
        {
            if (sucursal == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                _context.Add(sucursal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sucursal);
        }        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sucursal = await _context.Sucursal
                .FirstOrDefaultAsync(m => m.IdSucursal == id);
            if (sucursal == null)
            {
                return NotFound();
            }

            var dataBancos = from m in _context.Banco
                             select m;

            string nombreBanco = (from banco in dataBancos
                                   where banco.Id == sucursal.Id
                          select banco.Nombre).FirstOrDefault();

            sucursal.NombreBanco = nombreBanco;
            return View(sucursal);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sucursal = await _context.Sucursal.FindAsync(id);
            if (sucursal == null)
            {
                return NotFound();
            }

            var dataBancos = from m in _context.Banco
                             select m;

            var sucursalFinal = new Sucursal
            {                
                Bancos = dataBancos.ToList()
            };
            sucursalFinal.IdSucursal = sucursal.IdSucursal;
            sucursalFinal.Nombre = sucursal.Nombre;
            sucursalFinal.Direccion = sucursal.Direccion;
            sucursalFinal.Id = sucursal.Id;

            return View(sucursalFinal);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdSucursal,Nombre,Direccion,FechaRegistro,Id")] Sucursal sucursal)
        {
            if (id != sucursal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sucursal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExisteSucursal(sucursal.Id))
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
            return View(sucursal);
        }

        private bool ExisteSucursal(int id)
        {
            return _context.Sucursal.Any(e => e.Id == id);
        }


       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sucursal = await _context.Sucursal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sucursal == null)
            {
                return NotFound();
            }

            var dataBancos = from m in _context.Banco
                             select m;

            string nombreBanco = (from banco in dataBancos
                                  where banco.Id == sucursal.Id
                                  select banco.Nombre).FirstOrDefault();

            sucursal.NombreBanco = nombreBanco;

            return View(sucursal);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sucursal = await _context.Sucursal.FindAsync(id);
            _context.Sucursal.Remove(sucursal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
