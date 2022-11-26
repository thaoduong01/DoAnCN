using System;
using System.Collections.Generic;

namespace DACHUYENNGANH.Models
{
    public partial class HoSoThamDinh
    {
        public int IdHsthamDinh { get; set; }
        public double SoTienThamDinh { get; set; }
        public DateTime NgayThamDinh { get; set; }
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
