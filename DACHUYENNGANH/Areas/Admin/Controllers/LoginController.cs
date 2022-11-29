using DACHUYENNGANH.Models;
using Microsoft.AspNetCore.Mvc;

namespace DACHUYENNGANH.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private readonly DAChuyenNganhContext _context;

        public LoginController(DAChuyenNganhContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            if (HttpContext.Session.GetString("TenDangNhap") == null)
            {

            }
            ViewBag.IdChucVu = HttpContext.Session.GetString("TenDangNhap");

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([Bind("TenDangNhap,MatKhau")] NhanVien nhanVien)
        {
            if (nhanVien.TenDangNhap != null && nhanVien.MatKhau != null)
            {
                nhanVien.MatKhau = CreateMD5(nhanVien.MatKhau);
                var nhanvien = _context.NhanViens.FirstOrDefault(x => x.TenDangNhap == nhanVien.TenDangNhap && x.MatKhau == nhanVien.MatKhau);
                if (nhanvien == null)
                {
                    ViewBag.Mesage = "Đăng nhập không thành công!!!";
                    return View();
                }

                HttpContext.Session.SetString("TenDangNhap", nhanvien.TenDangNhap);
                HttpContext.Session.SetString("IdChucVu", nhanvien.IdChucVu);

                return RedirectToAction("Index");

                //string chucvu = nhanvien.IdChucVu;
                //if (chucvu == "NVIT1")
                //{
                //    return RedirectToAction("Index", "Home", new { area = "Admin" });
                //}
                //else
                //{
                //    return RedirectToAction("Index", "Home");
                //}
            }

            return View();
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
    }
}
