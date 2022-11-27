using System;
using System.Collections.Generic;

namespace DACHUYENNGANH.Models
{
    public partial class HoSoTinDung
    {
        public HoSoTinDung()
        {
            TheTinDungs = new HashSet<TheTinDung>();
        }

        public string IdHstinDung { get; set; } = null!;
        public DateTime NgayNhanHs { get; set; }
        public double PhiMoThe { get; set; }
        public string ChuKy { get; set; } = null!;
        public string IdNhanVien { get; set; } = null!;
        public string IdKhachHangCaNhan { get; set; } = null!;

        public virtual KhachHangCaNhan IdKhachHangCaNhanNavigation { get; set; } = null!;
        public virtual NhanVien IdNhanVienNavigation { get; set; } = null!;
        public virtual ICollection<TheTinDung> TheTinDungs { get; set; }
    }
}
