using System;
using System.Collections.Generic;

namespace DACHUYENNGANH.Models
{
    public partial class KhachHang
    {
        public KhachHang()
        {
            DoanhNghieps = new HashSet<DoanhNghiep>();
            KhachHangCaNhans = new HashSet<KhachHangCaNhan>();
        }

        public int IdKhachHang { get; set; }
        public string? MaSoThue { get; set; }

        public virtual ICollection<DoanhNghiep> DoanhNghieps { get; set; }
        public virtual ICollection<KhachHangCaNhan> KhachHangCaNhans { get; set; }
    }
}
