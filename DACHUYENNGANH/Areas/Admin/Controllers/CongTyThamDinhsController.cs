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
    public class CongTyThamDinhsController : Controller
    {
        private readonly DAChuyenNganhContext _context;

        public CongTyThamDinhsController(DAChuyenNganhContext context)
        {
            _context = context;
        }

        // GET: Admin/CongTyThamDinhs
        public async Task<IActionResult> Index()
        {
              return View(await _context.CongTyThamDinhs.ToListAsync());
        }

        [HttpGet]
        public IActionResult Index(string search)
        {
            ViewData["Getcongtydetails"] = search;

            var congty = from x in _context.CongTyThamDinhs select x;
            if (!string.IsNullOrEmpty(search))
            {
                congty = congty.Where(x => x.TenCty.Contains(search) || x.IdCongTy.Contains(search));

            }
            return View(congty);
        }
        // GET: Admin/CongTyThamDinhs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.CongTyThamDinhs == null)
            {
                return NotFound();
            }

            var congTyThamDinh = await _context.CongTyThamDinhs
                .FirstOrDefaultAsync(m => m.IdCongTy == id);
            if (congTyThamDinh == null)
            {
                return NotFound();
            }

            return View(congTyThamDinh);
        }

        // GET: Admin/CongTyThamDinhs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/CongTyThamDinhs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCongTy,TenCty,DiaChi,Sdt,Email")] CongTyThamDinh congTyThamDinh)
        {
            if (ModelState.IsValid)
            {
                congTyThamDinh.IdCongTy = GetIDCTY.GetIDByFullNameAnDobCTY();
                _context.Add(congTyThamDinh);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(congTyThamDinh);
        }

        // GET: Admin/CongTyThamDinhs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.CongTyThamDinhs == null)
            {
                return NotFound();
            }

            var congTyThamDinh = await _context.CongTyThamDinhs.FindAsync(id);
            if (congTyThamDinh == null)
            {
                return NotFound();
            }
            return View(congTyThamDinh);
        }

        // POST: Admin/CongTyThamDinhs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("IdCongTy,TenCty,DiaChi,Sdt,Email")] CongTyThamDinh congTyThamDinh)
        {
            if (id != congTyThamDinh.IdCongTy)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(congTyThamDinh);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CongTyThamDinhExists(congTyThamDinh.IdCongTy))
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
            return View(congTyThamDinh);
        }

        // GET: Admin/CongTyThamDinhs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.CongTyThamDinhs == null)
            {
                return NotFound();
            }

            var congTyThamDinh = await _context.CongTyThamDinhs
                .FirstOrDefaultAsync(m => m.IdCongTy == id);
            if (congTyThamDinh == null)
            {
                return NotFound();
            }

            return View(congTyThamDinh);
        }

        // POST: Admin/CongTyThamDinhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.CongTyThamDinhs == null)
            {
                return Problem("Entity set 'DAChuyenNganhContext.CongTyThamDinhs'  is null.");
            }
            var congTyThamDinh = await _context.CongTyThamDinhs.FindAsync(id);
            if (congTyThamDinh != null)
            {
                _context.CongTyThamDinhs.Remove(congTyThamDinh);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CongTyThamDinhExists(string id)
        {
          return _context.CongTyThamDinhs.Any(e => e.IdCongTy == id);
        }
    }
}
