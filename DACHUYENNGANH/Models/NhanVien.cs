using System;
using System.Collections.Generic;

namespace DACHUYENNGANH.Models
{
    public partial class NhanVien
    {
        public NhanVien()
        {
            HoSoTinDungs = new HashSet<HoSoTinDung>();
            HoSoVayDoanhNghieps = new HashSet<HoSoVayDoanhNghiep>();
        }

        public string IdNhanVien { get; set; } = null!;
        public string TenNhanVien { get; set; } = null!;
        public DateTime NgaySinh { get; set; }
        public string CmndCccd { get; set; } = null!;
        public string Sdt { get; set; } = null!;
        public string GioiTinh { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string TenDangNhap { get; set; } = null!;
        public string MatKhau { get; set; } = null!;
        public string IdChucVu { get; set; } = null!;

        public virtual ChucVu IdChucVuNavigation { get; set; } = null!;
        public virtual ICollection<HoSoTinDung> HoSoTinDungs { get; set; }
        public virtual ICollection<HoSoVayDoanhNghiep> HoSoVayDoanhNghieps { get; set; }
    }
}
