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
    public class BancoController : Controller
    {
        private MvcBancosContext _context;
        public BancoController(MvcBancosContext context)
        {

            _context = context;
            if (!_context.Banco.Any())
            {
                _context.Banco.Add(new Banco
                { Nombre = "Banco Prueba", Direccion = "Direccion Prueba Banco 1"});
                _context.SaveChanges();
            }
        }

        //INI Index 001
        //[Authorize(Roles = "Administrador,Operador1")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Banco.ToListAsync());
        }
        //FIN Index 001

       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banco = await _context.Banco
                .FirstOrDefaultAsync(m => m.Id == id);
            if (banco == null)
            {
                return NotFound();
            }

            return View(banco);
        }


        
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Direccion,FechaRegistro")]  Banco banco)
        {
            if (banco == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                _context.Add(banco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(banco);
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banco = await _context.Banco.FindAsync(id);
            if (banco == null)
            {
                return NotFound();
            }
            return View(banco);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Direccion,FechaRegistro")] Banco banco)
        {
            if (id != banco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(banco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExisteBanco(banco.Id))
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
            return View(banco);
        }

        private bool ExisteBanco(int id)
        {
            return _context.Banco.Any(e => e.Id == id);
        }


       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var banco = await _context.Banco
                .FirstOrDefaultAsync(m => m.Id == id);
            if (banco == null)
            {
                return NotFound();
            }

            return View(banco);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var banco = await _context.Banco.FindAsync(id);
            _context.Banco.Remove(banco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}