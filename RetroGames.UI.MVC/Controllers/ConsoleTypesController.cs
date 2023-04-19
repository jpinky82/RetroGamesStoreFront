using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RetroGames.DATA.EF.Models;

namespace RetroGames.UI.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ConsoleTypesController : Controller
    {
        private readonly RetroGamesContext _context;

        public ConsoleTypesController(RetroGamesContext context)
        {
            _context = context;
        }

        // GET: ConsoleTypes
        public async Task<IActionResult> Index()
        {
              return _context.ConsoleTypes != null ? 
                          View(await _context.ConsoleTypes.ToListAsync()) :
                          Problem("Entity set 'RetroGamesContext.ConsoleTypes'  is null.");
        }

        // GET: ConsoleTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ConsoleTypes == null)
            {
                return NotFound();
            }

            var consoleType = await _context.ConsoleTypes
                .FirstOrDefaultAsync(m => m.ConsoleTypeId == id);
            if (consoleType == null)
            {
                return NotFound();
            }

            return View(consoleType);
        }

        // GET: ConsoleTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ConsoleTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConsoleTypeId,ConsoleName")] ConsoleType consoleType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consoleType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(consoleType);
        }

        // GET: ConsoleTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ConsoleTypes == null)
            {
                return NotFound();
            }

            var consoleType = await _context.ConsoleTypes.FindAsync(id);
            if (consoleType == null)
            {
                return NotFound();
            }
            return View(consoleType);
        }

        // POST: ConsoleTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConsoleTypeId,ConsoleName")] ConsoleType consoleType)
        {
            if (id != consoleType.ConsoleTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consoleType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsoleTypeExists(consoleType.ConsoleTypeId))
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
            return View(consoleType);
        }

        // GET: ConsoleTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ConsoleTypes == null)
            {
                return NotFound();
            }

            var consoleType = await _context.ConsoleTypes
                .FirstOrDefaultAsync(m => m.ConsoleTypeId == id);
            if (consoleType == null)
            {
                return NotFound();
            }

            return View(consoleType);
        }

        // POST: ConsoleTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ConsoleTypes == null)
            {
                return Problem("Entity set 'RetroGamesContext.ConsoleTypes'  is null.");
            }
            var consoleType = await _context.ConsoleTypes.FindAsync(id);
            if (consoleType != null)
            {
                _context.ConsoleTypes.Remove(consoleType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsoleTypeExists(int id)
        {
          return (_context.ConsoleTypes?.Any(e => e.ConsoleTypeId == id)).GetValueOrDefault();
        }
    }
}
