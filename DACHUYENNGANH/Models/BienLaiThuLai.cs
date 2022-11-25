using System;
using System.Collections.Generic;

namespace DACHUYENNGANH.Models
{
    public partial class BienLaiThuLai
    {
        public int IdBienLai { get; set; }
        public DateTime NgayNhanLai { get; set; }
        public string IdNhanVien { get; set; } = null!;
        public string IdHsvay { get; set; } = null!;
        public double? DuNo { get; set; }
        public double? LaiKyKe { get; set; }
        public double? GocKyKe { get; set; }
        public double LaiSuat { get; set; }
        public double? SoTienDong { get; set; }
        public double TienGoc { get; set; }

        public virtual HoSoVayDoanhNghiep IdHsvayNavigation { get; set; } = null!;
        public virtual NhanVien IdNhanVienNavigation { get; set; } = null!;
    }
}
