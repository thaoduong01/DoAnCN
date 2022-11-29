using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DACHUYENNGANH.Models
{
    public partial class TheTinDung
    {
        public string Stk { get; set; } = null!;
        public string TenTk { get; set; } = null!;
        public string SoTrenThe { get; set; } = null!;

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayMoThe { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayNhanThe { get; set; }
        public string IdHstinDung { get; set; } = null!;
        public string? MaPin { get; set; }
        public string? SoSauThe { get; set; }

        public virtual HoSoTinDung IdHstinDungNavigation { get; set; } = null!;
    }
}
