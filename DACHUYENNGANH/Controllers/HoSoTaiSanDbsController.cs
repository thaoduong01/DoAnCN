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

namespace DACHUYENNGANH.Controllers
{
    public class HoSoTaiSanDbsController : Controller
    {
        private readonly DAChuyenNganhContext _context;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        public HoSoTaiSanDbsController(DAChuyenNganhContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        // GET: HoSoTaiSanDbs
        public async Task<IActionResult> Index()
        {
            var dAChuyenNganhContext = _context.HoSoTaiSanDbs.Include(h => h.IdHsvayNavigation).Include(h => h.IdLoaiHsNavigation);
            return View(await dAChuyenNganhContext.ToListAsync());
        }

        [HttpGet]
        public IActionResult Index(string search, int id)
        {
            ViewData["Getphaplydetails"] = search;

            //var baocao = from x in _context.HoSoBaoCaoTcs select x;

            ViewBag.LoaiHSDB = _context.LoaiHoSoTsdbs;
            var ket = from s in _context.HoSoTaiSanDbs
                      join i in _context.HoSoVayDoanhNghieps
                      on s.IdHsvay equals i.IdHsvay
                      select new { s.IdHsvay, s.IdLoaiHs, s.NgayNhanHs, s.ChungNhanBaoHiem, s.DcnsoHuuDat, s.SoNhaDat, s.HdtaiSan, s.IdHsdb, s.SoDangKiem, s.TbnopPhiNd };

            if (!string.IsNullOrEmpty(search))
            {
                ket = ket.Where(x => x.SoDangKiem.Contains(search) || x.IdHsvay.Contains(search));

            }
            if (id != 0)
            {
                ket = ket.Where(x => x.IdLoaiHs == id);
            }
            List<HoSoTaiSanDb> hoSoTaiSanDbs = new List<HoSoTaiSanDb>();
            foreach (var k in ket)
            {
                HoSoTaiSanDb hsts = new HoSoTaiSanDb();
                hsts.IdHsdb = k.IdHsdb;
                hsts.IdLoaiHs = k.IdLoaiHs;
                hsts.IdHsvay = k.IdHsvay;
                hsts.ChungNhanBaoHiem = k.ChungNhanBaoHiem;
                hsts.DcnsoHuuDat = k.DcnsoHuuDat;
                hsts.SoNhaDat = k.SoNhaDat;
                hsts.HdtaiSan = k.HdtaiSan;
                hsts.SoDangKiem = k.SoDangKiem;
                hsts.TbnopPhiNd = k.TbnopPhiNd;
                hsts.NgayNhanHs = k.NgayNhanHs;
                hoSoTaiSanDbs.Add(hsts);
            }
            return View(hoSoTaiSanDbs);
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
            ViewData["IdLoaiHs"] = new SelectList(_context.LoaiHoSoTsdbs, "IdLoaiHs", "TenLoai");
            return View();
        }

        // POST: HoSoTaiSanDbs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] HoSoTaiSanDBCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            HoSoTaiSanDb hs = new HoSoTaiSanDb()
            {
                IdHsvay = request.IdHsvay,
                IdLoaiHs = request.IdLoaiHs,
                DcnsoHuuDat = await SaveFile(request.DcnsoHuuDat),
                HdtaiSan = await SaveFile(request.HdtaiSan),
                SoNhaDat = await SaveFile(request.SoNhaDat),
                TbnopPhiNd = await SaveFile(request.TbnopPhiNd),
                SoDangKiem = await SaveFile(request.SoDangKiem),
                ChungNhanBaoHiem = await SaveFile(request.ChungNhanBaoHiem),
                NgayNhanHs = DateTime.Now


            };
            _context.Add(hs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            ModelState.AddModelError("", "Thêm Hồ sơ thất bại!!");
            return View(request);
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
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
            ViewData["IdLoaiHs"] = new SelectList(_context.LoaiHoSoTsdbs, "IdLoaiHs", "TenLoai", hoSoTaiSanDb.IdLoaiHs);
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

            if (!ModelState.IsValid)
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
