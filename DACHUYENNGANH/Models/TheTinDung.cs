using System;
using System.Collections.Generic;

namespace DACHUYENNGANH.Models
{
    public partial class TheTinDung
    {
        public string Stk { get; set; } = null!;
        public string TenTk { get; set; } = null!;
        public string SoTrenThe { get; set; } = null!;
        public DateTime NgayMoThe { get; set; }
        public DateTime NgayNhanThe { get; set; }
        public int IdHstinDung { get; set; }

        public virtual HoSoTinDung IdHstinDungNavigation { get; set; } = null!;
    }
}
