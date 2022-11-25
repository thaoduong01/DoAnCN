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
    public class HoSoThamDinhsController : Controller
    {
        private readonly DAChuyenNganhContext _context;

        public HoSoThamDinhsController(DAChuyenNganhContext context)
        {
            _context = context;
        }

        // GET: HoSoThamDinhs
        public async Task<IActionResult> Index()
        {
            var dAChuyenNganhContext = _context.HoSoThamDinhs.Include(h => h.IdCongTyNavigation).Include(h => h.IdHsdbNavigation);
            return View(await dAChuyenNganhContext.ToListAsync());
        }

        [HttpGet]
        public IActionResult Index(string search, string id)
        {
            ViewData["Getchucvudetails"] = search;
            ViewBag.CongTy = _context.CongTyThamDinhs;

            /*var hsvay = from x in _context.HoSoThamDinhs select x*/
            ;

            var ketDN = from s in _context.HoSoThamDinhs
                        join i in _context.CongTyThamDinhs
                        on s.IdCongTy equals i.IdCongTy
                        select new { s.IdCongTy, s.NgayThamDinh, s.IdHsthamDinh, s.BaoCaoThamDinh, s.SoTienThamDinh, s.TenNguoiThamDinh, s.CmndCccd, s.NgayNhanHoSo, s.IdHsdb };

            if (!string.IsNullOrEmpty(search))
            {
                ketDN = ketDN.Where(x => x.TenNguoiThamDinh.Contains(search)).OrderByDescending(x => x.NgayNhanHoSo);

            }
            if (id != null)
            {
                ketDN = ketDN.Where(x => x.IdCongTy == id);
            }
            List<HoSoThamDinh> hoSoThamDinhs = new List<HoSoThamDinh>();
            foreach (var k in ketDN)
            {
                HoSoThamDinh hstd = new HoSoThamDinh();
                hstd.BaoCaoThamDinh = k.BaoCaoThamDinh;
                hstd.IdCongTy = k.IdCongTy;
                hstd.IdHsthamDinh = k.IdHsthamDinh;
                hstd.IdHsdb = k.IdHsdb;
                hstd.SoTienThamDinh = k.SoTienThamDinh;
                hstd.TenNguoiThamDinh = k.TenNguoiThamDinh;
                hstd.CmndCccd = k.CmndCccd;
                hstd.NgayNhanHoSo = k.NgayNhanHoSo;
                hstd.NgayThamDinh = k.NgayThamDinh;

                hoSoThamDinhs.Add(hstd);
            }
            return View(hoSoThamDinhs);
        }

        // GET: HoSoThamDinhs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HoSoThamDinhs == null)
            {
                return NotFound();
            }

            var hoSoThamDinh = await _context.HoSoThamDinhs
                .Include(h => h.IdCongTyNavigation)
                .Include(h => h.IdHsdbNavigation)
                .FirstOrDefaultAsync(m => m.IdHsthamDinh == id);
            if (hoSoThamDinh == null)
            {
                return NotFound();
            }

            return View(hoSoThamDinh);
        }

        // GET: HoSoThamDinhs/Create
        public IActionResult Create()
        {
            ViewData["IdCongTy"] = new SelectList(_context.CongTyThamDinhs, "IdCongTy", "IdCongTy");
            ViewData["IdHsdb"] = new SelectList(_context.HoSoTaiSanDbs, "IdHsdb", "IdHsdb");
            return View();
        }

        // POST: HoSoThamDinhs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdHsthamDinh,SoTienThamDinh,NgayThamDinh,NgayNhanHoSo,BaoCaoThamDinh,TenNguoiThamDinh,CmndCccd,IdCongTy,IdHsdb")] HoSoThamDinh hoSoThamDinh)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(hoSoThamDinh);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCongTy"] = new SelectList(_context.CongTyThamDinhs, "IdCongTy", "IdCongTy", hoSoThamDinh.IdCongTy);
            ViewData["IdHsdb"] = new SelectList(_context.HoSoTaiSanDbs, "IdHsdb", "IdHsdb", hoSoThamDinh.IdHsdb);
            return View(hoSoThamDinh);
        }

        // GET: HoSoThamDinhs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HoSoThamDinhs == null)
            {
                return NotFound();
            }

            var hoSoThamDinh = await _context.HoSoThamDinhs.FindAsync(id);
            if (hoSoThamDinh == null)
            {
                return NotFound();
            }
            ViewData["IdCongTy"] = new SelectList(_context.CongTyThamDinhs, "IdCongTy", "IdCongTy", hoSoThamDinh.IdCongTy);
            ViewData["IdHsdb"] = new SelectList(_context.HoSoTaiSanDbs, "IdHsdb", "IdHsdb", hoSoThamDinh.IdHsdb);
            return View(hoSoThamDinh);
        }

        // POST: HoSoThamDinhs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdHsthamDinh,SoTienThamDinh,NgayThamDinh,NgayNhanHoSo,BaoCaoThamDinh,TenNguoiThamDinh,CmndCccd,IdCongTy,IdHsdb")] HoSoThamDinh hoSoThamDinh)
        {
            if (id != hoSoThamDinh.IdHsthamDinh)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoSoThamDinh);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoSoThamDinhExists(hoSoThamDinh.IdHsthamDinh))
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
            ViewData["IdCongTy"] = new SelectList(_context.CongTyThamDinhs, "IdCongTy", "IdCongTy", hoSoThamDinh.IdCongTy);
            ViewData["IdHsdb"] = new SelectList(_context.HoSoTaiSanDbs, "IdHsdb", "IdHsdb", hoSoThamDinh.IdHsdb);
            return View(hoSoThamDinh);
        }

        // GET: HoSoThamDinhs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HoSoThamDinhs == null)
            {
                return NotFound();
            }

            var hoSoThamDinh = await _context.HoSoThamDinhs
                .Include(h => h.IdCongTyNavigation)
                .Include(h => h.IdHsdbNavigation)
                .FirstOrDefaultAsync(m => m.IdHsthamDinh == id);
            if (hoSoThamDinh == null)
            {
                return NotFound();
            }

            return View(hoSoThamDinh);
        }

        // POST: HoSoThamDinhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HoSoThamDinhs == null)
            {
                return Problem("Entity set 'DAChuyenNganhContext.HoSoThamDinhs'  is null.");
            }
            var hoSoThamDinh = await _context.HoSoThamDinhs.FindAsync(id);
            if (hoSoThamDinh != null)
            {
                _context.HoSoThamDinhs.Remove(hoSoThamDinh);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoSoThamDinhExists(int id)
        {
          return _context.HoSoThamDinhs.Any(e => e.IdHsthamDinh == id);
        }
    }
}
