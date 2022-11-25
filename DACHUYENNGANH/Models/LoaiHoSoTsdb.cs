using System;
using System.Collections.Generic;

namespace DACHUYENNGANH.Models
{
    public partial class LoaiHoSoTsdb
    {
        public LoaiHoSoTsdb()
        {
            HoSoTaiSanDbs = new HashSet<HoSoTaiSanDb>();
        }

        public int IdLoaiHs { get; set; }
        public string TenLoai { get; set; } = null!;

        public virtual ICollection<HoSoTaiSanDb> HoSoTaiSanDbs { get; set; }
    }
}
