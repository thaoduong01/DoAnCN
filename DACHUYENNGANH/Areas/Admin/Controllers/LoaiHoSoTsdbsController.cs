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
    public class LoaiHoSoTsdbsController : Controller
    {
        private readonly DAChuyenNganhContext _context;

        public LoaiHoSoTsdbsController(DAChuyenNganhContext context)
        {
            _context = context;
        }

        // GET: Admin/LoaiHoSoTsdbs
        public async Task<IActionResult> Index()
        {
              return View(await _context.LoaiHoSoTsdbs.ToListAsync());
        }

        [HttpGet]
        public IActionResult Index(string search)
        {
            ViewData["Getchucvudetails"] = search;

            var chucvu = from x in _context.LoaiHoSoTsdbs select x;
            if (!string.IsNullOrEmpty(search))
            {
                chucvu = chucvu.Where(x => x.TenLoai.Contains(search));

            }
            return View(chucvu);
        }

        // GET: Admin/LoaiHoSoTsdbs/Details/5
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

        // GET: Admin/LoaiHoSoTsdbs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/LoaiHoSoTsdbs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdLoaiHs,TenLoai")] LoaiHoSoTsdb loaiHoSoTsdb)
        {
            if (!ModelState.IsValid)
            {
                _context.Add(loaiHoSoTsdb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loaiHoSoTsdb);
        }

        // GET: Admin/LoaiHoSoTsdbs/Edit/5
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

        // POST: Admin/LoaiHoSoTsdbs/Edit/5
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

            if (!ModelState.IsValid)
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

        // GET: Admin/LoaiHoSoTsdbs/Delete/5
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

        // POST: Admin/LoaiHoSoTsdbs/Delete/5
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
