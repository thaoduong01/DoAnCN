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
    public class TheTinDungsController : Controller
    {
        private readonly DAChuyenNganhContext _context;

        public TheTinDungsController(DAChuyenNganhContext context)
        {
            _context = context;
        }

        // GET: TheTinDungs
        public async Task<IActionResult> Index()
        {
            var dAChuyenNganhContext = _context.TheTinDungs.Include(t => t.IdHstinDungNavigation);
            return View(await dAChuyenNganhContext.ToListAsync());
        }

        [HttpGet]
        public IActionResult Index(string search, int id)
        {
            ViewData["Getchucvudetails"] = search;
            ViewBag.HoSoTD = _context.HoSoTinDungs;
            

            var ketDN = from s in _context.TheTinDungs
                        join i in _context.HoSoTinDungs
                        on s.IdHstinDung equals i.IdHstinDung
                        select new { s.IdHstinDung, s.NgayNhanThe, s.NgayMoThe, s.SoTrenThe, s.TenTk, s.Stk };

            if (!string.IsNullOrEmpty(search))
            {
                ketDN = ketDN.Where(x => x.Stk.Contains(search) || x.TenTk.Contains(search) || x.SoTrenThe.Contains(search));

            }
            if (id != 0)
            {
                ketDN = ketDN.Where(x => x.IdHstinDung == id);
            }
            List<TheTinDung> theTinDungs = new List<TheTinDung>();
            foreach (var k in ketDN)
            {
                TheTinDung ttd = new TheTinDung();
                ttd.IdHstinDung = k.IdHstinDung;
                ttd.Stk = k.Stk;
                ttd.TenTk = k.TenTk;
                ttd.SoTrenThe = k.SoTrenThe;
                ttd.NgayNhanThe = k.NgayNhanThe;
                ttd.NgayMoThe = k.NgayMoThe;

                theTinDungs.Add(ttd);
            }
            return View(theTinDungs);
        }

        // GET: TheTinDungs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.TheTinDungs == null)
            {
                return NotFound();
            }

            var theTinDung = await _context.TheTinDungs
                .Include(t => t.IdHstinDungNavigation)
                .FirstOrDefaultAsync(m => m.Stk == id);
            if (theTinDung == null)
            {
                return NotFound();
            }

            return View(theTinDung);
        }

        // GET: TheTinDungs/Create
        public IActionResult Create()
        {
            ViewData["IdHstinDung"] = new SelectList(_context.HoSoTinDungs, "IdHstinDung", "IdHstinDung");
            return View();
        }

        // POST: TheTinDungs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Stk,TenTk,SoTrenThe,NgayMoThe,NgayNhanThe,IdHstinDung")] TheTinDung theTinDung)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(theTinDung);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdHstinDung"] = new SelectList(_context.HoSoTinDungs, "IdHstinDung", "IdHstinDung", theTinDung.IdHstinDung);
            return View(theTinDung);
        }

        // GET: TheTinDungs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.TheTinDungs == null)
            {
                return NotFound();
            }

            var theTinDung = await _context.TheTinDungs.FindAsync(id);
            if (theTinDung == null)
            {
                return NotFound();
            }
            ViewData["IdHstinDung"] = new SelectList(_context.HoSoTinDungs, "IdHstinDung", "IdHstinDung", theTinDung.IdHstinDung);
            return View(theTinDung);
        }

        // POST: TheTinDungs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Stk,TenTk,SoTrenThe,NgayMoThe,NgayNhanThe,IdHstinDung")] TheTinDung theTinDung)
        {
            if (id != theTinDung.Stk)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(theTinDung);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TheTinDungExists(theTinDung.Stk))
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
            ViewData["IdHstinDung"] = new SelectList(_context.HoSoTinDungs, "IdHstinDung", "IdHstinDung", theTinDung.IdHstinDung);
            return View(theTinDung);
        }

        // GET: TheTinDungs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.TheTinDungs == null)
            {
                return NotFound();
            }

            var theTinDung = await _context.TheTinDungs
                .Include(t => t.IdHstinDungNavigation)
                .FirstOrDefaultAsync(m => m.Stk == id);
            if (theTinDung == null)
            {
                return NotFound();
            }

            return View(theTinDung);
        }

        // POST: TheTinDungs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.TheTinDungs == null)
            {
                return Problem("Entity set 'DAChuyenNganhContext.TheTinDungs'  is null.");
            }
            var theTinDung = await _context.TheTinDungs.FindAsync(id);
            if (theTinDung != null)
            {
                _context.TheTinDungs.Remove(theTinDung);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TheTinDungExists(string id)
        {
          return _context.TheTinDungs.Any(e => e.Stk == id);
        }
    }
}
