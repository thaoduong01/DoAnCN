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
    public class HoSoBaoCaoTcsController : Controller
    {
        private readonly DAChuyenNganhContext _context;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public HoSoBaoCaoTcsController(DAChuyenNganhContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        // GET: HoSoBaoCaoTcs
        public async Task<IActionResult> Index()
        {
            var dAChuyenNganhContext = _context.HoSoBaoCaoTcs.Include(h => h.IdHsvayNavigation);
            return View(await dAChuyenNganhContext.ToListAsync());
        }


        [HttpGet]
        public IActionResult Index(string search, int id)
        {
            ViewData["Getchucvudetails"] = search;

            //var baocao = from x in _context.HoSoBaoCaoTcs select x;

            ViewBag.HoSoVay = _context.HoSoVayDoanhNghieps;

            if (_context.HoSoBaoCaoTcs == null)
            {
                return Problem("Entity set 'MvcHoSoBaoCaoTcContext.HoSoBaoCaoTcs'  is null.");
            }
            var ket = from s in _context.HoSoBaoCaoTcs
                      join i in _context.HoSoVayDoanhNghieps
                      on s.IdHsvay equals i.IdHsvay
                      select new { s.ToVat, s.IdHsvay, s.NgayNhanHs, s.BctaiChinh, s.HopDongMuaBan, s.HopDongSdld, s.IdBctc, s.SaoKeTknh };

            if (!string.IsNullOrEmpty(search))
            {
                ket = ket.Where(x => x.IdHsvay.Contains(search)).OrderByDescending(x => x.NgayNhanHs);

            }
            if (id != 0)
            {
                ket = ket.Where(x => x.IdBctc == id);
            }
            List<HoSoBaoCaoTc> listHsbaocao = new List<HoSoBaoCaoTc>();
            foreach (var k in ket)
            {
                HoSoBaoCaoTc hsbc = new HoSoBaoCaoTc();
                hsbc.IdBctc = k.IdBctc;
                hsbc.IdHsvay = k.IdHsvay;
                hsbc.ToVat = k.ToVat;
                hsbc.SaoKeTknh = k.SaoKeTknh;
                hsbc.BctaiChinh = k.BctaiChinh;
                hsbc.HopDongMuaBan = k.HopDongMuaBan;
                hsbc.HopDongSdld = k.HopDongSdld;
                hsbc.NgayNhanHs = k.NgayNhanHs;
                listHsbaocao.Add(hsbc);
            }
            return View(listHsbaocao);
        }

        // GET: HoSoBaoCaoTcs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HoSoBaoCaoTcs == null)
            {
                return NotFound();
            }

            var hoSoBaoCaoTc = await _context.HoSoBaoCaoTcs
                .Include(h => h.IdHsvayNavigation)
                .FirstOrDefaultAsync(m => m.IdBctc == id);
            if (hoSoBaoCaoTc == null)
            {
                return NotFound();
            }

            return View(hoSoBaoCaoTc);
        }

        // GET: HoSoBaoCaoTcs/Create
        public IActionResult Create()
        {
            ViewData["IdHsvay"] = new SelectList(_context.HoSoVayDoanhNghieps, "IdHsvay", "IdHsvay");
            return View();
        }

        // POST: HoSoBaoCaoTcs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] HoSoBaoCaoTCCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            HoSoBaoCaoTc hs = new HoSoBaoCaoTc()
            {
                IdHsvay = request.IdHsvay,
                ToVat = await SaveFile(request.ToVat),
                HopDongSdld = await SaveFile(request.HopDongSdld),
                HopDongMuaBan = await SaveFile(request.HopDongMuaBan),
                SaoKeTknh = await SaveFile(request.SaoKeTknh),
                NgayNhanHs = DateTime.Now,
                BctaiChinh = await SaveFile(request.BctaiChinh),


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


        // GET: HoSoBaoCaoTcs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HoSoBaoCaoTcs == null)
            {
                return NotFound();
            }

            var hoSoBaoCaoTc = await _context.HoSoBaoCaoTcs.FindAsync(id);
            if (hoSoBaoCaoTc == null)
            {
                return NotFound();
            }
            ViewData["IdHsvay"] = new SelectList(_context.HoSoVayDoanhNghieps, "IdHsvay", "IdHsvay", hoSoBaoCaoTc.IdHsvay);
            return View(hoSoBaoCaoTc);
        }

        // POST: HoSoBaoCaoTcs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdBctc,ToVat,HopDongSdld,HopDongMuaBan,SaoKeTknh,NgayNhanHs,BctaiChinh,IdHsvay")] HoSoBaoCaoTc hoSoBaoCaoTc)
        {
            if (id != hoSoBaoCaoTc.IdBctc)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoSoBaoCaoTc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoSoBaoCaoTcExists(hoSoBaoCaoTc.IdBctc))
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
            ViewData["IdHsvay"] = new SelectList(_context.HoSoVayDoanhNghieps, "IdHsvay", "IdHsvay", hoSoBaoCaoTc.IdHsvay);
            return View(hoSoBaoCaoTc);
        }

        // GET: HoSoBaoCaoTcs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HoSoBaoCaoTcs == null)
            {
                return NotFound();
            }

            var hoSoBaoCaoTc = await _context.HoSoBaoCaoTcs
                .Include(h => h.IdHsvayNavigation)
                .FirstOrDefaultAsync(m => m.IdBctc == id);
            if (hoSoBaoCaoTc == null)
            {
                return NotFound();
            }

            return View(hoSoBaoCaoTc);
        }

        // POST: HoSoBaoCaoTcs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HoSoBaoCaoTcs == null)
            {
                return Problem("Entity set 'DAChuyenNganhContext.HoSoBaoCaoTcs'  is null.");
            }
            var hoSoBaoCaoTc = await _context.HoSoBaoCaoTcs.FindAsync(id);
            if (hoSoBaoCaoTc != null)
            {
                _context.HoSoBaoCaoTcs.Remove(hoSoBaoCaoTc);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoSoBaoCaoTcExists(int id)
        {
            return _context.HoSoBaoCaoTcs.Any(e => e.IdBctc == id);
        }
    }
}
