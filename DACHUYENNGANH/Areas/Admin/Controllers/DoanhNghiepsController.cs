﻿using System;
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
    public class DoanhNghiepsController : Controller
    {
        private readonly DAChuyenNganhContext _context;

        public DoanhNghiepsController(DAChuyenNganhContext context)
        {
            _context = context;
        }

        // GET: Admin/DoanhNghieps
        public async Task<IActionResult> Index()
        {
            var dAChuyenNganhContext = _context.DoanhNghieps.Include(d => d.IdKhachHangNavigation);
            return View(await dAChuyenNganhContext.ToListAsync());
        }

        [HttpGet]
        public IActionResult Index(string search)
        {
            ViewData["Getdoanhnghiepndetails"] = search;

            var doanhNghieps = from x in _context.DoanhNghieps select x;
            if (!string.IsNullOrEmpty(search))
            {
                doanhNghieps = doanhNghieps.Where(x => x.TenDoanhNghiep.Contains(search) || x.IdDoanhNghiep.Contains(search) );

            }
            return View(doanhNghieps);
        }

        // GET: Admin/DoanhNghieps/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.DoanhNghieps == null)
            {
                return NotFound();
            }

            var doanhNghiep = await _context.DoanhNghieps
                .Include(d => d.IdKhachHangNavigation)
                .FirstOrDefaultAsync(m => m.IdDoanhNghiep == id);
            if (doanhNghiep == null)
            {
                return NotFound();
            }

            return View(doanhNghiep);
        }

        // GET: Admin/DoanhNghieps/Create
        public IActionResult Create()
        {
            ViewData["IdKhachHang"] = new SelectList(_context.KhachHangs, "IdKhachHang", "IdKhachHang");
            return View();
        }

        // POST: Admin/DoanhNghieps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenDoanhNghiep,DiaChi,Sdt,TenNguoiDaiDien,CmndCccd,Email,IdDoanhNghiep,IdKhachHang")] DoanhNghiep doanhNghiep)
        {
            if (doanhNghiep.IdKhachHang != 0 && doanhNghiep.IdDoanhNghiep != null)
            {
                doanhNghiep.IdDoanhNghiep = GetIDDN.GetIDByFullNameAnDobDN();
                
                _context.Add(doanhNghiep);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdKhachHang"] = new SelectList(_context.KhachHangs, "IdKhachHang", "IdKhachHang", doanhNghiep.IdKhachHang);
            return View(doanhNghiep);
        }

        // GET: Admin/DoanhNghieps/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.DoanhNghieps == null)
            {
                return NotFound();
            }

            var doanhNghiep = await _context.DoanhNghieps.FindAsync(id);
            if (doanhNghiep == null)
            {
                return NotFound();
            }
            ViewData["IdKhachHang"] = new SelectList(_context.KhachHangs, "IdKhachHang", "IdKhachHang", doanhNghiep.IdKhachHang);
            return View(doanhNghiep);
        }

        // POST: Admin/DoanhNghieps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TenDoanhNghiep,DiaChi,Sdt,TenNguoiDaiDien,CmndCccd,Email,IdDoanhNghiep,IdKhachHang")] DoanhNghiep doanhNghiep)
        {
            if (id != doanhNghiep.IdDoanhNghiep)
            {
                return NotFound();
            }

            if (doanhNghiep.IdKhachHang != 0 && doanhNghiep.IdDoanhNghiep != null)
            {
                try
                {
                    _context.Update(doanhNghiep);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoanhNghiepExists(doanhNghiep.IdDoanhNghiep))
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
            ViewData["IdKhachHang"] = new SelectList(_context.KhachHangs, "IdKhachHang", "IdKhachHang", doanhNghiep.IdKhachHang);
            return View(doanhNghiep);
        }

        // GET: Admin/DoanhNghieps/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.DoanhNghieps == null)
            {
                return NotFound();
            }

            var doanhNghiep = await _context.DoanhNghieps
                .Include(d => d.IdKhachHangNavigation)
                .FirstOrDefaultAsync(m => m.IdDoanhNghiep == id);
            if (doanhNghiep == null)
            {
                return NotFound();
            }

            return View(doanhNghiep);
        }

        // POST: Admin/DoanhNghieps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.DoanhNghieps == null)
            {
                return Problem("Entity set 'DAChuyenNganhContext.DoanhNghieps'  is null.");
            }
            var doanhNghiep = await _context.DoanhNghieps.FindAsync(id);
            if (doanhNghiep != null)
            {
                _context.DoanhNghieps.Remove(doanhNghiep);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoanhNghiepExists(string id)
        {
          return _context.DoanhNghieps.Any(e => e.IdDoanhNghiep == id);
        }
    }
}
