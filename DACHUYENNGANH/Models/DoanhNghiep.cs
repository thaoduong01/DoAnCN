using System;
using System.Collections.Generic;

namespace DACHUYENNGANH.Models
{
    public partial class DoanhNghiep
    {
        public DoanhNghiep()
        {
            HoSoVayDoanhNghieps = new HashSet<HoSoVayDoanhNghiep>();
        }

        public string TenDoanhNghiep { get; set; } = null!;
        public string DiaChi { get; set; } = null!;
        public string Sdt { get; set; } = null!;
        public string TenNguoiDaiDien { get; set; } = null!;
        public string CmndCccd { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string IdDoanhNghiep { get; set; } = null!;
        public int IdKhachHang { get; set; }

        public virtual KhachHang IdKhachHangNavigation { get; set; } = null!;
        public virtual ICollection<HoSoVayDoanhNghiep> HoSoVayDoanhNghieps { get; set; }
    }
}
