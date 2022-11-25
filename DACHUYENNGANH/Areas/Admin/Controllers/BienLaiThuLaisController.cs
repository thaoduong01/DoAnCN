using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DACHUYENNGANH.Models;

namespace DACHUYENNGANH.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BienLaiThuLaisController : Controller
    {
        private readonly DAChuyenNganhContext _context;

        public BienLaiThuLaisController(DAChuyenNganhContext context)
        {
            _context = context;
        }

        // GET: Admin/BienLaiThuLais
        public async Task<IActionResult> Index()
        {
            var dAChuyenNganhContext = _context.BienLaiThuLais.Include(b => b.IdHsvayNavigation).Include(b => b.IdNhanVienNavigation);
            return View(await dAChuyenNganhContext.ToListAsync());
        }

       
        // GET: Admin/BienLaiThuLais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BienLaiThuLais == null)
            {
                return NotFound();
            }

            var bienLaiThuLai = await _context.BienLaiThuLais
                .Include(b => b.IdHsvayNavigation)
                .Include(b => b.IdNhanVienNavigation)
                .FirstOrDefaultAsync(m => m.IdBienLai == id);
            if (bienLaiThuLai == null)
            {
                return NotFound();
            }

            return View(bienLaiThuLai);
        }

        // GET: Admin/BienLaiThuLais/Create
        public IActionResult Create()
        {
            ViewData["IdHsvay"] = new SelectList(_context.HoSoVayDoanhNghieps, "IdHsvay", "IdHsvay");
            ViewData["IdNhanVien"] = new SelectList(_context.NhanViens, "IdNhanVien", "TenNhanVien");
            return View();
        }

        // POST: Admin/BienLaiThuLais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdBienLai,NgayNhanLai,IdNhanVien,IdHsvay,DuNo,LaiKyKe,GocKyKe,LaiSuat,SoTienDong,TienGoc")] BienLaiThuLai bienLaiThuLai)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(bienLaiThuLai);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdHsvay"] = new SelectList(_context.HoSoVayDoanhNghieps, "IdHsvay", "IdHsvay", bienLaiThuLai.IdHsvay);
            ViewData["IdNhanVien"] = new SelectList(_context.NhanViens, "IdNhanVien", "TenNhanVien", bienLaiThuLai.IdNhanVien);
            return View(bienLaiThuLai);
        }

        // GET: Admin/BienLaiThuLais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.BienLaiThuLais == null)
            {
                return NotFound();
            }

            var bienLaiThuLai = await _context.BienLaiThuLais.FindAsync(id);
            if (bienLaiThuLai == null)
            {
                return NotFound();
            }
            ViewData["IdHsvay"] = new SelectList(_context.HoSoVayDoanhNghieps, "IdHsvay", "IdHsvay", bienLaiThuLai.IdHsvay);
            ViewData["IdNhanVien"] = new SelectList(_context.NhanViens, "IdNhanVien", "TenNhanVien", bienLaiThuLai.IdNhanVien);
            return View(bienLaiThuLai);
        }

        // POST: Admin/BienLaiThuLais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdBienLai,NgayNhanLai,IdNhanVien,IdHsvay,DuNo,LaiKyKe,GocKyKe,LaiSuat,SoTienDong,TienGoc")] BienLaiThuLai bienLaiThuLai)
        {
            if (id != bienLaiThuLai.IdBienLai)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(bienLaiThuLai);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BienLaiThuLaiExists(bienLaiThuLai.IdBienLai))
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
            ViewData["IdHsvay"] = new SelectList(_context.HoSoVayDoanhNghieps, "IdHsvay", "IdHsvay", bienLaiThuLai.IdHsvay);
            ViewData["IdNhanVien"] = new SelectList(_context.NhanViens, "IdNhanVien", "TenNhanVien", bienLaiThuLai.IdNhanVien);
            return View(bienLaiThuLai);
        }

        // GET: Admin/BienLaiThuLais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.BienLaiThuLais == null)
            {
                return NotFound();
            }

            var bienLaiThuLai = await _context.BienLaiThuLais
                .Include(b => b.IdHsvayNavigation)
                .Include(b => b.IdNhanVienNavigation)
                .FirstOrDefaultAsync(m => m.IdBienLai == id);
            if (bienLaiThuLai == null)
            {
                return NotFound();
            }

            return View(bienLaiThuLai);
        }

        // POST: Admin/BienLaiThuLais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BienLaiThuLais == null)
            {
                return Problem("Entity set 'DAChuyenNganhContext.BienLaiThuLais'  is null.");
            }
            var bienLaiThuLai = await _context.BienLaiThuLais.FindAsync(id);
            if (bienLaiThuLai != null)
            {
                _context.BienLaiThuLais.Remove(bienLaiThuLai);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BienLaiThuLaiExists(int id)
        {
          return _context.BienLaiThuLais.Any(e => e.IdBienLai == id);
        }
    }
}
