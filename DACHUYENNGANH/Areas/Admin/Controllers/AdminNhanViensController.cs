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
    public class AdminNhanViensController : Controller
    {
        private readonly DAChuyenNganhContext _context;

        public AdminNhanViensController(DAChuyenNganhContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminNhanViens
        public async Task<IActionResult> Index()
        {
            var dAChuyenNganhContext = _context.NhanViens.Include(n => n.IdChucVuNavigation);
            return View(await dAChuyenNganhContext.ToListAsync());
        }


        [HttpGet]
        public IActionResult Index(string search, string id)
        {
            //ViewData["Getnhanviendetails"] = search;

            //var nhanViens = from x in _context.NhanViens select x;
            ViewBag.ChucVu = _context.ChucVus.ToList();
            var ket = from s in _context.NhanViens
                      join i in _context.ChucVus
                      on s.IdChucVu equals i.IdChucVu
                      select new { s.IdChucVu, s.IdNhanVien, s.TenNhanVien, s.NgaySinh, s.GioiTinh, s.CmndCccd, s.Email, s.Sdt, s.TenDangNhap, s.MatKhau };
            if (!string.IsNullOrEmpty(search))
            {
                ket = ket.Where(x => x.TenNhanVien.Contains(search) || x.IdNhanVien.Contains(search));

            }
            if(id != null)
            {
                ket = ket.Where(x => x.IdChucVu == id);
            }
            List<NhanVien> lNV = new List<NhanVien>();
            foreach (var k in ket)
            {
                NhanVien nv = new NhanVien();
                nv.IdChucVu = k.IdChucVu;
                nv.IdNhanVien = k.IdNhanVien;
                nv.TenNhanVien = k.TenNhanVien;
                nv.GioiTinh = k.GioiTinh;
                nv.NgaySinh = k.NgaySinh;
                nv.CmndCccd = k.CmndCccd;
                nv.Sdt = k.Sdt;
                nv.Email = k.Email;
                nv.TenDangNhap = k.TenDangNhap;
                nv.MatKhau = k.MatKhau;
                lNV.Add(nv);
            }
            return View(lNV);
        }


        // GET: Admin/AdminNhanViens/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.NhanViens == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanViens
                .Include(n => n.IdChucVuNavigation)
                .FirstOrDefaultAsync(m => m.IdNhanVien == id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            return View(nhanVien);
        }

        // GET: Admin/AdminNhanViens/Create
        public IActionResult Create()
        {
            ViewData["IdChucVu"] = new SelectList(_context.ChucVus, "IdChucVu", "TenChucVu");
            return View();
        }

        // POST: Admin/AdminNhanViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNhanVien,TenNhanVien,NgaySinh,CmndCccd,Sdt,GioiTinh,Email,TenDangNhap,MatKhau,IdChucVu")] NhanVien nhanVien)
        {
            if (nhanVien.IdChucVu != null)
            {
                nhanVien.IdNhanVien = GetID.GetIDByFullNameAnDob(nhanVien.TenNhanVien, nhanVien.NgaySinh);
                nhanVien.MatKhau = CreateMD5(nhanVien.MatKhau);
                _context.Add(nhanVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdChucVu"] = new SelectList(_context.ChucVus, "IdChucVu", "TenChucVu", nhanVien.IdChucVu);
            return View(nhanVien);
        }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes); // .NET 5 +

                // Convert the byte array to hexadecimal string prior to .NET 5
                // StringBuilder sb = new System.Text.StringBuilder();
                // for (int i = 0; i < hashBytes.Length; i++)
                // {
                //     sb.Append(hashBytes[i].ToString("X2"));
                // }
                // return sb.ToString();
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login([Bind("TenDangNhap,MatKhau")] NhanVien nhanVien)
        {
            if (nhanVien.TenDangNhap != null && nhanVien.MatKhau != null)
            {
                nhanVien.MatKhau = CreateMD5(nhanVien.MatKhau);
                var nhanvien = _context.NhanViens.FirstOrDefault(x => x.TenDangNhap == nhanVien.TenDangNhap && x.MatKhau == nhanVien.MatKhau);
                if (nhanvien == null)
                {
                    return View();
                }
                string chucvu = nhanvien.IdChucVu;
                if (chucvu == "NVIT1")
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View();
        }


        // GET: Admin/AdminNhanViens/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.NhanViens == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanViens.FindAsync(id);
            if (nhanVien == null)
            {
                return NotFound();
            }
            ViewData["IdChucVu"] = new SelectList(_context.ChucVus, "IdChucVu", "TenChucVu", nhanVien.IdChucVu);
            return View(nhanVien);
        }

        // POST: Admin/AdminNhanViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("IdNhanVien,TenNhanVien,NgaySinh,CmndCccd,Sdt,GioiTinh,Email,TenDangNhap,MatKhau,IdChucVu")] NhanVien nhanVien)
        {
            if (id != nhanVien.IdNhanVien)
            {
                return NotFound();
            }

            if (nhanVien.IdChucVu != null && nhanVien.IdNhanVien != null)
            {
                try
                {
                    _context.Update(nhanVien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhanVienExists(nhanVien.IdNhanVien))
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
            ViewData["IdChucVu"] = new SelectList(_context.ChucVus, "IdChucVu", "TenChucVu", nhanVien.IdChucVu);
            return View(nhanVien);
        }

        // GET: Admin/AdminNhanViens/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.NhanViens == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanViens
                .Include(n => n.IdChucVuNavigation)
                .FirstOrDefaultAsync(m => m.IdNhanVien == id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            return View(nhanVien);
        }

        // POST: Admin/AdminNhanViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.NhanViens == null)
            {
                return Problem("Entity set 'DAChuyenNganhContext.NhanViens'  is null.");
            }
            var nhanVien = await _context.NhanViens.FindAsync(id);
            if (nhanVien != null)
            {
                _context.NhanViens.Remove(nhanVien);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhanVienExists(string id)
        {
            return _context.NhanViens.Any(e => e.IdNhanVien == id);
        }
    }
}
