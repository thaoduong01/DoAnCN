using System;
using System.Collections.Generic;

namespace DACHUYENNGANH.Models
{
    public partial class ChucVu
    {
        public ChucVu()
        {
            NhanViens = new HashSet<NhanVien>();
        }

        public string IdChucVu { get; set; } = null!;
        public string TenChucVu { get; set; } = null!;

        public virtual ICollection<NhanVien> NhanViens { get; set; }
    }
}
