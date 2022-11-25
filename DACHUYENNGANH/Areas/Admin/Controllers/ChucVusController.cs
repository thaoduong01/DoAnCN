using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using DACHUYENNGANH.Models;
using PagedList.Core;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DACHUYENNGANH.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChucVusController : Controller
    {
        private readonly DAChuyenNganhContext _context;

        public ChucVusController(DAChuyenNganhContext context)
        {
            _context = context;
        }

        //GET: Admin/ChucVus
        public IActionResult Index(int page = 1)
        {
            page = page < 1 ? 1 : page;
            int pageSize = 3;
            var chucVus = _context.ChucVus.ToPagedList(page, pageSize);
            return View(chucVus);
        }


        [HttpGet]
        public IActionResult Index(string search)
        {
            ViewData["Getchucvudetails"] = search;

            var chucvu = from x in _context.ChucVus select x;
            if (!string.IsNullOrEmpty(search))
            {
                chucvu = chucvu.Where(x => x.TenChucVu.Contains(search) || x.IdChucVu.Contains(search));

            }
            return View(chucvu);
        }

        //public IActionResult Page(int page = 1)
        //{
        //    page = page < 1 ? 1 : page;
        //    int pageSize = 3;
        //    var chucVus = _context.ChucVus.ToPagedList(page, pageSize);
        //    return View(chucVus);
        //}

        // GET: Admin/ChucVus/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.ChucVus == null)
            {
                return NotFound();
            }

            var chucVu = await _context.ChucVus
                .FirstOrDefaultAsync(m => m.IdChucVu == id);
            if (chucVu == null)
            {
                return NotFound();
            }

            return View(chucVu);
        }

        // GET: Admin/ChucVus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ChucVus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdChucVu,TenChucVu")] ChucVu chucVu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chucVu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chucVu);
        }

        // GET: Admin/ChucVus/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.ChucVus == null)
            {
                return NotFound();
            }

            var chucVu = await _context.ChucVus.FindAsync(id);
            if (chucVu == null)
            {
                return NotFound();
            }
            return View(chucVu);
        }

        // POST: Admin/ChucVus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("IdChucVu,TenChucVu")] ChucVu chucVu)
        {
            if (id != chucVu.IdChucVu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chucVu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChucVuExists(chucVu.IdChucVu))
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
            return View(chucVu);
        }

        // GET: Admin/ChucVus/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.ChucVus == null)
            {
                return NotFound();
            }

            var chucVu = await _context.ChucVus
                .FirstOrDefaultAsync(m => m.IdChucVu == id);
            if (chucVu == null)
            {
                return NotFound();
            }

            return View(chucVu);
        }

        // POST: Admin/ChucVus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.ChucVus == null)
            {
                return Problem("Entity set 'DAChuyenNganhContext.ChucVus'  is null.");
            }
            var chucVu = await _context.ChucVus.FindAsync(id);
            if (chucVu != null)
            {
                _context.ChucVus.Remove(chucVu);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChucVuExists(string id)
        {
          return _context.ChucVus.Any(e => e.IdChucVu == id);
        }
    }
}
