using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DACHUYENNGANH.Models;
using DACHUYENNGANH.TienIch;

namespace DACHUYENNGANH.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HoSoVayDoanhNghiepsController : Controller
    {
        private readonly DAChuyenNganhContext _context;

        public HoSoVayDoanhNghiepsController(DAChuyenNganhContext context)
        {
            _context = context;
        }

        // GET: Admin/HoSoVayDoanhNghieps
        public async Task<IActionResult> Index()
        {
            var dAChuyenNganhContext = _context.HoSoVayDoanhNghieps.Include(h => h.IdDoanhNghiepNavigation).Include(h => h.IdNhanVienNavigation);
            return View(await dAChuyenNganhContext.ToListAsync());
        }

        [HttpGet]
        public IActionResult Index(string search, string iddn, string idnv, DateTime fromdate, DateTime todate)
        {
            ViewData["Getchucvudetails"] = search;
            ViewBag.DoanhNghiep = _context.DoanhNghieps;
            ViewBag.NhanVien = _context.NhanViens;
            /*var hsvay = from x in _context.HoSoVayDoanhNghieps select x*/
            ;

            var ket = from s in _context.HoSoVayDoanhNghieps
                      join l in _context.NhanViens
                      on s.IdNhanVien equals l.IdNhanVien
                      join i in _context.DoanhNghieps
                      on s.IdDoanhNghiep equals i.IdDoanhNghiep
                      select new { s.IdDoanhNghiep, s.IdHsvay, s.IdNhanVien, s.LaiSuat, s.NgayBdvay, s.NgayKt, s.SoTienVay };

            DateTime dateTimeNull = new DateTime(1, 1, 0001, 0, 0, 0);

            if (fromdate != dateTimeNull && todate != dateTimeNull)
            {
                ket = ket.Where(x => x.NgayBdvay >= fromdate && x.NgayBdvay <= todate);
            }
            if (fromdate == dateTimeNull && todate != dateTimeNull)
            {
                ket = ket.Where(x => x.NgayBdvay <= todate);
            }
            if (fromdate != dateTimeNull && todate == dateTimeNull)
            {
                ket = ket.Where(x => x.NgayBdvay >= fromdate);
            }


            //if (!string.IsNullOrEmpty(search))
            //{
            //    ket = ket.Where(x => x.IdHsvay.Contains(search) || x.IdDoanhNghiep.Contains(search) || x.IdNhanVien.Contains(search)).OrderByDescending(x => x.NgayBdvay);

            //}
            if (iddn != null && idnv != null)
            {
                ket = ket.Where(x => x.IdNhanVien == idnv || x.IdDoanhNghiep == iddn).OrderByDescending(x => x.NgayBdvay);
            }
            List<HoSoVayDoanhNghiep> hoSoVayDoanhNghieps = new List<HoSoVayDoanhNghiep>();
            foreach (var k in ket)
            {
                HoSoVayDoanhNghiep hsvay = new HoSoVayDoanhNghiep();
                hsvay.IdHsvay = k.IdHsvay;
                hsvay.IdDoanhNghiep = k.IdDoanhNghiep;
                hsvay.IdNhanVien = k.IdNhanVien;
                hsvay.SoTienVay = k.SoTienVay;
                hsvay.LaiSuat = k.LaiSuat;
                hsvay.NgayBdvay = k.NgayBdvay;
                hsvay.NgayKt = k.NgayKt;

                hoSoVayDoanhNghieps.Add(hsvay);
            }
            return View(hoSoVayDoanhNghieps);
        }

        // GET: Admin/HoSoVayDoanhNghieps/Details/5
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

        // GET: Admin/HoSoVayDoanhNghieps/Create
        public IActionResult Create()
        {
            ViewData["IdDoanhNghiep"] = new SelectList(_context.DoanhNghieps, "IdDoanhNghiep", "TenDoanhNghiep");
            ViewData["IdNhanVien"] = new SelectList(_context.NhanViens, "IdNhanVien", "TenNhanVien");
            return View();
        }

        // POST: Admin/HoSoVayDoanhNghieps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdHsvay,NgayBdvay,SoTienVay,NgayKt,LaiSuat,IdNhanVien,IdDoanhNghiep")] HoSoVayDoanhNghiep hoSoVayDoanhNghiep)
        {
            if (!ModelState.IsValid)
            {
                hoSoVayDoanhNghiep.IdNhanVien = HttpContext.Session.GetString("IdNhanVien").ToString();
                hoSoVayDoanhNghiep.IdHsvay = GetIDHD.GetIDHopDong();
                hoSoVayDoanhNghiep.NgayBdvay = DateTime.Now;
                _context.Add(hoSoVayDoanhNghiep);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDoanhNghiep"] = new SelectList(_context.DoanhNghieps, "IdDoanhNghiep", "TenDoanhNghiep", hoSoVayDoanhNghiep.IdDoanhNghiep);
            ViewData["IdNhanVien"] = new SelectList(_context.NhanViens, "IdNhanVien", "TenNhanVien", hoSoVayDoanhNghiep.IdNhanVien);
            return View(hoSoVayDoanhNghiep);
        }

        // GET: Admin/HoSoVayDoanhNghieps/Edit/5
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
            ViewData["IdDoanhNghiep"] = new SelectList(_context.DoanhNghieps, "IdDoanhNghiep", "TenDoanhNghiep", hoSoVayDoanhNghiep.IdDoanhNghiep);
            ViewData["IdNhanVien"] = new SelectList(_context.NhanViens, "IdNhanVien", "TenNhanVien", hoSoVayDoanhNghiep.IdNhanVien);
            return View(hoSoVayDoanhNghiep);
        }

        // POST: Admin/HoSoVayDoanhNghieps/Edit/5
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
            ViewData["IdDoanhNghiep"] = new SelectList(_context.DoanhNghieps, "IdDoanhNghiep", "TenDoanhNghiep", hoSoVayDoanhNghiep.IdDoanhNghiep);
            ViewData["IdNhanVien"] = new SelectList(_context.NhanViens, "IdNhanVien", "TenNhanVien", hoSoVayDoanhNghiep.IdNhanVien);
            return View(hoSoVayDoanhNghiep);
        }

        // GET: Admin/HoSoVayDoanhNghieps/Delete/5
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

        // POST: Admin/HoSoVayDoanhNghieps/Delete/5
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
