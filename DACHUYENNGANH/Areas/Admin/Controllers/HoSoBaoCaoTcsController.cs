using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DACHUYENNGANH.Models;
using Microsoft.Net.Http.Headers;

namespace DACHUYENNGANH.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HoSoBaoCaoTcsController : Controller
    {
        private readonly DAChuyenNganhContext _context;

        public HoSoBaoCaoTcsController(DAChuyenNganhContext context)
        {
            _context = context;
        }

        // GET: Admin/HoSoBaoCaoTcs
        public async Task<IActionResult> Index()
        {
            var dAChuyenNganhContext = _context.HoSoBaoCaoTcs.Include(h => h.IdHsvayNavigation);
            return View(await dAChuyenNganhContext.ToListAsync());
        }

        [HttpGet]
        public IActionResult Index(string search, int id)
        {
            ViewData["Getchucvudetails"] = search;

            //var baocao = from x in _context.HoSoBaoCaoTcs select x;

            ViewBag.HoSoVay = _context.HoSoVayDoanhNghieps;

            var ket = from s in _context.HoSoBaoCaoTcs
                      join i in _context.HoSoVayDoanhNghieps
                      on s.IdHsvay equals i.IdHsvay
                      select new { s.ToVat, s.IdHsvay, s.NgayNhanHs, s.BctaiChinh, s.HopDongMuaBan, s.HopDongSdld, s.IdBctc, s.SaoKeTknh };

            if (!string.IsNullOrEmpty(search))
            {
                ket = ket.Where(x => x.IdHsvay.Contains(search)).OrderByDescending(x => x.NgayNhanHs);

            }
            if (id != 0)
            {
                ket = ket.Where(x => x.IdBctc == id);
            }
            List<HoSoBaoCaoTc> listHsbaocao = new List<HoSoBaoCaoTc>();
            foreach(var k in ket)
            {
                HoSoBaoCaoTc hsbc = new HoSoBaoCaoTc();
                hsbc.IdBctc = k.IdBctc;
                hsbc.IdHsvay = k.IdHsvay;
                hsbc.ToVat = k.ToVat;
                hsbc.SaoKeTknh = k.SaoKeTknh;
                hsbc.BctaiChinh = k.BctaiChinh;
                hsbc.HopDongMuaBan = k.HopDongMuaBan;
                hsbc.HopDongSdld = k.HopDongSdld;
                hsbc.NgayNhanHs = k.NgayNhanHs;
                listHsbaocao.Add(hsbc);
            }
            return View(listHsbaocao);
        }
       


        // GET: Admin/HoSoBaoCaoTcs/Details/5
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

        // GET: Admin/HoSoBaoCaoTcs/Create
        public IActionResult Create()
        {
            ViewData["IdHsvay"] = new SelectList(_context.HoSoVayDoanhNghieps, "IdHsvay", "IdHsvay");
            return View();
        }

        // POST: Admin/HoSoBaoCaoTcs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdBctc,ToVat,HopDongSdld,HopDongMuaBan,SaoKeTknh,NgayNhanHs,BctaiChinh,IdHsvay")] HoSoBaoCaoTc hoSoBaoCaoTc)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(hoSoBaoCaoTc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdHsvay"] = new SelectList(_context.HoSoVayDoanhNghieps, "IdHsvay", "IdHsvay", hoSoBaoCaoTc.IdHsvay);
            return View(hoSoBaoCaoTc);
        }

        // GET: Admin/HoSoBaoCaoTcs/Edit/5
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

        // POST: Admin/HoSoBaoCaoTcs/Edit/5
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

            if (!ModelState.IsValid)
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

        // GET: Admin/HoSoBaoCaoTcs/Delete/5
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

        // POST: Admin/HoSoBaoCaoTcs/Delete/5
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
