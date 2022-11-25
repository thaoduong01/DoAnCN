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

        [HttpGet]
        public IActionResult Index(string search, string iddn, string idnv)
        {
            ViewData["Getchucvudetails"] = search;
            ViewBag.DoanhNghiep = _context.DoanhNghieps;
            ViewBag.NhanVien = _context.NhanViens;
            /*var hsvay = from x in _context.HoSoVayDoanhNghieps select x*/
            ;

            var ketDN = from s in _context.HoSoVayDoanhNghieps
                        join i in _context.DoanhNghieps
                        on s.IdDoanhNghiep equals i.IdDoanhNghiep
                        select new { s.IdDoanhNghiep, s.IdHsvay, s.IdNhanVien, s.LaiSuat, s.NgayBdvay, s.NgayKt, s.SoTienVay };

            var ketNV = from s in _context.HoSoVayDoanhNghieps
                        join i in _context.NhanViens
                        on s.IdNhanVien equals i.IdNhanVien
                        select new { s.IdDoanhNghiep, s.IdHsvay, s.IdNhanVien, s.LaiSuat, s.NgayBdvay, s.NgayKt, s.SoTienVay };

            if (!string.IsNullOrEmpty(search))
            {
                ketDN = ketDN.Where(x => x.IdHsvay.Contains(search)).OrderByDescending(x => x.NgayBdvay);

            }
            if (iddn != null)
            {
                ketDN = ketDN.Where(x => x.IdDoanhNghiep == iddn);
            }
            if (idnv != null)
            {
                ketNV = ketNV.Where(x => x.IdNhanVien == idnv).OrderByDescending(x => x.NgayBdvay);
            }
            List<HoSoVayDoanhNghiep> hoSoVayDoanhNghieps = new List<HoSoVayDoanhNghiep>();
            foreach (var k in ketDN)
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
            //foreach (var k in ketNV)
            //{
            //    HoSoVayDoanhNghiep nv = new HoSoVayDoanhNghiep();
            //    nv.IdHsvay = k.IdHsvay;
            //    nv.IdDoanhNghiep = k.IdDoanhNghiep;
            //    nv.IdNhanVien = k.IdNhanVien;
            //    nv.SoTienVay = k.SoTienVay;
            //    nv.LaiSuat = k.LaiSuat;
            //    nv.NgayBdvay = k.NgayBdvay;
            //    nv.NgayKt = k.NgayKt;

            //    hoSoVayDoanhNghieps.Add(nv);
            //}
            return View(hoSoVayDoanhNghieps);
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
            ViewData["IdNhanVien"] = new SelectList(_context.NhanViens, "IdNhanVien", "TenNhanVien");
            return View();
        }

        // POST: HoSoVayDoanhNghieps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdHsvay,NgayBdvay,SoTienVay,NgayKt,LaiSuat,IdNhanVien,IdDoanhNghiep")] HoSoVayDoanhNghiep hoSoVayDoanhNghiep)
        {
            if (hoSoVayDoanhNghiep.IdNhanVien != null && hoSoVayDoanhNghiep.IdDoanhNghiep != null)
            {
                hoSoVayDoanhNghiep.IdHsvay = GetIDHD.GetIDHopDong();
                _context.Add(hoSoVayDoanhNghiep);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDoanhNghiep"] = new SelectList(_context.DoanhNghieps, "IdDoanhNghiep", "IdDoanhNghiep", hoSoVayDoanhNghiep.IdDoanhNghiep);
            ViewData["IdNhanVien"] = new SelectList(_context.NhanViens, "IdNhanVien", "TenNhanVien", hoSoVayDoanhNghiep.IdNhanVien);
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
            ViewData["IdNhanVien"] = new SelectList(_context.NhanViens, "IdNhanVien", "TenNhanVien", hoSoVayDoanhNghiep.IdNhanVien);
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
            ViewData["IdNhanVien"] = new SelectList(_context.NhanViens, "IdNhanVien", "TenNhanVien", hoSoVayDoanhNghiep.IdNhanVien);
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
