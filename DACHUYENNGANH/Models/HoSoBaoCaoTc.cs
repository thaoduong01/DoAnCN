using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DACHUYENNGANH.Models
{
    public partial class HoSoBaoCaoTc
    {
        public int IdBctc { get; set; }
        public string? ToVat { get; set; }
        public string? HopDongSdld { get; set; }
        public string? HopDongMuaBan { get; set; }
        public string SaoKeTknh { get; set; } = null!;

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayNhanHs { get; set; }
        public string BctaiChinh { get; set; } = null!;
        public string IdHsvay { get; set; } = null!;

        public virtual HoSoVayDoanhNghiep IdHsvayNavigation { get; set; } = null!;
    }
}
