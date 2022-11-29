using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DACHUYENNGANH.Models
{
    public partial class HoSoThamDinh
    {
        public int IdHsthamDinh { get; set; }
        public double SoTienThamDinh { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayThamDinh { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayNhanHoSo { get; set; }
        public string BaoCaoThamDinh { get; set; } = null!;
        public string TenNguoiThamDinh { get; set; } = null!;
        public int CmndCccd { get; set; }
        public string IdCongTy { get; set; } = null!;
        public int IdHsdb { get; set; }

        public virtual CongTyThamDinh IdCongTyNavigation { get; set; } = null!;
        public virtual HoSoTaiSanDb IdHsdbNavigation { get; set; } = null!;
    }
}
