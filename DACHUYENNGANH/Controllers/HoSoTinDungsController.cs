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
    public class HoSoTinDungsController : Controller
    {
        private readonly DAChuyenNganhContext _context;

        public HoSoTinDungsController(DAChuyenNganhContext context)
        {
            _context = context;
        }

        // GET: Admin/HoSoTinDungs
        public async Task<IActionResult> Index()
        {
            var dAChuyenNganhContext = _context.HoSoTinDungs.Include(h => h.IdKhachHangCaNhanNavigation).Include(h => h.IdNhanVienNavigation);
            return View(await dAChuyenNganhContext.ToListAsync());
        }

        [HttpGet]
        public IActionResult Index(string search, string iddn, string idnv)
        {
            ViewData["Getchucvudetails"] = search;
            ViewBag.KhachHang = _context.KhachHangCaNhans;
            ViewBag.NhanVien = _context.NhanViens;
            /*var hsvay = from x in _context.HoSoVayDoanhNghieps select x*/
            ;

            var ketDN = from s in _context.HoSoTinDungs
                        join i in _context.KhachHangCaNhans
                        on s.IdKhachHangCaNhan equals i.IdKhachHangCaNhan
                        select new { s.IdKhachHangCaNhan, s.NgayNhanHs, s.IdHstinDung, s.ChuKy, s.PhiMoThe, s.IdNhanVien };

            var ketNV = from s in _context.HoSoTinDungs
                        join i in _context.NhanViens
                        on s.IdNhanVien equals i.IdNhanVien
                        select new { s.IdKhachHangCaNhan, s.NgayNhanHs, s.IdHstinDung, s.ChuKy, s.PhiMoThe, s.IdNhanVien };

            if (!string.IsNullOrEmpty(search))
            {
                ketDN = ketDN.Where(x => x.ChuKy.Contains(search)).OrderByDescending(x => x.NgayNhanHs);

            }
            if (iddn != null)
            {
                ketDN = ketDN.Where(x => x.IdKhachHangCaNhan == iddn);
            }
            if (idnv != null)
            {
                ketNV = ketNV.Where(x => x.IdNhanVien == idnv).OrderByDescending(x => x.NgayNhanHs);
            }
            List<HoSoTinDung> hoSoTinDungs = new List<HoSoTinDung>();
            foreach (var k in ketDN)
            {
                HoSoTinDung hstd = new HoSoTinDung();
                hstd.IdHstinDung = k.IdHstinDung;
                hstd.IdKhachHangCaNhan = k.IdKhachHangCaNhan;
                hstd.IdNhanVien = k.IdNhanVien;
                hstd.NgayNhanHs = k.NgayNhanHs;
                hstd.ChuKy = k.ChuKy;
                hstd.PhiMoThe = k.PhiMoThe;

                hoSoTinDungs.Add(hstd);
            }
            foreach (var k in ketNV)
            {
                HoSoTinDung nv = new HoSoTinDung();
                nv.IdHstinDung = k.IdHstinDung;
                nv.IdKhachHangCaNhan = k.IdKhachHangCaNhan;
                nv.IdNhanVien = k.IdNhanVien;
                nv.NgayNhanHs = k.NgayNhanHs;
                nv.ChuKy = k.ChuKy;
                nv.PhiMoThe = k.PhiMoThe;

                hoSoTinDungs.Add(nv);
            }
            return View(hoSoTinDungs);
        }

        // GET: Admin/HoSoTinDungs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HoSoTinDungs == null)
            {
                return NotFound();
            }

            var hoSoTinDung = await _context.HoSoTinDungs
                .Include(h => h.IdKhachHangCaNhanNavigation)
                .Include(h => h.IdNhanVienNavigation)
                .FirstOrDefaultAsync(m => m.IdHstinDung == id);
            if (hoSoTinDung == null)
            {
                return NotFound();
            }

            return View(hoSoTinDung);
        }

        // GET: Admin/HoSoTinDungs/Create
        public IActionResult Create()
        {
            ViewData["IdKhachHangCaNhan"] = new SelectList(_context.KhachHangCaNhans, "IdKhachHangCaNhan", "TenKh");
            ViewData["IdNhanVien"] = new SelectList(_context.NhanViens, "IdNhanVien", "TenNhanVien");
            return View();
        }

        // POST: Admin/HoSoTinDungs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdHstinDung,NgayNhanHs,PhiMoThe,ChuKy,IdNhanVien,IdKhachHangCaNhan")] HoSoTinDung hoSoTinDung)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(hoSoTinDung);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdKhachHangCaNhan"] = new SelectList(_context.KhachHangCaNhans, "IdKhachHangCaNhan", "TenKh", hoSoTinDung.IdKhachHangCaNhan);
            ViewData["IdNhanVien"] = new SelectList(_context.NhanViens, "IdNhanVien", "TenNhanVien", hoSoTinDung.IdNhanVien);
            return View(hoSoTinDung);
        }

        // GET: Admin/HoSoTinDungs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HoSoTinDungs == null)
            {
                return NotFound();
            }

            var hoSoTinDung = await _context.HoSoTinDungs.FindAsync(id);
            if (hoSoTinDung == null)
            {
                return NotFound();
            }
            ViewData["IdKhachHangCaNhan"] = new SelectList(_context.KhachHangCaNhans, "IdKhachHangCaNhan", "TenKh", hoSoTinDung.IdKhachHangCaNhan);
            ViewData["IdNhanVien"] = new SelectList(_context.NhanViens, "IdNhanVien", "TenNhanVien", hoSoTinDung.IdNhanVien);
            return View(hoSoTinDung);
        }

        // POST: Admin/HoSoTinDungs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdHstinDung,NgayNhanHs,PhiMoThe,ChuKy,IdNhanVien,IdKhachHangCaNhan")] HoSoTinDung hoSoTinDung)
        {
            if (id != hoSoTinDung.IdHstinDung)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoSoTinDung);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoSoTinDungExists(hoSoTinDung.IdHstinDung))
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
            ViewData["IdKhachHangCaNhan"] = new SelectList(_context.KhachHangCaNhans, "IdKhachHangCaNhan", "TenKh", hoSoTinDung.IdKhachHangCaNhan);
            ViewData["IdNhanVien"] = new SelectList(_context.NhanViens, "IdNhanVien", "TenNhanVien", hoSoTinDung.IdNhanVien);
            return View(hoSoTinDung);
        }

        // GET: Admin/HoSoTinDungs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HoSoTinDungs == null)
            {
                return NotFound();
            }

            var hoSoTinDung = await _context.HoSoTinDungs
                .Include(h => h.IdKhachHangCaNhanNavigation)
                .Include(h => h.IdNhanVienNavigation)
                .FirstOrDefaultAsync(m => m.IdHstinDung == id);
            if (hoSoTinDung == null)
            {
                return NotFound();
            }

            return View(hoSoTinDung);
        }

        // POST: Admin/HoSoTinDungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HoSoTinDungs == null)
            {
                return Problem("Entity set 'DAChuyenNganhContext.HoSoTinDungs'  is null.");
            }
            var hoSoTinDung = await _context.HoSoTinDungs.FindAsync(id);
            if (hoSoTinDung != null)
            {
                _context.HoSoTinDungs.Remove(hoSoTinDung);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoSoTinDungExists(int id)
        {
            return _context.HoSoTinDungs.Any(e => e.IdHstinDung == id);
        }
    }
}
