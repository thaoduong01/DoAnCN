using System;
using System.Collections.Generic;

namespace DACHUYENNGANH.Models
{
    public partial class KhachHangCaNhan
    {
        public KhachHangCaNhan()
        {
            HoSoTinDungs = new HashSet<HoSoTinDung>();
        }

        public DateTime NgaySinh { get; set; }
        public int IdKhachHang { get; set; }
        public string DiaChi { get; set; } = null!;
        public string GioiTinh { get; set; } = null!;
        public string Sdt { get; set; } = null!;
        public string CmndCccd { get; set; } = null!;
        public string TenKh { get; set; } = null!;
        public string IdKhachHangCaNhan { get; set; } = null!;

        public virtual KhachHang IdKhachHangNavigation { get; set; } = null!;
        public virtual ICollection<HoSoTinDung> HoSoTinDungs { get; set; }
    }
}
