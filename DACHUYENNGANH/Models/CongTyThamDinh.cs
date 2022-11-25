using System;
using System.Collections.Generic;

namespace DACHUYENNGANH.Models
{
    public partial class CongTyThamDinh
    {
        public CongTyThamDinh()
        {
            HoSoThamDinhs = new HashSet<HoSoThamDinh>();
        }

        public string IdCongTy { get; set; } = null!;
        public string TenCty { get; set; } = null!;
        public string DiaChi { get; set; } = null!;
        public string Sdt { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual ICollection<HoSoThamDinh> HoSoThamDinhs { get; set; }
    }
}
