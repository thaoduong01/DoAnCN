using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DACHUYENNGANH.Models;
using DACHUYENNGANH.Helpers.FileManager;
using DACHUYENNGANH.Models.HoSo;
using System.Net.Http.Headers;

namespace DACHUYENNGANH.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HoSoThamDinhsController : Controller
    {
        private readonly DAChuyenNganhContext _context;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public HoSoThamDinhsController(DAChuyenNganhContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        // GET: Admin/HoSoThamDinhs
        public async Task<IActionResult> Index()
        {
            var dAChuyenNganhContext = _context.HoSoThamDinhs.Include(h => h.IdCongTyNavigation).Include(h => h.IdHsdbNavigation);
            return View(await dAChuyenNganhContext.ToListAsync());
        }

        [HttpGet]
        public IActionResult Index(string search, string id)
        {
            ViewData["Getchucvudetails"] = search;
            ViewBag.CongTy = _context.CongTyThamDinhs;

            /*var hsvay = from x in _context.HoSoThamDinhs select x*/
            ;

            var ketDN = from s in _context.HoSoThamDinhs
                        join i in _context.CongTyThamDinhs
                        on s.IdCongTy equals i.IdCongTy
                        select new { s.IdCongTy, s.NgayThamDinh, s.IdHsthamDinh, s.BaoCaoThamDinh, s.SoTienThamDinh, s.TenNguoiThamDinh, s.CmndCccd, s.NgayNhanHoSo, s.IdHsdb };

            if (!string.IsNullOrEmpty(search))
            {
                ketDN = ketDN.Where(x => x.TenNguoiThamDinh.Contains(search)).OrderByDescending(x => x.NgayNhanHoSo);

            }
            if (id != null)
            {
                ketDN = ketDN.Where(x => x.IdCongTy == id);
            }
            List<HoSoThamDinh> hoSoThamDinhs = new List<HoSoThamDinh>();
            foreach (var k in ketDN)
            {
                HoSoThamDinh hstd = new HoSoThamDinh();
                hstd.BaoCaoThamDinh = k.BaoCaoThamDinh;
                hstd.IdCongTy = k.IdCongTy;
                hstd.IdHsthamDinh = k.IdHsthamDinh;
                hstd.IdHsdb = k.IdHsdb;
                hstd.SoTienThamDinh = k.SoTienThamDinh;
                hstd.TenNguoiThamDinh = k.TenNguoiThamDinh;
                hstd.CmndCccd = k.CmndCccd;
                hstd.NgayNhanHoSo = k.NgayNhanHoSo;
                hstd.NgayThamDinh = k.NgayThamDinh;

                hoSoThamDinhs.Add(hstd);
            }
            return View(hoSoThamDinhs);
        }
        // GET: Admin/HoSoThamDinhs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HoSoThamDinhs == null)
            {
                return NotFound();
            }

            var hoSoThamDinh = await _context.HoSoThamDinhs
                .Include(h => h.IdCongTyNavigation)
                .Include(h => h.IdHsdbNavigation)
                .FirstOrDefaultAsync(m => m.IdHsthamDinh == id);
            if (hoSoThamDinh == null)
            {
                return NotFound();
            }

            return View(hoSoThamDinh);
        }

        // GET: Admin/HoSoThamDinhs/Create
        public IActionResult Create()
        {
            ViewData["IdCongTy"] = new SelectList(_context.CongTyThamDinhs, "IdCongTy", "TenCty");
            ViewData["IdHsdb"] = new SelectList(_context.HoSoTaiSanDbs, "IdHsdb", "IdHsdb");
            return View();
        }

        // POST: Admin/HoSoThamDinhs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] HoSoThamDinhCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            HoSoThamDinh hs = new HoSoThamDinh()
            {
                IdHsdb = request.IdHsdb,
                IdCongTy = request.IdCongTy,
                SoTienThamDinh = request.SoTienThamDinh,
                NgayThamDinh = request.NgayThamDinh,
                NgayNhanHoSo = DateTime.Now,
                BaoCaoThamDinh = await SaveFile(request.BaoCaoThamDinh),
                TenNguoiThamDinh = request.TenNguoiThamDinh,
                CmndCccd = request.CmndCccd


            };
            _context.Add(hs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            ModelState.AddModelError("", "Thêm Hồ sơ pháp lý thất bạn!!");
            return View(request);
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }


        // GET: Admin/HoSoThamDinhs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HoSoThamDinhs == null)
            {
                return NotFound();
            }

            var hoSoThamDinh = await _context.HoSoThamDinhs.FindAsync(id);
            if (hoSoThamDinh == null)
            {
                return NotFound();
            }
            ViewData["IdCongTy"] = new SelectList(_context.CongTyThamDinhs, "IdCongTy", "TenCty", hoSoThamDinh.IdCongTy);
            ViewData["IdHsdb"] = new SelectList(_context.HoSoTaiSanDbs, "IdHsdb", "IdHsdb", hoSoThamDinh.IdHsdb);
            return View(hoSoThamDinh);
        }

        // POST: Admin/HoSoThamDinhs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] HoSoThamDinhEditRequest request)
        {
            var hoso = _context.HoSoThamDinhs.Find(id);
            hoso.SoTienThamDinh = request.SoTienThamDinh;
            hoso.CmndCccd = request.CmndCccd;
            hoso.TenNguoiThamDinh = request.TenNguoiThamDinh;
            hoso.NgayThamDinh = request.NgayThamDinh;
            if (request.BaoCaoThamDinh != null)
            {
                hoso.BaoCaoThamDinh = await SaveFile(request.BaoCaoThamDinh);
            }


            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            ModelState.AddModelError("", "Cập nhật thông tin Hồ sơ thất bại!!");
            return View(request);
        }

        // GET: Admin/HoSoThamDinhs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HoSoThamDinhs == null)
            {
                return NotFound();
            }

            var hoSoThamDinh = await _context.HoSoThamDinhs
                .Include(h => h.IdCongTyNavigation)
                .Include(h => h.IdHsdbNavigation)
                .FirstOrDefaultAsync(m => m.IdHsthamDinh == id);
            if (hoSoThamDinh == null)
            {
                return NotFound();
            }

            return View(hoSoThamDinh);
        }

        // POST: Admin/HoSoThamDinhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HoSoThamDinhs == null)
            {
                return Problem("Entity set 'DAChuyenNganhContext.HoSoThamDinhs'  is null.");
            }
            var hoSoThamDinh = await _context.HoSoThamDinhs.FindAsync(id);
            if (hoSoThamDinh != null)
            {
                _context.HoSoThamDinhs.Remove(hoSoThamDinh);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoSoThamDinhExists(int id)
        {
          return _context.HoSoThamDinhs.Any(e => e.IdHsthamDinh == id);
        }
    }
}
