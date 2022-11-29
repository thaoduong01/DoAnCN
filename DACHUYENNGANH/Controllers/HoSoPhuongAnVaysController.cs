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
    public class HoSoPhuongAnVaysController : Controller
    {
        private readonly DAChuyenNganhContext _context;

        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        public HoSoPhuongAnVaysController(DAChuyenNganhContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        // GET: HoSoPhuongAnVays
        public async Task<IActionResult> Index()
        {
            var dAChuyenNganhContext = _context.HoSoPhuongAnVays.Include(h => h.IdHsvayNavigation);
            return View(await dAChuyenNganhContext.ToListAsync());
        }

        [HttpGet]
        public IActionResult Index(string search, string id)
        {
            ViewData["Getchucvudetails"] = search;

            //var hspavay = from x in _context.HoSoPhuongAnVays select x;

            ViewBag.HoSoVay = _context.HoSoVayDoanhNghieps;
            var ket = from s in _context.HoSoPhuongAnVays
                      join i in _context.HoSoVayDoanhNghieps
                      on s.IdHsvay equals i.IdHsvay
                      select new { s.IdHsvay, s.IdHspavay, s.NgayNhanHs, s.KeHoachTraNo, s.PhuongAnKd };

            if (!string.IsNullOrEmpty(search))
            {
                ket = ket.Where(x => x.PhuongAnKd.Contains(search));

            }
            if (id != null)
            {
                ket = ket.Where(x => x.IdHsvay == id).OrderByDescending(x => x.NgayNhanHs);
            }
            List<HoSoPhuongAnVay> pavay = new List<HoSoPhuongAnVay>();
            foreach (var k in ket)
            {
                HoSoPhuongAnVay hspa = new HoSoPhuongAnVay();
                hspa.IdHsvay = k.IdHsvay;
                hspa.IdHspavay = k.IdHspavay;
                hspa.NgayNhanHs = k.NgayNhanHs;
                hspa.KeHoachTraNo = k.KeHoachTraNo;
                hspa.PhuongAnKd = k.PhuongAnKd;
                pavay.Add(hspa);

            }
            return View(pavay);
        }

        // GET: HoSoPhuongAnVays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.HoSoPhuongAnVays == null)
            {
                return NotFound();
            }

            var hoSoPhuongAnVay = await _context.HoSoPhuongAnVays
                .Include(h => h.IdHsvayNavigation)
                .FirstOrDefaultAsync(m => m.IdHspavay == id);
            if (hoSoPhuongAnVay == null)
            {
                return NotFound();
            }

            return View(hoSoPhuongAnVay);
        }

        // GET: HoSoPhuongAnVays/Create
        public IActionResult Create()
        {
            ViewData["IdHsvay"] = new SelectList(_context.HoSoVayDoanhNghieps, "IdHsvay", "IdHsvay");
            return View();
        }

        // POST: HoSoPhuongAnVays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] HoSoPhuongAnVayCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            HoSoPhuongAnVay hs = new HoSoPhuongAnVay()
            {
                IdHsvay = request.IdHsvay,
                PhuongAnKd = await SaveFile(request.PhuongAnKd),
                KeHoachTraNo = await SaveFile(request.KeHoachTraNo),
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

        // GET: HoSoPhuongAnVays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.HoSoPhuongAnVays == null)
            {
                return NotFound();
            }

            var hoSoPhuongAnVay = await _context.HoSoPhuongAnVays.FindAsync(id);
            if (hoSoPhuongAnVay == null)
            {
                return NotFound();
            }
            ViewData["IdHsvay"] = new SelectList(_context.HoSoVayDoanhNghieps, "IdHsvay", "IdHsvay", hoSoPhuongAnVay.IdHsvay);
            return View(hoSoPhuongAnVay);
        }

        // POST: HoSoPhuongAnVays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdHspavay,PhuongAnKd,KeHoachTraNo,NgayNhanHs,IdHsvay")] HoSoPhuongAnVay hoSoPhuongAnVay)
        {
            if (id != hoSoPhuongAnVay.IdHspavay)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoSoPhuongAnVay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoSoPhuongAnVayExists(hoSoPhuongAnVay.IdHspavay))
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
            ViewData["IdHsvay"] = new SelectList(_context.HoSoVayDoanhNghieps, "IdHsvay", "IdHsvay", hoSoPhuongAnVay.IdHsvay);
            return View(hoSoPhuongAnVay);
        }

        // GET: HoSoPhuongAnVays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.HoSoPhuongAnVays == null)
            {
                return NotFound();
            }

            var hoSoPhuongAnVay = await _context.HoSoPhuongAnVays
                .Include(h => h.IdHsvayNavigation)
                .FirstOrDefaultAsync(m => m.IdHspavay == id);
            if (hoSoPhuongAnVay == null)
            {
                return NotFound();
            }

            return View(hoSoPhuongAnVay);
        }

        // POST: HoSoPhuongAnVays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.HoSoPhuongAnVays == null)
            {
                return Problem("Entity set 'DAChuyenNganhContext.HoSoPhuongAnVays'  is null.");
            }
            var hoSoPhuongAnVay = await _context.HoSoPhuongAnVays.FindAsync(id);
            if (hoSoPhuongAnVay != null)
            {
                _context.HoSoPhuongAnVays.Remove(hoSoPhuongAnVay);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoSoPhuongAnVayExists(int id)
        {
            return _context.HoSoPhuongAnVays.Any(e => e.IdHspavay == id);
        }
    }
}
