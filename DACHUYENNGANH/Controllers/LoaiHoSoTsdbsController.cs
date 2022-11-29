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
    public class LoaiHoSoTsdbsController : Controller
    {
        private readonly DAChuyenNganhContext _context;

        public LoaiHoSoTsdbsController(DAChuyenNganhContext context)
        {
            _context = context;
        }

        // GET: LoaiHoSoTsdbs
        public async Task<IActionResult> Index()
        {
              return View(await _context.LoaiHoSoTsdbs.ToListAsync());
        }

        // GET: LoaiHoSoTsdbs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LoaiHoSoTsdbs == null)
            {
                return NotFound();
            }

            var loaiHoSoTsdb = await _context.LoaiHoSoTsdbs
                .FirstOrDefaultAsync(m => m.IdLoaiHs == id);
            if (loaiHoSoTsdb == null)
            {
                return NotFound();
            }

            return View(loaiHoSoTsdb);
        }

        // GET: LoaiHoSoTsdbs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LoaiHoSoTsdbs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLoaiHs,TenLoai")] LoaiHoSoTsdb loaiHoSoTsdb)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loaiHoSoTsdb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loaiHoSoTsdb);
        }

        // GET: LoaiHoSoTsdbs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LoaiHoSoTsdbs == null)
            {
                return NotFound();
            }

            var loaiHoSoTsdb = await _context.LoaiHoSoTsdbs.FindAsync(id);
            if (loaiHoSoTsdb == null)
            {
                return NotFound();
            }
            return View(loaiHoSoTsdb);
        }

        // POST: LoaiHoSoTsdbs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdLoaiHs,TenLoai")] LoaiHoSoTsdb loaiHoSoTsdb)
        {
            if (id != loaiHoSoTsdb.IdLoaiHs)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loaiHoSoTsdb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaiHoSoTsdbExists(loaiHoSoTsdb.IdLoaiHs))
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
            return View(loaiHoSoTsdb);
        }

        // GET: LoaiHoSoTsdbs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LoaiHoSoTsdbs == null)
            {
                return NotFound();
            }

            var loaiHoSoTsdb = await _context.LoaiHoSoTsdbs
                .FirstOrDefaultAsync(m => m.IdLoaiHs == id);
            if (loaiHoSoTsdb == null)
            {
                return NotFound();
            }

            return View(loaiHoSoTsdb);
        }

        // POST: LoaiHoSoTsdbs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LoaiHoSoTsdbs == null)
            {
                return Problem("Entity set 'DAChuyenNganhContext.LoaiHoSoTsdbs'  is null.");
            }
            var loaiHoSoTsdb = await _context.LoaiHoSoTsdbs.FindAsync(id);
            if (loaiHoSoTsdb != null)
            {
                _context.LoaiHoSoTsdbs.Remove(loaiHoSoTsdb);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoaiHoSoTsdbExists(int id)
        {
          return _context.LoaiHoSoTsdbs.Any(e => e.IdLoaiHs == id);
        }
    }
}
