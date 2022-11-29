using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DACHUYENNGANH.Models
{
    public partial class HoSoTinDung
    {
        public HoSoTinDung()
        {
            TheTinDungs = new HashSet<TheTinDung>();
        }

        public string IdHstinDung { get; set; } = null!;

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
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
