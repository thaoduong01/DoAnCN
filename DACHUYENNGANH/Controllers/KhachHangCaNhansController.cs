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
    public class KhachHangCaNhansController : Controller
    {
        private readonly DAChuyenNganhContext _context;

        public KhachHangCaNhansController(DAChuyenNganhContext context)
        {
            _context = context;
        }

        // GET: KhachHangCaNhans
        public async Task<IActionResult> Index()
        {
            var dAChuyenNganhContext = _context.KhachHangCaNhans.Include(k => k.IdKhachHangNavigation);
            return View(await dAChuyenNganhContext.ToListAsync());
        }

        [HttpGet]
        public IActionResult Index(string search, int id)
        {
            ViewData["Getchucvudetails"] = search;
            ViewBag.KhachHang = _context.KhachHangs;
            ;

            var ketDN = from s in _context.KhachHangCaNhans
                        join i in _context.KhachHangs
                        on s.IdKhachHang equals i.IdKhachHang
                        select new { s.IdKhachHangCaNhan, s.IdKhachHang, s.TenKh, s.NgaySinh, s.GioiTinh, s.DiaChi, s.CmndCccd, s.Sdt };

            if (!string.IsNullOrEmpty(search))
            {
                ketDN = ketDN.Where(x => x.IdKhachHangCaNhan.Contains(search) || x.TenKh.Contains(search));

            }
            if (id != 0)
            {
                ketDN = ketDN.Where(x => x.IdKhachHang == id);
            }
            List<KhachHangCaNhan> khachHangCaNhans = new List<KhachHangCaNhan>();
            foreach (var k in ketDN)
            {
                KhachHangCaNhan khcn = new KhachHangCaNhan();
                khcn.IdKhachHangCaNhan = k.IdKhachHangCaNhan;
                khcn.IdKhachHang = k.IdKhachHang;
                khcn.TenKh = k.TenKh;
                khcn.NgaySinh = k.NgaySinh;
                khcn.CmndCccd = k.CmndCccd;
                khcn.GioiTinh = k.GioiTinh;
                khcn.DiaChi = k.DiaChi;
                khcn.Sdt = k.Sdt;

                khachHangCaNhans.Add(khcn);
            }
            return View(khachHangCaNhans);
        }

        // GET: KhachHangCaNhans/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.KhachHangCaNhans == null)
            {
                return NotFound();
            }

            var khachHangCaNhan = await _context.KhachHangCaNhans
                .Include(k => k.IdKhachHangNavigation)
                .FirstOrDefaultAsync(m => m.IdKhachHangCaNhan == id);
            if (khachHangCaNhan == null)
            {
                return NotFound();
            }

            return View(khachHangCaNhan);
        }

        // GET: KhachHangCaNhans/Create
        public IActionResult Create()
        {
            ViewData["IdKhachHang"] = new SelectList(_context.KhachHangs, "IdKhachHang", "IdKhachHang");
            return View();
        }

        // POST: KhachHangCaNhans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NgaySinh,IdKhachHang,DiaChi,GioiTinh,Sdt,CmndCccd,TenKh,IdKhachHangCaNhan")] KhachHangCaNhan khachHangCaNhan)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(khachHangCaNhan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdKhachHang"] = new SelectList(_context.KhachHangs, "IdKhachHang", "IdKhachHang", khachHangCaNhan.IdKhachHang);
            return View(khachHangCaNhan);
        }

        // GET: KhachHangCaNhans/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.KhachHangCaNhans == null)
            {
                return NotFound();
            }

            var khachHangCaNhan = await _context.KhachHangCaNhans.FindAsync(id);
            if (khachHangCaNhan == null)
            {
                return NotFound();
            }
            ViewData["IdKhachHang"] = new SelectList(_context.KhachHangs, "IdKhachHang", "IdKhachHang", khachHangCaNhan.IdKhachHang);
            return View(khachHangCaNhan);
        }

        // POST: KhachHangCaNhans/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NgaySinh,IdKhachHang,DiaChi,GioiTinh,Sdt,CmndCccd,TenKh,IdKhachHangCaNhan")] KhachHangCaNhan khachHangCaNhan)
        {
            if (id != khachHangCaNhan.IdKhachHangCaNhan)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(khachHangCaNhan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhachHangCaNhanExists(khachHangCaNhan.IdKhachHangCaNhan))
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
            ViewData["IdKhachHang"] = new SelectList(_context.KhachHangs, "IdKhachHang", "IdKhachHang", khachHangCaNhan.IdKhachHang);
            return View(khachHangCaNhan);
        }

        // GET: KhachHangCaNhans/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.KhachHangCaNhans == null)
            {
                return NotFound();
            }

            var khachHangCaNhan = await _context.KhachHangCaNhans
                .Include(k => k.IdKhachHangNavigation)
                .FirstOrDefaultAsync(m => m.IdKhachHangCaNhan == id);
            if (khachHangCaNhan == null)
            {
                return NotFound();
            }

            return View(khachHangCaNhan);
        }

        // POST: KhachHangCaNhans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.KhachHangCaNhans == null)
            {
                return Problem("Entity set 'DAChuyenNganhContext.KhachHangCaNhans'  is null.");
            }
            var khachHangCaNhan = await _context.KhachHangCaNhans.FindAsync(id);
            if (khachHangCaNhan != null)
            {
                _context.KhachHangCaNhans.Remove(khachHangCaNhan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KhachHangCaNhanExists(string id)
        {
          return _context.KhachHangCaNhans.Any(e => e.IdKhachHangCaNhan == id);
        }
    }
}
