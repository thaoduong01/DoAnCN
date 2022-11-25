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
    public class HoSoTaiSanDbsController : Controller
    {
        private readonly DAChuyenNganhContext _context;

        public HoSoTaiSanDbsController(DAChuyenNganhContext context)
        {
            _context = context;
        }

        // GET: HoSoTaiSanDbs
        public async Task<IActionResult> Index()
        {
            var dAChuyenNganhContext = _context.HoSoTaiSanDbs.Include(h => h.IdHsvayNavigation).Include(h => h.IdLoaiHsNavigation);
            return View(await dAChuyenNganhContext.ToListAsync());
        }

        // GET: HoSoTaiSanDbs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HoSoTaiSanDbs == null)
            {
                return NotFound();
            }

            var hoSoTaiSanDb = await _context.HoSoTaiSanDbs
                .Include(h => h.IdHsvayNavigation)
                .Include(h => h.IdLoaiHsNavigation)
                .FirstOrDefaultAsync(m => m.IdHsdb == id);
            if (hoSoTaiSanDb == null)
            {
                return NotFound();
            }

            return View(hoSoTaiSanDb);
        }

        // GET: HoSoTaiSanDbs/Create
        public IActionResult Create()
        {
            ViewData["IdHsvay"] = new SelectList(_context.HoSoVayDoanhNghieps, "IdHsvay", "IdHsvay");
            ViewData["IdLoaiHs"] = new SelectList(_context.LoaiHoSoTsdbs, "IdLoaiHs", "IdLoaiHs");
            return View();
        }

        // POST: HoSoTaiSanDbs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdHsdb,DcnsoHuuDat,HdtaiSan,SoNhaDat,TbnopPhiNd,SoDangKiem,ChungNhanBaoHiem,NgayNhanHs,IdLoaiHs,IdHsvay")] HoSoTaiSanDb hoSoTaiSanDb)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hoSoTaiSanDb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdHsvay"] = new SelectList(_context.HoSoVayDoanhNghieps, "IdHsvay", "IdHsvay", hoSoTaiSanDb.IdHsvay);
            ViewData["IdLoaiHs"] = new SelectList(_context.LoaiHoSoTsdbs, "IdLoaiHs", "IdLoaiHs", hoSoTaiSanDb.IdLoaiHs);
            return View(hoSoTaiSanDb);
        }

        // GET: HoSoTaiSanDbs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HoSoTaiSanDbs == null)
            {
                return NotFound();
            }

            var hoSoTaiSanDb = await _context.HoSoTaiSanDbs.FindAsync(id);
            if (hoSoTaiSanDb == null)
            {
                return NotFound();
            }
            ViewData["IdHsvay"] = new SelectList(_context.HoSoVayDoanhNghieps, "IdHsvay", "IdHsvay", hoSoTaiSanDb.IdHsvay);
            ViewData["IdLoaiHs"] = new SelectList(_context.LoaiHoSoTsdbs, "IdLoaiHs", "IdLoaiHs", hoSoTaiSanDb.IdLoaiHs);
            return View(hoSoTaiSanDb);
        }

        // POST: HoSoTaiSanDbs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdHsdb,DcnsoHuuDat,HdtaiSan,SoNhaDat,TbnopPhiNd,SoDangKiem,ChungNhanBaoHiem,NgayNhanHs,IdLoaiHs,IdHsvay")] HoSoTaiSanDb hoSoTaiSanDb)
        {
            if (id != hoSoTaiSanDb.IdHsdb)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoSoTaiSanDb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoSoTaiSanDbExists(hoSoTaiSanDb.IdHsdb))
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
            ViewData["IdHsvay"] = new SelectList(_context.HoSoVayDoanhNghieps, "IdHsvay", "IdHsvay", hoSoTaiSanDb.IdHsvay);
            ViewData["IdLoaiHs"] = new SelectList(_context.LoaiHoSoTsdbs, "IdLoaiHs", "IdLoaiHs", hoSoTaiSanDb.IdLoaiHs);
            return View(hoSoTaiSanDb);
        }

        // GET: HoSoTaiSanDbs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HoSoTaiSanDbs == null)
            {
                return NotFound();
            }

            var hoSoTaiSanDb = await _context.HoSoTaiSanDbs
                .Include(h => h.IdHsvayNavigation)
                .Include(h => h.IdLoaiHsNavigation)
                .FirstOrDefaultAsync(m => m.IdHsdb == id);
            if (hoSoTaiSanDb == null)
            {
                return NotFound();
            }

            return View(hoSoTaiSanDb);
        }

        // POST: HoSoTaiSanDbs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HoSoTaiSanDbs == null)
            {
                return Problem("Entity set 'DAChuyenNganhContext.HoSoTaiSanDbs'  is null.");
            }
            var hoSoTaiSanDb = await _context.HoSoTaiSanDbs.FindAsync(id);
            if (hoSoTaiSanDb != null)
            {
                _context.HoSoTaiSanDbs.Remove(hoSoTaiSanDb);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoSoTaiSanDbExists(int id)
        {
          return _context.HoSoTaiSanDbs.Any(e => e.IdHsdb == id);
        }
    }
}
