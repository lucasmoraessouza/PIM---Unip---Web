using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using protechHotel.Data;
using protechHotel.Models;

namespace protechHotel.Controllers
{

    [Authorize]
    public class ReservarController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservarController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.Reservas.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservar = await _context.Reservas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservar == null)
            {
                return NotFound();
            }
            if (reservar.User != User.Identity.Name)
            {
                return NotFound();
            }

            return View(reservar);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomeCompleto,Cpf,Quarto,DataRerseva")] Reservar reservar)
        {
            if (ModelState.IsValid)
            {
                reservar.User = User.Identity.Name;
                _context.Add(reservar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservar);
        }

        // GET: Reservar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservar = await _context.Reservas.FindAsync(id);
            if (reservar == null)
            {
                return NotFound();
            }
            if (reservar.User != User.Identity.Name)
            {
                return NotFound();
            }
            return View(reservar);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeCompleto,Cpf,Quarto,DataRerseva,User")] Reservar reservar)
        {
            if (id != reservar.Id)
            {
                return NotFound();
            }
          
            if (ModelState.IsValid)
            {
                try
                {
                    reservar.User = User.Identity.Name;
                    _context.Update(reservar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservarExists(reservar.Id))
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
            return View(reservar);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservar = await _context.Reservas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservar == null)
            {
                return NotFound();
            }

            return View(reservar);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservar = await _context.Reservas.FindAsync(id);
            _context.Reservas.Remove(reservar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservarExists(int id)
        {
            return _context.Reservas.Any(e => e.Id == id);
        }
    }
}
