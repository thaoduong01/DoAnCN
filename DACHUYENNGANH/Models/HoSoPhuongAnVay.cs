using System;
using System.Collections.Generic;

namespace DACHUYENNGANH.Models
{
    public partial class HoSoPhuongAnVay
    {
        public int IdHspavay { get; set; }
        public string PhuongAnKd { get; set; } = null!;
        public string KeHoachTraNo { get; set; } = null!;
        public DateTime NgayNhanHs { get; set; }
        public string IdHsvay { get; set; } = null!;

        public virtual HoSoVayDoanhNghiep IdHsvayNavigation { get; set; } = null!;
    }
}
