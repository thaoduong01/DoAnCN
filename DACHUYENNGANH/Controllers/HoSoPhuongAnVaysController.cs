using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DACHUYENNGANH.Models;

namespace DACHUYENNGANH.Controllers
{
    public class HoSoPhuongAnVaysController : Controller
    {
        private readonly DAChuyenNganhContext _context;

        public HoSoPhuongAnVaysController(DAChuyenNganhContext context)
        {
            _context = context;
        }

        // GET: HoSoPhuongAnVays
        public async Task<IActionResult> Index()
        {
            var dAChuyenNganhContext = _context.HoSoPhuongAnVays.Include(h => h.IdHsvayNavigation);
            return View(await dAChuyenNganhContext.ToListAsync());
        }

        // GET: HoSoPhuongAnVays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HoSoPhuongAnVays == null)
            {
                return NotFound();
            }

            var hoSoPhuongAnVay = await _context.HoSoPhuongAnVays
                .Include(h => h.IdHsvayNavigation)
                .FirstOrDefaultAsync(m => m.IdHspavay == id);
            if (hoSoPhuongAnVay == null)
            {
                return NotFound();
            }

            return View(hoSoPhuongAnVay);
        }

        // GET: HoSoPhuongAnVays/Create
        public IActionResult Create()
        {
            ViewData["IdHsvay"] = new SelectList(_context.HoSoVayDoanhNghieps, "IdHsvay", "IdHsvay");
            return View();
        }

        // POST: HoSoPhuongAnVays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdHspavay,PhuongAnKd,KeHoachTraNo,NgayNhanHs,IdHsvay")] HoSoPhuongAnVay hoSoPhuongAnVay)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hoSoPhuongAnVay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdHsvay"] = new SelectList(_context.HoSoVayDoanhNghieps, "IdHsvay", "IdHsvay", hoSoPhuongAnVay.IdHsvay);
            return View(hoSoPhuongAnVay);
        }

        // GET: HoSoPhuongAnVays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HoSoPhuongAnVays == null)
            {
                return NotFound();
            }

            var hoSoPhuongAnVay = await _context.HoSoPhuongAnVays.FindAsync(id);
            if (hoSoPhuongAnVay == null)
            {
                return NotFound();
            }
            ViewData["IdHsvay"] = new SelectList(_context.HoSoVayDoanhNghieps, "IdHsvay", "IdHsvay", hoSoPhuongAnVay.IdHsvay);
            return View(hoSoPhuongAnVay);
        }

        // POST: HoSoPhuongAnVays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdHspavay,PhuongAnKd,KeHoachTraNo,NgayNhanHs,IdHsvay")] HoSoPhuongAnVay hoSoPhuongAnVay)
        {
            if (id != hoSoPhuongAnVay.IdHspavay)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoSoPhuongAnVay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoSoPhuongAnVayExists(hoSoPhuongAnVay.IdHspavay))
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
            ViewData["IdHsvay"] = new SelectList(_context.HoSoVayDoanhNghieps, "IdHsvay", "IdHsvay", hoSoPhuongAnVay.IdHsvay);
            return View(hoSoPhuongAnVay);
        }

        // GET: HoSoPhuongAnVays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HoSoPhuongAnVays == null)
            {
                return NotFound();
            }

            var hoSoPhuongAnVay = await _context.HoSoPhuongAnVays
                .Include(h => h.IdHsvayNavigation)
                .FirstOrDefaultAsync(m => m.IdHspavay == id);
            if (hoSoPhuongAnVay == null)
            {
                return NotFound();
            }

            return View(hoSoPhuongAnVay);
        }

        // POST: HoSoPhuongAnVays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HoSoPhuongAnVays == null)
            {
                return Problem("Entity set 'DAChuyenNganhContext.HoSoPhuongAnVays'  is null.");
            }
            var hoSoPhuongAnVay = await _context.HoSoPhuongAnVays.FindAsync(id);
            if (hoSoPhuongAnVay != null)
            {
                _context.HoSoPhuongAnVays.Remove(hoSoPhuongAnVay);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoSoPhuongAnVayExists(int id)
        {
          return _context.HoSoPhuongAnVays.Any(e => e.IdHspavay == id);
        }
    }
}
