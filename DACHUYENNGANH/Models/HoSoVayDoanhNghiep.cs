using System;
using System.Collections.Generic;

namespace DACHUYENNGANH.Models
{
    public partial class HoSoVayDoanhNghiep
    {
        public HoSoVayDoanhNghiep()
        {
            HoSoBaoCaoTcs = new HashSet<HoSoBaoCaoTc>();
            HoSoPhapLies = new HashSet<HoSoPhapLy>();
            HoSoPhuongAnVays = new HashSet<HoSoPhuongAnVay>();
            HoSoTaiSanDbs = new HashSet<HoSoTaiSanDb>();
        }

        public string IdHsvay { get; set; } = null!;
        public DateTime NgayBdvay { get; set; }
        public double SoTienVay { get; set; }
        public DateTime NgayKt { get; set; }
        public double LaiSuat { get; set; }
        public string IdNhanVien { get; set; } = null!;
        public string IdDoanhNghiep { get; set; } = null!;

        public virtual DoanhNghiep IdDoanhNghiepNavigation { get; set; } = null!;
        public virtual NhanVien IdNhanVienNavigation { get; set; } = null!;
        public virtual ICollection<HoSoBaoCaoTc> HoSoBaoCaoTcs { get; set; }
        public virtual ICollection<HoSoPhapLy> HoSoPhapLies { get; set; }
        public virtual ICollection<HoSoPhuongAnVay> HoSoPhuongAnVays { get; set; }
        public virtual ICollection<HoSoTaiSanDb> HoSoTaiSanDbs { get; set; }
    }
}
