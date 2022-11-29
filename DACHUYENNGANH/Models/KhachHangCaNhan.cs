using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DACHUYENNGANH.Models
{
    public partial class KhachHangCaNhan
    {
        public KhachHangCaNhan()
        {
            HoSoTinDungs = new HashSet<HoSoTinDung>();
        }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
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
