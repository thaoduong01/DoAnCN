using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DACHUYENNGANH.Models
{
    public partial class HoSoPhapLy
    {
        public int IdPhapLy { get; set; }
        public string Gdkkd { get; set; } = null!;
        public string DieuLeCty { get; set; } = null!;
        public string BbhopHd { get; set; } = null!;
        public string TenKttruong { get; set; } = null!;
        public string CmndCccdKtt { get; set; } = null!;

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]        
        public DateTime NgayNhanHs { get; set; }
        public string Gcndkthue { get; set; } = null!;
        public string IdHsvay { get; set; } = null!;

        public virtual HoSoVayDoanhNghiep IdHsvayNavigation { get; set; } = null!;
    }
}
