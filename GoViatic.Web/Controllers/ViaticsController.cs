using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GoViatic.Web.Data;
using GoViatic.Web.Data.Entities;

namespace GoViatic.Web.Controllers
{
    public class ViaticsController : Controller
    {
        private readonly DataContext _context;

        public ViaticsController(DataContext context)
        {
            _context = context;
        }

        // GET: Viatics
        public async Task<IActionResult> Index()
        {
            return View(await _context.Viatics.ToListAsync());
        }

        // GET: Viatics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viatic = await _context.Viatics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (viatic == null)
            {
                return NotFound();
            }

            return View(viatic);
        }

        // GET: Viatics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Viatics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ViaticName,Description,InvoiceDate,ImageUrl")] Viatic viatic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(viatic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viatic);
        }

        // GET: Viatics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viatic = await _context.Viatics.FindAsync(id);
            if (viatic == null)
            {
                return NotFound();
            }
            return View(viatic);
        }

        // POST: Viatics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ViaticName,Description,InvoiceDate,ImageUrl")] Viatic viatic)
        {
            if (id != viatic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viatic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ViaticExists(viatic.Id))
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
            return View(viatic);
        }

        // GET: Viatics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viatic = await _context.Viatics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (viatic == null)
            {
                return NotFound();
            }

            return View(viatic);
        }

        // POST: Viatics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var viatic = await _context.Viatics.FindAsync(id);
            _context.Viatics.Remove(viatic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ViaticExists(int id)
        {
            return _context.Viatics.Any(e => e.Id == id);
        }
    }
}
