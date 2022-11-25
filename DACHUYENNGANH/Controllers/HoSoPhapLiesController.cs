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
    public class HoSoPhapLiesController : Controller
    {
        private readonly DAChuyenNganhContext _context;

        public HoSoPhapLiesController(DAChuyenNganhContext context)
        {
            _context = context;
        }

        // GET: HoSoPhapLies
        public async Task<IActionResult> Index()
        {
            var dAChuyenNganhContext = _context.HoSoPhapLies.Include(h => h.IdHsvayNavigation);
            return View(await dAChuyenNganhContext.ToListAsync());
        }

        // GET: HoSoPhapLies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HoSoPhapLies == null)
            {
                return NotFound();
            }

            var hoSoPhapLy = await _context.HoSoPhapLies
                .Include(h => h.IdHsvayNavigation)
                .FirstOrDefaultAsync(m => m.IdPhapLy == id);
            if (hoSoPhapLy == null)
            {
                return NotFound();
            }

            return View(hoSoPhapLy);
        }

        // GET: HoSoPhapLies/Create
        public IActionResult Create()
        {
            ViewData["IdHsvay"] = new SelectList(_context.HoSoVayDoanhNghieps, "IdHsvay", "IdHsvay");
            return View();
        }

        // POST: HoSoPhapLies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPhapLy,Gdkkd,DieuLeCty,BbhopHd,TenKttruong,CmndCccdKtt,NgayNhanHs,Gcndkthue,IdHsvay")] HoSoPhapLy hoSoPhapLy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hoSoPhapLy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdHsvay"] = new SelectList(_context.HoSoVayDoanhNghieps, "IdHsvay", "IdHsvay", hoSoPhapLy.IdHsvay);
            return View(hoSoPhapLy);
        }

        // GET: HoSoPhapLies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HoSoPhapLies == null)
            {
                return NotFound();
            }

            var hoSoPhapLy = await _context.HoSoPhapLies.FindAsync(id);
            if (hoSoPhapLy == null)
            {
                return NotFound();
            }
            ViewData["IdHsvay"] = new SelectList(_context.HoSoVayDoanhNghieps, "IdHsvay", "IdHsvay", hoSoPhapLy.IdHsvay);
            return View(hoSoPhapLy);
        }

        // POST: HoSoPhapLies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPhapLy,Gdkkd,DieuLeCty,BbhopHd,TenKttruong,CmndCccdKtt,NgayNhanHs,Gcndkthue,IdHsvay")] HoSoPhapLy hoSoPhapLy)
        {
            if (id != hoSoPhapLy.IdPhapLy)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoSoPhapLy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoSoPhapLyExists(hoSoPhapLy.IdPhapLy))
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
            ViewData["IdHsvay"] = new SelectList(_context.HoSoVayDoanhNghieps, "IdHsvay", "IdHsvay", hoSoPhapLy.IdHsvay);
            return View(hoSoPhapLy);
        }

        // GET: HoSoPhapLies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HoSoPhapLies == null)
            {
                return NotFound();
            }

            var hoSoPhapLy = await _context.HoSoPhapLies
                .Include(h => h.IdHsvayNavigation)
                .FirstOrDefaultAsync(m => m.IdPhapLy == id);
            if (hoSoPhapLy == null)
            {
                return NotFound();
            }

            return View(hoSoPhapLy);
        }

        // POST: HoSoPhapLies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HoSoPhapLies == null)
            {
                return Problem("Entity set 'DAChuyenNganhContext.HoSoPhapLies'  is null.");
            }
            var hoSoPhapLy = await _context.HoSoPhapLies.FindAsync(id);
            if (hoSoPhapLy != null)
            {
                _context.HoSoPhapLies.Remove(hoSoPhapLy);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoSoPhapLyExists(int id)
        {
          return _context.HoSoPhapLies.Any(e => e.IdPhapLy == id);
        }
    }
}
