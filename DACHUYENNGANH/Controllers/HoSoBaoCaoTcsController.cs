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
    public class HoSoBaoCaoTcsController : Controller
    {
        private readonly DAChuyenNganhContext _context;

        public HoSoBaoCaoTcsController(DAChuyenNganhContext context)
        {
            _context = context;
        }

        // GET: HoSoBaoCaoTcs
        public async Task<IActionResult> Index()
        {
            var dAChuyenNganhContext = _context.HoSoBaoCaoTcs.Include(h => h.IdHsvayNavigation);
            return View(await dAChuyenNganhContext.ToListAsync());
        }

        // GET: HoSoBaoCaoTcs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HoSoBaoCaoTcs == null)
            {
                return NotFound();
            }

            var hoSoBaoCaoTc = await _context.HoSoBaoCaoTcs
                .Include(h => h.IdHsvayNavigation)
                .FirstOrDefaultAsync(m => m.IdBctc == id);
            if (hoSoBaoCaoTc == null)
            {
                return NotFound();
            }

            return View(hoSoBaoCaoTc);
        }

        // GET: HoSoBaoCaoTcs/Create
        public IActionResult Create()
        {
            ViewData["IdHsvay"] = new SelectList(_context.HoSoVayDoanhNghieps, "IdHsvay", "IdHsvay");
            return View();
        }

        // POST: HoSoBaoCaoTcs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdBctc,ToVat,HopDongSdld,HopDongMuaBan,SaoKeTknh,NgayNhanHs,BctaiChinh,IdHsvay")] HoSoBaoCaoTc hoSoBaoCaoTc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hoSoBaoCaoTc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdHsvay"] = new SelectList(_context.HoSoVayDoanhNghieps, "IdHsvay", "IdHsvay", hoSoBaoCaoTc.IdHsvay);
            return View(hoSoBaoCaoTc);
        }

        // GET: HoSoBaoCaoTcs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HoSoBaoCaoTcs == null)
            {
                return NotFound();
            }

            var hoSoBaoCaoTc = await _context.HoSoBaoCaoTcs.FindAsync(id);
            if (hoSoBaoCaoTc == null)
            {
                return NotFound();
            }
            ViewData["IdHsvay"] = new SelectList(_context.HoSoVayDoanhNghieps, "IdHsvay", "IdHsvay", hoSoBaoCaoTc.IdHsvay);
            return View(hoSoBaoCaoTc);
        }

        // POST: HoSoBaoCaoTcs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdBctc,ToVat,HopDongSdld,HopDongMuaBan,SaoKeTknh,NgayNhanHs,BctaiChinh,IdHsvay")] HoSoBaoCaoTc hoSoBaoCaoTc)
        {
            if (id != hoSoBaoCaoTc.IdBctc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoSoBaoCaoTc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoSoBaoCaoTcExists(hoSoBaoCaoTc.IdBctc))
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
            ViewData["IdHsvay"] = new SelectList(_context.HoSoVayDoanhNghieps, "IdHsvay", "IdHsvay", hoSoBaoCaoTc.IdHsvay);
            return View(hoSoBaoCaoTc);
        }

        // GET: HoSoBaoCaoTcs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HoSoBaoCaoTcs == null)
            {
                return NotFound();
            }

            var hoSoBaoCaoTc = await _context.HoSoBaoCaoTcs
                .Include(h => h.IdHsvayNavigation)
                .FirstOrDefaultAsync(m => m.IdBctc == id);
            if (hoSoBaoCaoTc == null)
            {
                return NotFound();
            }

            return View(hoSoBaoCaoTc);
        }

        // POST: HoSoBaoCaoTcs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HoSoBaoCaoTcs == null)
            {
                return Problem("Entity set 'DAChuyenNganhContext.HoSoBaoCaoTcs'  is null.");
            }
            var hoSoBaoCaoTc = await _context.HoSoBaoCaoTcs.FindAsync(id);
            if (hoSoBaoCaoTc != null)
            {
                _context.HoSoBaoCaoTcs.Remove(hoSoBaoCaoTc);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoSoBaoCaoTcExists(int id)
        {
          return _context.HoSoBaoCaoTcs.Any(e => e.IdBctc == id);
        }
    }
}
