using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DACHUYENNGANH.Models;
using DACHUYENNGANH.TienIch;

namespace DACHUYENNGANH.Controllers
{
    public class HoSoVayDoanhNghiepsController : Controller
    {
        private readonly DAChuyenNganhContext _context;

        public HoSoVayDoanhNghiepsController(DAChuyenNganhContext context)
        {
            _context = context;
        }

        // GET: HoSoVayDoanhNghieps
        public async Task<IActionResult> Index()
        {
            var dAChuyenNganhContext = _context.HoSoVayDoanhNghieps.Include(h => h.IdDoanhNghiepNavigation).Include(h => h.IdNhanVienNavigation);
            return View(await dAChuyenNganhContext.ToListAsync());
        }

        // GET: HoSoVayDoanhNghieps/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.HoSoVayDoanhNghieps == null)
            {
                return NotFound();
            }

            var hoSoVayDoanhNghiep = await _context.HoSoVayDoanhNghieps
                .Include(h => h.IdDoanhNghiepNavigation)
                .Include(h => h.IdNhanVienNavigation)
                .FirstOrDefaultAsync(m => m.IdHsvay == id);
            if (hoSoVayDoanhNghiep == null)
            {
                return NotFound();
            }

            return View(hoSoVayDoanhNghiep);
        }

        // GET: HoSoVayDoanhNghieps/Create
        public IActionResult Create()
        {

            ViewData["IdDoanhNghiep"] = new SelectList(_context.DoanhNghieps, "IdDoanhNghiep", "IdDoanhNghiep");
            ViewData["IdNhanVien"] = new SelectList(_context.NhanViens, "IdNhanVien", "IdNhanVien");
            return View();
        }

        // POST: HoSoVayDoanhNghieps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdHsvay,NgayBdvay,SoTienVay,NgayKt,LaiSuat,IdNhanVien,IdDoanhNghiep")] HoSoVayDoanhNghiep hoSoVayDoanhNghiep)
        {
            if (!ModelState.IsValid)
            {
                hoSoVayDoanhNghiep.IdHsvay = GetIDHD.GetIDHopDong();
                _context.Add(hoSoVayDoanhNghiep);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDoanhNghiep"] = new SelectList(_context.DoanhNghieps, "IdDoanhNghiep", "IdDoanhNghiep", hoSoVayDoanhNghiep.IdDoanhNghiep);
            ViewData["IdNhanVien"] = new SelectList(_context.NhanViens, "IdNhanVien", "IdNhanVien", hoSoVayDoanhNghiep.IdNhanVien);
            return View(hoSoVayDoanhNghiep);
        }

        // GET: HoSoVayDoanhNghieps/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.HoSoVayDoanhNghieps == null)
            {
                return NotFound();
            }

            var hoSoVayDoanhNghiep = await _context.HoSoVayDoanhNghieps.FindAsync(id);
            if (hoSoVayDoanhNghiep == null)
            {
                return NotFound();
            }
            ViewData["IdDoanhNghiep"] = new SelectList(_context.DoanhNghieps, "IdDoanhNghiep", "IdDoanhNghiep", hoSoVayDoanhNghiep.IdDoanhNghiep);
            ViewData["IdNhanVien"] = new SelectList(_context.NhanViens, "IdNhanVien", "IdNhanVien", hoSoVayDoanhNghiep.IdNhanVien);
            return View(hoSoVayDoanhNghiep);
        }

        // POST: HoSoVayDoanhNghieps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("IdHsvay,NgayBdvay,SoTienVay,NgayKt,LaiSuat,IdNhanVien,IdDoanhNghiep")] HoSoVayDoanhNghiep hoSoVayDoanhNghiep)
        {
            if (id != hoSoVayDoanhNghiep.IdHsvay)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoSoVayDoanhNghiep);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoSoVayDoanhNghiepExists(hoSoVayDoanhNghiep.IdHsvay))
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
            ViewData["IdDoanhNghiep"] = new SelectList(_context.DoanhNghieps, "IdDoanhNghiep", "IdDoanhNghiep", hoSoVayDoanhNghiep.IdDoanhNghiep);
            ViewData["IdNhanVien"] = new SelectList(_context.NhanViens, "IdNhanVien", "IdNhanVien", hoSoVayDoanhNghiep.IdNhanVien);
            return View(hoSoVayDoanhNghiep);
        }

        // GET: HoSoVayDoanhNghieps/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.HoSoVayDoanhNghieps == null)
            {
                return NotFound();
            }

            var hoSoVayDoanhNghiep = await _context.HoSoVayDoanhNghieps
                .Include(h => h.IdDoanhNghiepNavigation)
                .Include(h => h.IdNhanVienNavigation)
                .FirstOrDefaultAsync(m => m.IdHsvay == id);
            if (hoSoVayDoanhNghiep == null)
            {
                return NotFound();
            }

            return View(hoSoVayDoanhNghiep);
        }

        // POST: HoSoVayDoanhNghieps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.HoSoVayDoanhNghieps == null)
            {
                return Problem("Entity set 'DAChuyenNganhContext.HoSoVayDoanhNghieps'  is null.");
            }
            var hoSoVayDoanhNghiep = await _context.HoSoVayDoanhNghieps.FindAsync(id);
            if (hoSoVayDoanhNghiep != null)
            {
                _context.HoSoVayDoanhNghieps.Remove(hoSoVayDoanhNghiep);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoSoVayDoanhNghiepExists(string id)
        {
          return _context.HoSoVayDoanhNghieps.Any(e => e.IdHsvay == id);
        }
    }
}
