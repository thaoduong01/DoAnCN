using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayBdvay { get; set; }
        public double SoTienVay { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
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
