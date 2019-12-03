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
    public class ViaticTypesController : Controller
    {
        private readonly DataContext _context;

        public ViaticTypesController(DataContext context)
        {
            _context = context;
        }

        // GET: ViaticTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ViaticTypes.ToListAsync());
        }

        // GET: ViaticTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viaticType = await _context.ViaticTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (viaticType == null)
            {
                return NotFound();
            }

            return View(viaticType);
        }

        // GET: ViaticTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ViaticTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Concept")] ViaticType viaticType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(viaticType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viaticType);
        }

        // GET: ViaticTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viaticType = await _context.ViaticTypes.FindAsync(id);
            if (viaticType == null)
            {
                return NotFound();
            }
            return View(viaticType);
        }

        // POST: ViaticTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Concept")] ViaticType viaticType)
        {
            if (id != viaticType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viaticType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ViaticTypeExists(viaticType.Id))
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
            return View(viaticType);
        }

        // GET: ViaticTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viaticType = await _context.ViaticTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (viaticType == null)
            {
                return NotFound();
            }

            return View(viaticType);
        }

        // POST: ViaticTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var viaticType = await _context.ViaticTypes.FindAsync(id);
            _context.ViaticTypes.Remove(viaticType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ViaticTypeExists(int id)
        {
            return _context.ViaticTypes.Any(e => e.Id == id);
        }
    }
}
